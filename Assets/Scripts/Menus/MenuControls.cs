using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject panel;

    private InputAction escape;

    private void Awake()
    {
        escape = playerInput.actions.FindAction("Escape");
        escape?.Enable();
        escape.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext callbackContext)
    {
        panel.SetActive(!panel.activeSelf);
        Time.timeScale = panel.activeSelf?0f:1f;
    }

    public void UnPause()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        UnPause();
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnDestroy()
    {
        escape.performed -= Pause;
        escape?.Disable();
    }
}
