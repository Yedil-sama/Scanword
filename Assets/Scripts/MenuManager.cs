using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Space]
    [Header("GamgeObjects")]
    [SerializeField] private GameObject levelContainer;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private LevelSO[] levels;

    [Space]
    [SerializeField] private Canvas globalCanvas;
    [SerializeField] private Toggle statisticsToggle;
    [SerializeField] private Toggle levelToggle;

    [Space]
    [Header("LevelCanvas")]
    [SerializeField] private Canvas levelCanvas;

    [SerializeField] private Canvas menuCanvas;

    [Space]
    [Header("ConfirmCanvas")]
    [SerializeField] private Canvas confirmCanvas;
    [SerializeField] private TMP_Text confirmText;

    [Space]
    [Header("StatisticsCanvas")]
    [SerializeField] private Canvas statisticsCanvas;
    [SerializeField] private TMP_Text finishedScanwordsText;
    [SerializeField] private TMP_Text finishedWordsText;
    [SerializeField] private TMP_Text totalScanwordsText;
    [SerializeField] private TMP_Text totalWordsText;
    

    private void Start()
    {
        globalCanvas.enabled = true;
        levelCanvas.enabled = true;
        menuCanvas.enabled = false;
        confirmCanvas.enabled = false;
        statisticsCanvas.enabled = false;

        finishedScanwordsText.text = PlayerPrefs.GetInt("Levels", 0) + "";
        finishedWordsText.text = PlayerPrefs.GetInt("Words", 0) + "";

        totalScanwordsText.text = GetTotalScanWords() + "";
        totalWordsText.text = GetTotalWords() + "";
    }

    public void OpenLevelCanvas(bool action)
    {
        levelCanvas.enabled = action;
        statisticsCanvas.enabled = !action;
    }
    public void OpenMenuCanvas(bool action)
    {
        menuCanvas.enabled = action;
    }
    public void OpenStatisticsCanvas(bool action)
    {
        statisticsCanvas.enabled = action;
        levelCanvas.enabled = !action;
    }
    public void OpenConfirmCanvas(string text)
    {
        confirmText.text = text;
        confirmCanvas.enabled = !confirmCanvas.enabled;
    }
    public void ConfirmButton(bool action)
    {
        if (action)
        {
            DeleteData();   
        }
        confirmCanvas.enabled = false;
    }
    private void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        sceneChanger.ReloadScene();
    }
    private int GetTotalScanWords()
    {
        return levels.Length;
    }
    private int GetTotalWords()
    {
        int result = 0;
        for (int i = 0; i < levels.Length; i++) {
            result += levels[i].Words;
        }
        return result;
    }
}
