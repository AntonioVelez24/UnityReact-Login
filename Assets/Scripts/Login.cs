using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;  

public class Login : MonoBehaviour
{
    public string url = "http://localhost/Cloud-Proyecto/php-api/login2.php";
    public InputField emailInputField;
    public InputField passwordInputField;
    public Button loginButton;

    [SerializeField] LoginData loginData;

    public TextMeshProUGUI mensajeTexto; 

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        mensajeTexto.text = ""; 
    }

    public void OnLoginButtonClicked()
    {
        loginData.email = emailInputField.text;
        loginData.password = passwordInputField.text;
        print(loginData.email + " " + loginData.password);
        mensajeTexto.text = "Intentando iniciar sesión...";
        StartCoroutine(TryLogin());
    }

    IEnumerator TryLogin()
    {
        string jsonData = JsonUtility.ToJson(loginData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            mensajeTexto.text = "Error de conexión: " + request.error;
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            if (request.downloadHandler.text.Contains("success"))
            {
                mensajeTexto.text = "Inicio de sesión exitoso!";

            }
            else
            {
                mensajeTexto.text = "Email o contraseña incorrectos.";
            }
        }
    }
}

[System.Serializable]
public class LoginData
{
    public string email;
    public string password;
}
