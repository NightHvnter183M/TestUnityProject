using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private string FirstScene;
    public void OnClick()
    {
        SceneManager.LoadScene(FirstScene);
    }
}
