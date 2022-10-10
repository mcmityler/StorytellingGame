using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private int _wordCount = 2;
    private string _wordDifficulty = "easy";
    [SerializeField] Animator _wordController;

    [SerializeField] List<Sprite> _easyWordSprites;
    [SerializeField] List<Sprite> _mediumWordSprites;
    [SerializeField] List<Sprite> _hardWordSprites;
    [SerializeField] List<Sprite> _cartoonSprites;
    [SerializeField] GameObject[] _wordObjects; //goes from 1,2,3,4
    private Sprite[] _backupIRLSprites = { null, null, null, null };
    private bool[] _isCartoon = { false, false, false, false };
    public void SetWordCount(int m_wordCount)
    {
        _wordCount = m_wordCount;
    }
    public int GetWordCount()
    {
        return _wordCount;
    }
    public void SetWordDifficulty(string m_difficulty)
    {
        _wordDifficulty = m_difficulty;
    }
    List<string> _gameWordBank = new List<string>();
    List<Sprite> _gameSpriteBank = new List<Sprite>();
    public void StartGame()
    {
        Debug.Log("Start game here! " + _wordDifficulty + " difficulty " + _wordCount.ToString() + " words");
        _wordController.SetInteger("WordCount", _wordCount);
        RefillBank();
        RandomizePictures(-1);
    }
    void RefillBank()
    {
        _gameWordBank = new List<string>();
        _gameSpriteBank = new List<Sprite>();
        if (_wordDifficulty == "easy")
        {
            AddEasyWords();
        }
        else if (_wordDifficulty == "medium")
        {
            AddEasyWords();
            AddMediumWords();
        }
        else if (_wordDifficulty == "hard")
        {
            AddEasyWords();
            AddMediumWords();
            AddHardWords();
        }
    }
    void AddEasyWords()
    {
        foreach (Sprite _sprite in _easyWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    void AddMediumWords()
    {
        foreach (Sprite _sprite in _mediumWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    void AddHardWords()
    {
        foreach (Sprite _sprite in _hardWordSprites)
        {
            _gameWordBank.Add(_sprite.name);
            _gameSpriteBank.Add(_sprite);

        }
    }
    public void RandomizePictures(int m_objectNum) //SEND num of object you want to random to shuffle single, send -1 to shuffle all
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
        else
        {
            Debug.Log("out of words");
            RefillBank();

            ///remove already used words
            for (int i = 0; i < _wordCount; i++)
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

            RandomizePictures(m_objectNum);
        }
    }

    public void PlayAnimalNoise(int m_objectNum)
    {
        bool m_isExistingSound = FindObjectOfType<SoundManager>().PlayAnimal(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        if (m_isExistingSound == false)
        {
            Debug.Log("irl sound didnt Exist");
        }
        else
        {
            Debug.Log("irl sound played");
        }
    }
    public void PlayPronunciationNoise(int m_objectNum)
    {
        bool m_isExistingSound = FindObjectOfType<SoundManager>().PlayPronunciation(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        if (m_isExistingSound == false)
        {
            Debug.Log("pronunciation sound didnt Exist");
        }
        else
        {
            Debug.Log("pronunciation sound played");
        }
    }
    [SerializeField] private GameObject[] _noSoundObj;
    void DisplayAudioBtn(int m_objectNum)
    {

        string _displayAudio = FindObjectOfType<SoundManager>().SoundsExists(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        GameObject m_noSoundObj = _noSoundObj[m_objectNum];
        //Debug.Log(_wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text);
        switch (_displayAudio)
        {
            case "noreference":
                m_noSoundObj.SetActive(true);
                m_noSoundObj.GetComponent<TMP_Text>().text = "No Reference";
                break;
            case "nosounds":
                m_noSoundObj.SetActive(true);
                m_noSoundObj.GetComponent<TMP_Text>().text = "No Sounds";
                break;
            case "noirl":
                m_noSoundObj.SetActive(true);
                m_noSoundObj.GetComponent<TMP_Text>().text = "No IRL";
                break;
            case "nopron":
                m_noSoundObj.SetActive(true);
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
