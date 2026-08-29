using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{   
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
