using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GoToLogin()
    {
        ChangeScene("Login");
    }
    public void GoToRegister()
    {
        ChangeScene("RegisterUser");
    }
}
