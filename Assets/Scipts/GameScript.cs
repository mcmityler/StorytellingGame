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
    [SerializeField] GameObject[] _wordObjects; //goes from 1,2,3,4
    public void SetWordCount(int m_wordCount)
    {
        _wordCount = m_wordCount;
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
                    int m_randomStart = Random.Range(0, _gameWordBank.Count);
                    _wordObjects[i].GetComponentInChildren<Image>().sprite = _gameSpriteBank[m_randomStart];
                    _wordObjects[i].GetComponentInChildren<TMP_Text>().text = _gameWordBank[m_randomStart];
                    _gameWordBank.Remove(_gameWordBank[m_randomStart]);
                    _gameSpriteBank.Remove(_gameSpriteBank[m_randomStart]);
                }
            }
            else //SINGLE SHUFFLE
            {
                int m_randomStart = Random.Range(0, _gameWordBank.Count);
                _wordObjects[m_objectNum].GetComponentInChildren<Image>().sprite = _gameSpriteBank[m_randomStart];
                _wordObjects[m_objectNum].GetComponentInChildren<TMP_Text>().text = _gameWordBank[m_randomStart];
                _gameWordBank.Remove(_gameWordBank[m_randomStart]);
                _gameSpriteBank.Remove(_gameSpriteBank[m_randomStart]);
            }
        }
        else
        {
            Debug.Log("out of words");
            RefillBank();
            ///remove already used words
            for (int i = 0; i < _wordCount; i++)
            {
                _gameWordBank.Remove(_wordObjects[i].GetComponentInChildren<TMP_Text>().text);
                _gameSpriteBank.Remove(_wordObjects[i].GetComponentInChildren<Image>().sprite);
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
}
