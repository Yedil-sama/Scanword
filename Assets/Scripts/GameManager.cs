using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private GameObject guessingLetterPrefab;
    public GameObject guessingWordContainer;
    public GameObject guessingLettersContainer;
    public List<GuessingLetter> guessingWordRandomLetters;
    public List<GuessingLetter> guessingWordLetters;
    public TMP_Text descriptionText;
    public TMP_InputField inputfield;
    public int selectedWord = 0;
    public int guestWords = 0;
    public Word[] words;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        winCanvas.enabled = false;
        SelectWord(words[0]);
    }
    private void Update()
    {
        
    }
    private bool CheckIsWin()
    {
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].letters.Length; j++)
            {
                if (!words[i].letters[j].isRevealed)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void OpenWinCanvas(bool action)
    {
        winCanvas.enabled = action;
    }
    public void MenuButton()
    {
        sceneChanger.ChangeScene(0);
    }
    public void NextLevelButton()
    {
        sceneChanger.NextLevel();
    }
    public void SelectNextWord()
    {
        for (int i = selectedWord; i < words.Length; i++) 
        {
            if (!words[i].IsDone())
            {
                words[(selectedWord = i)].SelectWord();
                return;
            }
        }
        
    }
    public int GetPosition()
    {
        for (int i = 0; i < guessingWordLetters.Count; i++)
        {
            if (!guessingWordLetters[i].isRevealed)
            {
                return i;
            }
        }
        return -1;
    }

    public void DropLetter(GuessingLetter letter)
    {
        if (letter.pickedId == -1) return;
        guessingWordLetters[letter.pickedId].image.enabled = false;
        guessingWordLetters[letter.pickedId].image.color = guessingWordLetters[letter.pickedId].defaultColor;
        guessingWordLetters[letter.pickedId].text.enabled = false;
        guessingWordLetters[letter.pickedId].text.text = "";
        guessingWordLetters[letter.pickedId].letter = ' ';
        guessingWordLetters[letter.pickedId].isRevealed = false;
        guessingWordLetters[letter.pickedId].pickedId = -1;
        //Debug.Log("Dropped");

    }
    public void PickLetter(GuessingLetter letter)
    {
        int id = GetPosition();
        if (id != -1)
        {
            
            guessingWordLetters[id].image.enabled = true;
            guessingWordLetters[id].image.color = guessingWordLetters[id].selectColor;
            guessingWordLetters[id].text.enabled= true;
            guessingWordLetters[id].text.text = letter.letter + "";
            guessingWordLetters[id].letter = letter.letter;
            guessingWordLetters[id].isRevealed = true;
            guessingWordLetters[id].pickedId = id;
            //Debug.Log("Picked");
        }
        Check();
    }
    public void Win()
    {
        PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels", 0) + 1);
        PlayerPrefs.SetInt("Words", PlayerPrefs.GetInt("Words", 0) + words.Length);
        winCanvas.enabled = true;
        PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 2);
        PlayerPrefs.Save();
        //Debug.Log("Won");
    }
    public void CheckButton()
    {
        if(inputfield.text.ToUpper() == words[selectedWord].word.ToUpper())
        {
            words[selectedWord].RevealWord();
            PlayerPrefs.SetInt(words[selectedWord].word, 1);
            if (!words[selectedWord].IsDone())
            {
                if (++guestWords >= words.Length)
                {
                    PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 2);
                }
            }
            SelectNextWord();
            PlayerPrefs.Save();
        }
        if (CheckIsWin())
        {
            Win();
        }
        else
        {
            PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 1);
            PlayerPrefs.Save();
        }
        inputfield.text = "";
    }
    public string GetCurrentText()
    {
        string result = "";
        for (int i = 0; i < guessingWordLetters.Count; i++)
        {
            result += guessingWordLetters[i].GetComponent<GuessingLetter>().letter;
        }
        //Debug.Log(result);

        return result;
    }
    public void Check()
    {
        if (GetCurrentText().ToUpper() == words[selectedWord].word.ToUpper())
        {
            words[selectedWord].RevealWord();
            PlayerPrefs.SetInt(words[selectedWord].word, 1);
            if (!words[selectedWord].IsDone())
            {
                if (++guestWords >= words.Length)
                {
                    PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 2);
                }
            }
            SelectNextWord();
            PlayerPrefs.Save();
        }
        if (CheckIsWin())
        {
            Win();
        }
        else
        {
            PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 1);
            PlayerPrefs.Save();
        }
        inputfield.text = "";
    }
    
    public void DeselectWord()
    {
        words[selectedWord].DeselectWord();
        guessingWordLetters.Clear();
        for (int i = 0; i < guessingLettersContainer.transform.childCount; i++)
        {
            Destroy(guessingLettersContainer.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<guessingWordContainer.transform.childCount; i++)
        {
            Destroy(guessingWordContainer.transform.GetChild(i).gameObject);
        }
    }

    public void SelectWord(Word word)
    {
        DeselectWord();

        word.image.color = word.selectColor;
        descriptionText.text = word.description;
        for (int i = 0; i < word.letters.Length; i++)
        {
            word.letters[i].Select();
        }
        selectedWord = word.id;


        for(int i=0;i < word.guessingLetters.Length; i++)
        {
            GameObject guessingLetter = Instantiate(guessingLetterPrefab);
            guessingLetter.GetComponent<GuessingLetter>().letter = word.guessingLetters[i];
            guessingLetter.transform.SetParent(guessingLettersContainer.transform);
            guessingLetter.GetComponent<GuessingLetter>().text.text = guessingLetter.GetComponent<GuessingLetter>().letter + "";
            guessingLetter.GetComponent<GuessingLetter>().isAnswer = false;
            guessingLetter.GetComponent<GuessingLetter>().isRevealed = false;
            guessingWordRandomLetters.Add(guessingLetter.GetComponent<GuessingLetter>());
            //Debug.Log("Random Letters: "+word.guessingLetters);
        }
        for(int i = 0; i < word.word.Length; i++)
        {
            GameObject guessingWordLetter = Instantiate(guessingLetterPrefab);
            guessingWordLetter.transform.SetParent(guessingWordContainer.transform);
            guessingWordLetter.GetComponent<GuessingLetter>().isAnswer=true;
            guessingWordLetter.GetComponent<GuessingLetter>().isRevealed = false;
            guessingWordLetter.GetComponent<GuessingLetter>().letterRef = word.letters[i];
            guessingWordLetters.Add(guessingWordLetter.GetComponent<GuessingLetter>());
            //Debug.Log("Answer Letters: " + word.guessingLetters);

        }
        
    }
}
