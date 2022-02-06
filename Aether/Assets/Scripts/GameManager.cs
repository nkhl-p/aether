using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    public void PlayGame()
    {
        // Todo Update the index after integration
        SceneManager.LoadScene(0);
    }
    
    public void ResumeGame()
    {
        // Todo Update the index after integration
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        // Todo Update the index after integration
        SceneManager.LoadScene(4);
    }

    public void EndGame()
    {
        // Todo Update the index after integration
        SceneManager.LoadScene(3);
    }

    public void StartLevel1() {
        // Todo Update the index after integration
        SceneManager.LoadScene(1);
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
    }

}
