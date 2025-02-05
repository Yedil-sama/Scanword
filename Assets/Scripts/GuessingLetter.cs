using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessingLetter : Letter
{
    public bool isAnswer = true;
    public int pickedId = -1;
    public Image underlineImage;
    public Letter letterRef;
    private void Start()
    {
        if (isAnswer && letterRef != null && letterRef.isRevealed)
        {
            isRevealed = true;
            text.enabled = true;
            image.enabled = true;
            image.color = revealColor;
            text.text =  letterRef.letter + "";
            letter = letterRef.letter;
        }
        else if (isAnswer)
        {
            isRevealed = false;
            text.enabled = false;
            image.enabled = false;
            underlineImage.enabled = true;
            image.color = selectColor;
        }
        else
        {
            isRevealed = false;
            text.enabled = true;
            image.enabled = true;
            underlineImage.enabled = false;
            image.color = defaultColor;
        }
    }
    public void Pick()
    {
        if (!isRevealed)
        {
            GameManager.Instance.PickLetter(this);
        }
        else
        {
            GameManager.Instance.DropLetter(this);
        }
    }
}
