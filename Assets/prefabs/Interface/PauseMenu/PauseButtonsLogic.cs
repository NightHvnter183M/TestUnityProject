using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonsLogic : MonoBehaviour
{
    [SerializeField] private GamePause gamePause;
    
    public void OnResume()
    {
        gamePause.Resume();
        gameObject.SetActive(false);
    }
    public void onMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
