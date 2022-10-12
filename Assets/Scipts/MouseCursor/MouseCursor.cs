/*
Created By: Tyler McMillan
Description: This class holds the information about each mouse cursor
*/
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MouseCursor
{
    public string cursorName;
    public bool animated = false;
    public float secondPerFrame = 1f;
    public float cooldownTime = 0f;
    public List<Texture2D> cursorTextures;
    public Vector2 cursorClickLoc = Vector2.zero; //Where should you expect to click when clicking mouse
}
