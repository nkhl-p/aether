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

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        // Todo Update the index after integration
        SceneManager.LoadScene(3);
    }
    
}
