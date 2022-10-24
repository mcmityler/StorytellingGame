/*
Created By: Tyler McMillan
Description: This script deals with the shop, buying/equipping cursors and ticket management
*/
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private List<MouseCursor> _cursors; // List of all the cursors & details
    [SerializeField] private TMP_Text _ticketText; //ref to ticket text object to update number of tickets
    [SerializeField] private TMP_Text _ticketSpentText; //ref to ticket spent text object to set how many tickets you spent
    [SerializeField] private int _ticketAmount = 50; //how many tickets you have right now
    [SerializeField] private int _currentCursorButtonNum = 0; //what is the current cursor you are usings number (used to equipped cursor to keep track and make it easy to 'equip' and 'unequip' them)
    private bool _cursorJustBought = false; //was the cursor just purchased (for sounds in equipped function)
    [SerializeField] private Color _purchasedColour = Color.clear; //colour to set the cursor button when you purchase it
    [SerializeField] private Color _goldColour = Color.yellow; //colour of the ticket prices when you can afford them 
    [SerializeField] private Color _redColour = Color.red; //colour of ticket prices when you cant afford them
    [SerializeField] private Color _defaultColour = Color.white; //default colour
    [SerializeField] private Color _lockedColor = Color.black; //default colour
    [SerializeField] private Texture2D _lockedButton;



    [SerializeField] private CursorScript _cursorScript; //ref to cursor script to change the cursor thats equipped
    [SerializeField] private Animator _ticketAnimator; //ref to animator that animates showing the tickets and when you spend some


    void Awake()// what to do on awake of script
    {
        Init(); //initialize values and text boxes
        Equipped(_currentCursorButtonNum); //equip starting cursor
    }
    private void Init() //initialize cursor buttons text and other variables 
    {
        _ticketText.text = _ticketAmount.ToString(); //put ticket amount in ticket display textbox
        for (int i = 0; i < _cursors.Count; i++)//cycle all cursor buttons
        {
            if (_cursors[i].cursorLocked)
            {
                LockCursor(i);
            }
            else if (_cursors[i].cursorCost >= 0) //set cursors price if its above 0 or at 0 otherwise...
            {
                _cursors[i].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = _cursors[i].cursorCost.ToString();
                Button b = _cursors[i].cursorButton; //update cursor buttons colour so it looks different then the regular one
                ColorBlock cb = b.colors;
                cb.normalColor = _defaultColour; //set to default color
                b.colors = cb;
            }
            else //otherwise make it so you already own that cursor
            {
                Debug.Log("price was less then 0 so its now owned");
                _cursors[i].cursorOwned = true; //tell script you own default cursor
            }

        }
        UpdatePriceColor(); //update cursor price colours to either gold or red depending on if you can afford them 
    }

    // ------------------------------------------------------------------ CURSOR SHOP FUNCTIONS--------------------------------------------------------------------------------
    public void Equipped(int m_buttonNum) //if you click a cursor, send number on cursor button
    {
        if (_cursors[m_buttonNum].cursorOwned) //if you own this cursor change its equipped text && equip that cursor
        {
            _cursors[m_buttonNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = "eqipped";
            _cursors[m_buttonNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = Color.green;
            if (m_buttonNum != _currentCursorButtonNum) //make sure you dont reset button if it was the one you already have on
            {
                //reset last cursor
                _cursors[_currentCursorButtonNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = "";
                _cursors[_currentCursorButtonNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _defaultColour;

                _currentCursorButtonNum = m_buttonNum; //update current cursor num
                //CHANGE CURSOR!!
                _cursorScript.ChangeCursor(_cursors[_currentCursorButtonNum]);
                if (!_cursorJustBought) //play equip sound if not just bought
                {
                    FindObjectOfType<SoundManager>().PlaySound("CursorEquipped");
                }
                else if (_cursorJustBought) //play just bought sound if you just purchased this
                {
                    FindObjectOfType<SoundManager>().PlaySound("CursorBought");
                }
                _cursorJustBought = false; //reset just bought value
            }
            else
            {
                //IF I WANT SOUND FOR WHEN I TRY EQUIPPING CURSOR THATS ALREADY EQUIPPED
                Debug.Log("alreadyEquippedSound");
            }
        }
        else if (_cursors[m_buttonNum].cursorLocked)
        {
            FindObjectOfType<SoundManager>().PlaySound("Locked");
        }
        else//if you dont own the cursor
        {
            Purchase(m_buttonNum); //try and buy it
        }
    }
    private void Purchase(int m_buttonNum) //called when trying to select cursor on shop screen and you dont own that cursor
    {
        if (_cursors[m_buttonNum].cursorCost <= _ticketAmount) //if you have enough tickets.. buy it
        {
            _ticketAmount -= _cursors[m_buttonNum].cursorCost; //remove cursor cost from total tickets
            _ticketText.text = _ticketAmount.ToString(); //update tickets text
            _cursors[m_buttonNum].cursorOwned = true; //tell script you now own that cursor
            Button b = _cursors[m_buttonNum].cursorButton; //update cursor button colour
            ColorBlock cb = b.colors;
            cb.normalColor = _purchasedColour; //set clear
            b.colors = cb;
            _ticketSpentText.text = _cursors[m_buttonNum].cursorCost.ToString(); //update text that displays cost 
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
        for (int i = 0; i < _cursors.Count; i++)
        {
            if (!_cursors[i].cursorOwned && _ticketAmount < _cursors[i].cursorCost || _cursors[i].cursorLocked) //if you dont own the cursor update price colours incase some you can no longer afford
            {
                _cursors[i].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _redColour;
            }
            else if (!_cursors[i].cursorOwned && _ticketAmount >= _cursors[i].cursorCost) //if you can now afford it and dont own it
            {
                _cursors[i].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().color = _goldColour;
            }
        }
    }
    public void LockCursor(int m_cursorNum)
    {
        _cursors[m_cursorNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = "locked";
        _cursors[m_cursorNum].cursorButton.transform.Find("LockedImage").gameObject.SetActive(true);
        _cursors[m_cursorNum].cursorButton.GetComponent<TooltipTrigger>().SetToolTipEnabled(false);
        _cursors[m_cursorNum].cursorLocked = true;

        Button b = _cursors[m_cursorNum].cursorButton; //update cursor buttons colour so it looks different then the regular one
        ColorBlock cb = b.colors;
        cb.normalColor = _defaultColour; //set to default color
        b.colors = cb;
        UpdatePriceColor();
    }
    public void UnlockCursor(int m_cursorNum)
    {
        if (_cursors[m_cursorNum].cursorLocked)
        {
            _cursors[m_cursorNum].cursorButton.transform.Find("PriceText (TMP)").GetComponent<TMP_Text>().text = _cursors[m_cursorNum].cursorCost.ToString();

            _cursors[m_cursorNum].cursorButton.transform.Find("LockedImage").gameObject.SetActive(false);
            _cursors[m_cursorNum].cursorButton.GetComponent<TooltipTrigger>().SetToolTipEnabled(true);
            _cursors[m_cursorNum].cursorLocked = false;
            Button b = _cursors[m_cursorNum].cursorButton; //update cursor buttons colour so it looks different then the regular one
            ColorBlock cb = b.colors;
            cb.normalColor = _defaultColour; //set to default color
            b.colors = cb;

            UpdatePriceColor();
        }
    }



    // -------------------------------------------------------------------------------- TICKET FUNCTIONS------------------------------------------------------------------------------------------------
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
