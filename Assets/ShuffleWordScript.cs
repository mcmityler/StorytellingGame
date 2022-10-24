using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleWordScript : MonoBehaviour
{
    [SerializeField] private Animator _overallWordAnimator; //ref to animator that animates all word banks to disable it to allow it to play shuffle animation properly
    [SerializeField] private Animator _shuffleAnimator; //animator that shuffles the image
    private List<Sprite> _randomWordSprites; //list of sprites to use when shuffling
    [SerializeField] GameObject[] _imageObjs;//0 = normal, 1 = placeholder1, 2 = placeholder2
    [SerializeField] private int _amountOfLoops = 10; //how many shuffle loops to play
    private int _wordObjNumber = 0; //what number word object is this animating
    private int _counter = 0;//how many shuffle loops you have left (sets when you press shuffle)
    private int _lastRandom; //last picture it showed when shuffling (so it doesnt show back to back same pictures)
    [SerializeField] private GameScript _gameScript;
    [SerializeField] private TMP_Text _wordNameText;
    private Sprite _newWordSprite = null;

    private bool _blankTextShuffle = false;

    [SerializeField] private float _shuffleAnimSpeed = 4;
    [SerializeField] private float _minimumShuffleSpeed = 1;



    public void SetRandomSprites(List<Sprite> m_spriteList) //fill what images to use when randoming when you start a game it fills the list with words 
    {
        _randomWordSprites = new List<Sprite>(m_spriteList);
    }
    public void StartSingleShuffle(int m_wordObjNum)
    {
        if (_overallWordAnimator.isActiveAndEnabled) //disable overall word obj animator to allow images to be shuffled
        {
            _overallWordAnimator.enabled = false;
        }
        _newWordSprite = null; //reset next words sprite
        do
        {
            _newWordSprite = _gameScript.ShuffleWords(m_wordObjNum); //set next words sprite from words left in current games wordbox (get next word before animation is over so you can play multiple animations easily)
        } while (_newWordSprite == null);
        _counter = _amountOfLoops; //set how many loops of the shuffle animation to play
        _wordObjNumber = m_wordObjNum; //what word obj this is... for game script but not sure  if i really need this
        _shuffleAnimator.SetFloat("ShuffleSpeed", _shuffleAnimSpeed);
        _shuffleAnimator.SetTrigger("Shuffle"); //start shuffle animation
    }
    public void ShuffleWithDifferentLoopAmount(int m_wordObjNum, int m_loopAmount)
    {
        if (_overallWordAnimator.isActiveAndEnabled) //disable overall word obj animator to allow images to be shuffled
        {
            _overallWordAnimator.enabled = false;
        }
        _newWordSprite = null; //reset next words sprite
        do
        {
            _newWordSprite = _gameScript.ShuffleWords(m_wordObjNum); //set next words sprite from words left in current games wordbox (get next word before animation is over so you can play multiple animations easily)
        } while (_newWordSprite == null);
        _counter = m_loopAmount; //set how many loops of the shuffle animation to play
        _wordObjNumber = m_wordObjNum; //what word obj this is... for game script but not sure  if i really need this
        _shuffleAnimator.SetFloat("ShuffleSpeed", _shuffleAnimSpeed);
        _shuffleAnimator.SetTrigger("Shuffle"); //start shuffle animation

    }
    public void ChangeToRandomImage(int m_shuffleImageObjNum) //0 = normal, 1 = placeholder1, 2 = placeholder2
    {
        if (m_shuffleImageObjNum == 0 && _counter == 0) //if you are on last shuffle animation set to final image.
        {
            //set to final picture
            _imageObjs[m_shuffleImageObjNum].GetComponent<Image>().sprite = _newWordSprite;
            return;
        }
        //ensure random images dont show the same ones back to back
        int m_random = 0;
        do //randomize until it was different then the last pictures number
        {
            m_random = Random.Range(0, _randomWordSprites.Count);
        } while (_lastRandom == m_random);
        _lastRandom = m_random; //remember last shuffle number (what image to not use nextv)
        _imageObjs[m_shuffleImageObjNum].GetComponent<Image>().sprite = _randomWordSprites[m_random]; //change image for shuffle animation
        if (!_blankTextShuffle) //depending if you want to hide names while shuffling or show them
        {
            _wordNameText.text = _randomWordSprites[m_random].name; //change the image display name for the shuffle animaiton
        }
        else
        {
            _wordNameText.text = ""; //change the image display name for the shuffle animaiton
        }


    }
    public void AnimationCompleted() //called at the end of every loop within the animation to count how many loops have passed before you trigger a different animation
    {
        _counter--;
        if (_counter < 0)
        {
            _shuffleAnimator.SetTrigger("EndShuffle");
            return;
        }
        //set new shuffle speed below the counter check so it doesnt get a negative speed.
        //float m_temp = _shuffleAnimSpeed * ((float)_counter / (float)_amountOfLoops) + _minimumShuffleSpeed;

        float m_temp = _shuffleAnimSpeed;
        float m_speeddecrement = 0.7f;

        if (_counter > 6) //SLOW DOWN THE ANIMATION SPEED OF SHUFFLE AS IT GETS CLOSER TO ENDING
        {
            m_temp = _shuffleAnimSpeed;
        }
        else if (_counter > 4)
        {
            m_temp = _shuffleAnimSpeed - m_speeddecrement;
        }
        else if (_counter > 2)
        {
            m_temp = _shuffleAnimSpeed - (m_speeddecrement * 2);
        }
        else
        {
            m_temp = _shuffleAnimSpeed - (m_speeddecrement * 3);
        }

        //Debug.Log(_wordObjNumber + " " + _counter + " " + m_temp);

        _shuffleAnimator.SetFloat("ShuffleSpeed", m_temp);
    }
    public void UpdateName() //update the text box that displays the words name, this is called within the idle animation because that is the final animation after the shuffle is over
    {
        _wordNameText.text = _imageObjs[0].GetComponent<Image>().sprite.name;
        if (_wordNameText.text == "QuestionMark")
        {
            _wordNameText.text = "???";
        }
    }

    public void BackToSelection() //called by back button on game screen to reenable the animator that opens word objects upon opening gamescreen
    {
        _overallWordAnimator.enabled = true;
        _shuffleAnimator.ResetTrigger("Shuffle");
        _shuffleAnimator.SetTrigger("EndShuffle");
        _counter = 0;

    }
    public void ToggleBlankText(Toggle m_toggle)
    {
        _blankTextShuffle = m_toggle.isOn;
    }
}
