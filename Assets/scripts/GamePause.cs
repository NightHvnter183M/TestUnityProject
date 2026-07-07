using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GamePause : MonoBehaviour
{
    [SerializeField] public bool isPaused;
    [SerializeField] private GameObject pauseMenuCanvas;
    
    private void Start() 
    {
        Resume();
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseMenuCanvas != null) 
            pauseMenuCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseMenuCanvas != null) 
            pauseMenuCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
