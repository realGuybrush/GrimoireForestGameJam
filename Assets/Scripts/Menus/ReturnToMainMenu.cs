using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
