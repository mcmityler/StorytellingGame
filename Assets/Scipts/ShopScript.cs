using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cursorButtonObjs; //button objects of the cursors (they have numbers on them when pressed to send to this so you know, also cursor has matching number)
    [SerializeField] private TMP_Text _ticketText; //ref to ticket text object to update number of tickets
    [SerializeField] private TMP_Text _ticketSpentText; //ref to ticket spent text object to set how many tickets you spent
    [SerializeField] private int _ticketAmount = 50; //how many tickets you have right now
    [SerializeField] private int _currentCursorButtonNum = 0; //what is the current cursor you are usings number (used to equipped cursor to keep track and make it easy to 'equip' and 'unequip' them)
    [SerializeField] private List<int> _cursorPrices; //list of cursor prices (set in inspector, need as many prices as you have cursor buttons)
    private List<bool> _cursorOwned; //list of booleans to keep track of which cursors are owned
    private bool _cursorJustBought = false; //was the cursor just purchased (for sounds in equipped function)
    [SerializeField] private Color _purchasedColour = Color.clear; //colour to set the cursor button when you purchase it
    [SerializeField] private Color _goldColour = Color.yellow; //colour of the ticket prices when you can afford them 
    [SerializeField] private Color _redColour = Color.red; //colour of ticket prices when you cant afford them
    [SerializeField] private Color _defaultColour = Color.white; //default colour


    [SerializeField] private CursorScript _cursorScript; //ref to cursor script to change the cursor thats equipped
    [SerializeField] private Animator _ticketAnimator; //ref to animator that animates showing the tickets and when you spend some


    void Awake()// what to do on awake of script
    {
        Init(); //initialize values and text boxes
        Equipped(_currentCursorButtonNum); //equip starting cursor
    }
    private void Init() //initialize cursor buttons text and other variables 
    {
        _ticketText.text = _ticketAmount.ToString(); //make sure correct price is in the ticket textbox

        _cursorOwned = new List<bool>(); //make new list of the cursors owned (so its the same size as how many buttons you have)
        for (int i = 0; i < _cursorButtonObjs.Count; i++)//cycle all cursor buttons
        {
            //set if you own the cursors
            if (i == _currentCursorButtonNum) //own the default automatically
            {
                _cursorOwned.Add(true);
                continue;
            }
            _cursorOwned.Add(false); //set false elsewise
            if (i < _cursorPrices.Count)//check the price exits for the cursor
            {
                if (_cursorPrices[i] >= 0) //set cursors price if its above 0 or at 0 otherwise...
                {
                    _cursorButtonObjs[i].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = _cursorPrices[i].ToString();
                    Button b = _cursorButtonObjs[i].GetComponent<Button>(); //update cursor buttons colour so it looks different then the regular one
                    ColorBlock cb = b.colors;
                    cb.normalColor = _defaultColour; //set to default color
                    b.colors = cb;
                }
                else //otherwise make it so you already own that cursor
                {
                    Debug.Log("price was less then 0 so its now owned");
                    _cursorOwned[i] = true; //tell script you own default cursor
                }
            }
            else
            {

                Debug.Log("price doesnt exist(out of bounds)");
            }
        }
        UpdatePriceColor(); //update cursor price colours to either gold or red depending on if you can afford them 
    }
    public void Equipped(int m_buttonNum) //if you click a cursor, send number on cursor button
    {
        if (_cursorOwned[m_buttonNum]) //if you own this cursor change its equipped text && equip that cursor
        {
            _cursorButtonObjs[m_buttonNum].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = "eqipped";
            _cursorButtonObjs[m_buttonNum].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = Color.green;
            if (m_buttonNum != _currentCursorButtonNum) //make sure you dont reset button if it was the one you already have on
            {
                //reset last cursor
                _cursorButtonObjs[_currentCursorButtonNum].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = "";
                _cursorButtonObjs[_currentCursorButtonNum].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _defaultColour;

                _currentCursorButtonNum = m_buttonNum; //update current cursor num
                //CHANGE CURSOR!!
                _cursorScript.ChangeCursor(m_buttonNum);
                if (!_cursorJustBought)
                {
                    FindObjectOfType<SoundManager>().PlaySound("CursorEquipped");
                }
                else if (_cursorJustBought)
                {
                    FindObjectOfType<SoundManager>().PlaySound("CursorBought");
                }
                _cursorJustBought = false; //reset value
            }
            else
            {
                //IF I WANT SOUND FOR WHEN I TRY EQUIPPING CURSOR THATS ALREADY EQUIPPED
                Debug.Log("alreadyEquippedSound");
            }
        }
        else//if you dont own the cursor
        {
            Purchase(m_buttonNum); //try and buy it
        }
    }
    private void Purchase(int m_buttonNum) //called when trying to select cursor on shop screen and you dont own that cursor
    {
        if (_cursorPrices[m_buttonNum] <= _ticketAmount) //if you have enough tickets.. buy it
        {
            _ticketAmount -= _cursorPrices[m_buttonNum]; //remove cursor cost from total tickets
            _ticketText.text = _ticketAmount.ToString(); //update tickets text
            _cursorOwned[m_buttonNum] = true; //tell script you now own that cursor
            Button b = _cursorButtonObjs[m_buttonNum].GetComponent<Button>(); //update cursor button colour
            ColorBlock cb = b.colors;
            cb.normalColor = _purchasedColour; //set clear
            b.colors = cb;
            _ticketSpentText.text = _cursorPrices[m_buttonNum].ToString(); //update text that displays cost 
            _ticketAnimator.SetTrigger("TicketsSpent"); //animate cost text
            UpdatePriceColor(); //update cursor price colours incase now you cant afford any after spending those tickets
            _cursorJustBought = true;
            Equipped(m_buttonNum); //equip cursor you just bought
        }
        else //cant afford the cursor
        {
            Debug.Log("not enough money");
            FindObjectOfType<SoundManager>().PlaySound("CantAfford");
        }
    }
    private void UpdatePriceColor() //update cursor button price text to red if you cant afford it and gold if you can afford it
    {
        for (int i = 0; i < _cursorPrices.Count; i++)
        {
            if (!_cursorOwned[i] && _ticketAmount < _cursorPrices[i]) //if you dont own the cursor update price colours incase some you can no longer afford
            {
                _cursorButtonObjs[i].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _redColour;
            }
            else if (!_cursorOwned[i] && _ticketAmount >= _cursorPrices[i]) //if you can now afford it and dont own it
            {
                _cursorButtonObjs[i].transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _goldColour;
            }
        }
    }
    public void AddTickets(int m_addedTickets) //function to add tickets to total amount
    {
        _ticketAmount += m_addedTickets; //add amount of tickets passed to total
        if (_ticketAmount > 9999) //check its within its ticket limit (so it fits in the text box)
        {
            _ticketAmount = 9999;
            Debug.Log("At max ticket amount");
        }
        _ticketText.text = _ticketAmount.ToString(); //update text box to display tickets
        UpdatePriceColor(); //update cursor price colours incase now you can afford some


    }
}
