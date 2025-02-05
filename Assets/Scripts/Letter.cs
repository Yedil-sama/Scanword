using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Letter : MonoBehaviour
{
    public char letter;
    public bool isRevealed = false;
    public TMP_Text text;
    public Image image;
    public Color defaultColor;
    public Color selectColor;
    public Color revealColor;
    public void Select()
    {
        image.color = selectColor;
    }
    public void Deselect()
    {
        image.color = defaultColor;
    }
    public void Reveal()
    {
        isRevealed = true;
        text.text = letter + "";
        image.color = revealColor;
    }
}
