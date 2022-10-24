
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] List<TMP_Text> _textObjs; //list of text objects you want to change the colour of
    [SerializeField] List<Image> _gameImage; //list of game objects you want to change the colour of

    [SerializeField] Color _defaultColor = Color.black; //default colour of the text
    [SerializeField] Color _highlightColor = Color.white; //colour you want to change the text to when you are hovering over it
    [SerializeField] bool _waveText, _continuousWave, _alwaysWavy = false; //is this text a wavy text
    [SerializeField] bool _changeColorOnHover = true; //change texts coulour on highlight?
    [SerializeField] bool _clicked = false; //has it been clicked (keep highlight only if you call script on button)
    bool _makeWave = false; //is it hovered (make it wavy in update)

    void Awake() //enable clicked in editor to make it start highlighted + wavy
    {
        if (_clicked)
        {
            _clicked = false; //set to false so that the clicked color toggle triggers
            ToggleClickedColour(true);
        }
    }
    void OnEnable() //reset text to default colours and make wave stop.
    {
        if (!_clicked) //if this hasnt been clicked but was hovered and then disabled reset it to default
        {
            HighlightText(false);
            HighlightGameObject(false);


            if (_waveText) //disable wavy text on opening (if enabled in editor)
            {
                _makeWave = false;
                _continuousWave = false;
            }
        }
    }
    void Update()
    {
        if (_makeWave || _continuousWave || _alwaysWavy)
        {
            WaveText();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_clicked)
        {
            HighlightText(true);
            HighlightGameObject(true);

            if (_waveText) //enable wavy text on hover(if enabled in editor)
            {
                _makeWave = true;

            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_clicked)
        {
            HighlightText(false);
            HighlightGameObject(false);
            if (_waveText) //disable wavy text on exit(if enabled in editor)
            {
                _makeWave = false;

            }
        }
    }
    private void HighlightText(bool m_highlight)
    {
        if (_textObjs != null)//make text default colour if there is text objects linked in editor
        {
            foreach (TMP_Text m_text in _textObjs)
            {
                if (m_highlight) //should you highlight text
                {
                    m_text.color = _highlightColor;

                }
                else
                {
                    m_text.color = _defaultColor;

                }
            }
        }
    }
    private void HighlightGameObject(bool m_highlight)
    {
        if (_gameImage != null)//make text default colour if there is text objects linked in editor
        {
            foreach (Image m_gameImage in _gameImage)
            {
                if (m_highlight) //should you highlight text
                {
                    m_gameImage.color = _highlightColor;

                }
                else
                {
                    m_gameImage.color = _defaultColor;

                }
            }
        }
    }
    public void ToggleConstantWave()
    {
        _continuousWave = !_continuousWave;
    }
    private void WaveText() //make text wavy
    {
        foreach (TMP_Text m_text in _textObjs)
        {
            m_text.ForceMeshUpdate(); //update mesh
            var m_textInfo = m_text.textInfo;

            for (int i = 0; i < m_textInfo.characterCount; i++)
            {
                var m_charInfo = m_textInfo.characterInfo[i];
                if (!m_charInfo.isVisible)
                {
                    continue; //skip invis charcaters
                }
                var verts = m_textInfo.meshInfo[m_charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[m_charInfo.vertexIndex + j];
                    //verts[m_charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Sin(Time.time * 6f + orig.x * 0.04f) * 5f, 0, 0); //MAKE IT LOOK LIKE ITS BEHIND WATER
                    //verts[m_charInfo.vertexIndex + j] = orig + new Vector3(Random.Range(0,10), Random.Range(0,10), 0); //MAKE IT RUMBLE
                    verts[m_charInfo.vertexIndex + j] = orig + new Vector3(5 * Mathf.Sin(Time.time * 6f + orig.x * 0.04f), 5 * Mathf.Sin(Time.time * 6f + orig.x * 0.04f), 0);  //MAKE IT FLAGGY


                }
            }
            for (int i = 0; i < m_textInfo.meshInfo.Length; i++)
            {
                var m_meshInfo = m_textInfo.meshInfo[i];
                m_meshInfo.mesh.vertices = m_meshInfo.vertices;
                m_text.UpdateGeometry(m_meshInfo.mesh, i);
            }
        }
    }


    public void ToggleClickedColour(bool m_clicked)
    {
        if (m_clicked != _clicked) //ensure its a different click
        {
            _clicked = m_clicked;
            if (_textObjs != null)////highlight text if there is text objects linked in editor
            {
                if (_waveText)//(if wavy enabled in editor)
                {
                    ToggleConstantWave();
                    _makeWave = false;
                }
                HighlightText(_clicked);
                HighlightGameObject(_clicked);


            }
        }
    }

}
