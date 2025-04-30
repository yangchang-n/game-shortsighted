using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    private const string stage01SceneName = "Stage01";

    public void StartGame()
    {
        SceneManager.LoadScene(stage01SceneName);
    }
}
