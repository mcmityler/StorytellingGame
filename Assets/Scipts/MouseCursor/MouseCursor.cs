/*
Created By: Tyler McMillan
Description: This class holds the information about each mouse cursor
*/
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class MouseCursor
{
    public string cursorName; //what is the cursor called
    public bool animated = false; //is this an animated cursor
    public float secondPerFrame = 1f; //how long should each frame of the animation last
    public float cooldownTime = 0f; //should there be a cool down between animations
    public List<Texture2D> cursorTextures; //what are the sprites/textures of the cursor
    public Vector2 cursorClickLoc = Vector2.zero; //Where should you expect to click when clicking mouse
    public int CursorNum; //what number cursor this is to match button... (might not need after optimizing)
    public Button cursorButton; //what cursor button represents this cursor
    public int cursorCost = -10; //how much does this cursor cost (negatives means it is free)
    public bool cursorOwned = false;
    public bool cursorLocked = false;
}
