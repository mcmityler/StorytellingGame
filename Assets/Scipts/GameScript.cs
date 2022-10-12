/*
Created By: Tyler McMillan
Description: This script deals with the main functionality of the word game
*/
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private int _wordCount = 2; //how many words are in the game (set by buttons on selections screen)
    private string _wordDifficulty = "easy"; //what is the difficulty of the game (set by buttons on selections screen)
    [SerializeField] List<Sprite> _easyWordSprites; //list of easy word pictures (real pictures)
    [SerializeField] List<Sprite> _mediumWordSprites;  //list of medium word pictures (real pictures)
    [SerializeField] List<Sprite> _hardWordSprites; //list of hard word pictures (real pictures)
    [SerializeField] List<Sprite> _cartoonSprites; //list of all cartoon pictures
    [SerializeField] GameObject[] _wordObjects; //gameobjects for words (goes from 1,2,3,4)
    [SerializeField] Animator _wordController; //animator to make words appear in game screen
    private Sprite[] _backupIRLSprites = { null, null, null, null }; //place holder for irl image when you click turn to cartoon button
    private bool[] _isCartoon = { false, false, false, false }; //bool to tell which word objects are on the cartoon or real image (for the tooltip)
    List<string> _gameWordBank = new List<string>(); //list of words you are using for the game
    List<Sprite> _gameSpriteBank = new List<Sprite>(); //list of sprites you are using for the game
    [SerializeField] private GameObject[] _noSoundObj; //gameobject of text that tells player if a sound exists or not
    public void SetWordCount(int m_wordCount) //set how many word objects to display (from buttons on selection screen)
    {
        _wordCount = m_wordCount;
    }
    public int GetWordCount()//get how many words you are displaying from another script
    {
        return _wordCount;
    }
    public void SetWordDifficulty(string m_difficulty) //set how difficulty of the words or what word banks to use (from buttons on selection screen)
    {
        _wordDifficulty = m_difficulty;
    }
    public void StartGame() //called by screen manager script when you click start game button on selection screen
    {
        Debug.Log("Start game here! " + _wordDifficulty + " difficulty " + _wordCount.ToString() + " words"); //output difficulty (easy,medium,or hard) & word amount (2-4)
        _wordController.SetInteger("WordCount", _wordCount); //animate opening the word objects that are needed (2-4)
        RefillBank(); //refill gamewordbank with all level of words you want
        RandomizePictures(-1); //randomize all words that will go into word objects (-1 means shuffle all, 0,1,2,3 indicates which word to shuffle)
    }
    void RefillBank() //refill word bank with all words you intend to use
    {
        _gameWordBank = new List<string>(); //empty word bank
        _gameSpriteBank = new List<Sprite>(); //empty sprite bank
        if (_wordDifficulty == "easy") //if easy difficulty add just easy words
        {
            AddEasyWords();
        }
        else if (_wordDifficulty == "medium")//if medium difficulty add  easy words & medium words
        {
            AddEasyWords();
            AddMediumWords();
        }
        else if (_wordDifficulty == "hard")//if hard difficulty add  easy words & medium words & hard words
        {
            AddEasyWords();
            AddMediumWords();
            AddHardWords();
        }
    }
    void AddEasyWords() //add easy words to game bank
    {
        foreach (Sprite _sprite in _easyWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    void AddMediumWords()//add medium words to game bank
    {
        foreach (Sprite _sprite in _mediumWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    void AddHardWords()//add hard words to game bank
    {
        foreach (Sprite _sprite in _hardWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    public void RandomizePictures(int m_objectNum) //SEND num of object you want to random to shuffle single, send -1 to shuffle all (-1 means shuffle all, 0,1,2,3 indicates which word to shuffle)
    {
        FindObjectOfType<SoundManager>().StopAllAudio(); //stop audio playing when changing pictures
        if (_gameWordBank.Count >= _wordCount || (m_objectNum != -1 && _gameWordBank.Count > 0)) //make sure enough words left in word bank
        {
            if (m_objectNum == -1) //SHUFFLE ALL
            {
                for (int i = 0; i < _wordCount; i++)
                {
                    int m_randomNum = Random.Range(0, _gameWordBank.Count);
                    _wordObjects[i].transform.Find("ObjectWordImage").GetComponentInChildren<Image>().sprite = _gameSpriteBank[m_randomNum]; //set new image to random image, also make sure its set to correct child, and not the bg child.
                    _wordObjects[i].GetComponentInChildren<TMP_Text>().text = _gameWordBank[m_randomNum];
                    _wordObjects[i].transform.Find("ImageToggle").GetComponent<TooltipTrigger>().ResetTiptext();
                    _backupIRLSprites[i] = _gameSpriteBank[m_randomNum]; //set backup sprite for when turning back to irl image when its a cartoon
                    _isCartoon[i] = false; //make sure it knows its a real image
                    DisplayAudioBtn(i);
                    _gameWordBank.Remove(_gameWordBank[m_randomNum]);
                    _gameSpriteBank.Remove(_gameSpriteBank[m_randomNum]);
                }
            }
            else //SINGLE SHUFFLE
            {
                int m_randomNum = Random.Range(0, _gameWordBank.Count);
                _wordObjects[m_objectNum].transform.Find("ObjectWordImage").GetComponent<Image>().sprite = _gameSpriteBank[m_randomNum];//set new image to random image, also make sure its set to correct child, and not the bg child.
                _wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text = _gameWordBank[m_randomNum];
                _wordObjects[m_objectNum].transform.Find("ImageToggle").GetComponent<TooltipTrigger>().ResetTiptext();
                _backupIRLSprites[m_objectNum] = _gameSpriteBank[m_randomNum]; //set backup sprite for when turning back to irl image when its a cartoon
                _isCartoon[m_objectNum] = false;//make sure it knows its a real image
                DisplayAudioBtn(m_objectNum);
                _gameWordBank.Remove(_gameWordBank[m_randomNum]);
                _gameSpriteBank.Remove(_gameSpriteBank[m_randomNum]);
            }
        }
        else //if you run out of words while shuffling
        {
            Debug.Log("out of words");
            RefillBank(); //refill word bank

            ///remove already used words (that are currently in word objects so you dont get duplicates on screen)
            for (int i = 0; i < _wordCount; i++) //cycle entire wordbank to find words that are already in bank
            {
                if (_gameWordBank.Count > 0)
                {
                    int m_m = 0;
                    foreach (string m_string in _gameWordBank)
                    {
                        Debug.Log(i);
                        if (m_string == _wordObjects[i].GetComponentInChildren<TMP_Text>().text)
                        {
                            break;
                        }
                        else
                        {
                            m_m++;
                        }
                    }
                    _gameWordBank.RemoveAt(m_m);
                    _gameSpriteBank.RemoveAt(m_m);

                }
            }

            RandomizePictures(m_objectNum); //recall this function but now with a full word bank
        }
    }

    public void PlayAnimalNoise(int m_objectNum) //play animal noise from pressing IRL noise button on game screen word objects
    {
        bool m_isExistingSound = FindObjectOfType<SoundManager>().PlayAnimal(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text); //get what the current words noise is from sound manager
        Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text); //output wordobj word
        if (m_isExistingSound == false)//tell if sound exists or not
        {
            Debug.Log("irl sound didnt Exist");
        }
        else
        {
            Debug.Log("irl sound played");
        }
    }
    public void PlayPronunciationNoise(int m_objectNum)  //play pronunciation noise from pressing pronounce noise button on game screen word objects
    {
        bool m_isExistingSound = FindObjectOfType<SoundManager>().PlayPronunciation(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);//get what the current words noise is from sound manager
        Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text); //output wordobj word
        if (m_isExistingSound == false) //tell if sound exists or not
        {
            Debug.Log("pronunciation sound didnt Exist");
        }
        else
        {
            Debug.Log("pronunciation sound played");
        }
    }
    void DisplayAudioBtn(int m_objectNum) //display what sounds are available to user in text box ********** Where to control showing sound buttons
    {

        string _displayAudio = FindObjectOfType<SoundManager>().SoundsExists(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        GameObject m_noSoundObj = _noSoundObj[m_objectNum];
        m_noSoundObj.SetActive(true);
        //Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        switch (_displayAudio)
        {
            case "noreference":
                m_noSoundObj.GetComponent<TMP_Text>().text = "No Reference";
                break;
            case "nosounds":
                m_noSoundObj.GetComponent<TMP_Text>().text = "No Sounds";
                break;
            case "noirl":
                m_noSoundObj.GetComponent<TMP_Text>().text = "No IRL";
                break;
            case "nopron":
                m_noSoundObj.GetComponent<TMP_Text>().text = "No Pronounce";
                break;
            case "hassounds":
                m_noSoundObj.GetComponent<TMP_Text>().text = "";
                m_noSoundObj.SetActive(false);
                break;
        }
    }

    public void ToggleImages(int m_objectNum) //called by button on word objects to change images between IRL and caroon image
    {
        string m_currentWord = _wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text; //get what the image is called from the text box
        bool m_cartoonExists = false; //does the cartoon exist
        if (_isCartoon[m_objectNum] == false) //if its an irl image set it to a cartoon image
        {
            foreach (Sprite m_cartoonImage in _cartoonSprites) //cycle through all cartoons to find correct image and set it
            {
                if (m_cartoonImage.name == m_currentWord + "Cartoon")
                {
                    m_cartoonExists = true;
                    _isCartoon[m_objectNum] = true;
                    _wordObjects[m_objectNum].transform.Find("ObjectWordImage").GetComponent<Image>().sprite = m_cartoonImage;
                    _wordObjects[m_objectNum].transform.Find("ImageToggle").GetComponent<TooltipTrigger>().ChangeContentTip();
                    break;
                }
            }
            if (m_cartoonExists == false && _isCartoon[m_objectNum] == false) //no cartoon exists
            {
                Debug.Log("no such cartoon: " + m_currentWord);
                _isCartoon[m_objectNum] = true;
                _wordObjects[m_objectNum].transform.Find("ObjectWordImage").GetComponent<Image>().sprite = _cartoonSprites[0]; //make it no cartoon image if the cartoon doesnt exist
            }

        }
        else if (_isCartoon[m_objectNum] == true) //if its a cartoon set back to irl image
        {
            _isCartoon[m_objectNum] = false;
            _wordObjects[m_objectNum].transform.Find("ObjectWordImage").GetComponent<Image>().sprite = _backupIRLSprites[m_objectNum]; //set image through backup that was set when it was initialized
            _wordObjects[m_objectNum].transform.Find("ImageToggle").GetComponent<TooltipTrigger>().ChangeContentTip();
        }



    }

}
