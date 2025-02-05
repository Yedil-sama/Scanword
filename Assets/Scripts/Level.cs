using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour
{
    public LevelSO level;
    public TMP_Text text;
    public Image backgrouondImage;
    public Color defaultColor;
    public Color inProcessColor;
    public Color finishedColor;
    public void Awake()
    {
        int saveState = PlayerPrefs.GetInt("Level" + level.Id, 0);
        text.text = "№ " + level.Id;
        if (saveState == 0)
        {
            backgrouondImage.color = defaultColor;
        }
        else if (saveState == 1) { 
            backgrouondImage.color = inProcessColor;
        }
        else
        {
            backgrouondImage.color = finishedColor;
        }
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(level.Id);
    }
}
