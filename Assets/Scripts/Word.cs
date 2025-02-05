using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Word : MonoBehaviour
{
    public int id;
    public string word;
    public string description;
    public TMP_Text text;
    public Image image;
    public Color defaultColor;
    public Color selectColor;
    public Color revealColor;
    public Letter[] letters;
    public string guessingLetters;
    private void Awake()
    {
        image.color = defaultColor;
        if (description.Length >= 20)
        {
            text.enableAutoSizing = false;
            text.fontSize = 300 / description.Length;
        }
        text.text = description;
        if (PlayerPrefs.GetInt(word, 0) == 1)
        {
            RevealWord();
        }
        guessingLetters = GetLetters(word);


    }
    private void Start()
    {
    }
    public string GetLetters(string cl)
    {
        int numL = 14;// количество букв в итоговом слове
        string mainAlf = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string alf = mainAlf;

        //удаляем одинаковые буквы
        cl = string.Concat(cl.Distinct());

        //удаляем буквы слова из набора букв
        for (int i = 0; i < cl.Length; i++)
        {
            alf = alf.Replace(cl[i].ToString(), "");
        }

        //добавляем новые буквы
        for (int i = cl.Length; i < numL; i++)
        {
            string l = alf[Random.Range(0, alf.Length)].ToString();
            cl += l;
            alf = alf.Replace(l, "");
        }

        //сортируем по алфавиту
        //string result = string.Concat(cl.OrderBy(char.ToLower).ThenBy(char.IsLower));
        string result = "";

        for (int i = 0; i < mainAlf.Length; i++)
        {
            if (cl.IndexOf(mainAlf[i]) > -1)
            {
                result += mainAlf[i];
            }
        }
        return result;
    }
    public bool IsDone()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (!letters[i].isRevealed)
            {
                return false;
            }
        }
        return true;
    }
    public void SelectWord()
    {
        GameManager.Instance.SelectWord(this);
    }
    public void DeselectWord()
    {
        image.color = defaultColor;
        for(int i = 0;i < letters.Length; i++)
        {
            letters[i].Deselect();
        }
    }
    public void RevealWord()
    {
        
        image.color = revealColor;
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].letter = word[i];
            letters[i].Reveal();
        }
    }
    
}
