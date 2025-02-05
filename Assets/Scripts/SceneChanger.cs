using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            ChangeScene(0);
        }
        else
        {
            ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        PlayerPrefs.Save();
    }
}
