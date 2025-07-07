using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public InputField usernameInput;
    public InputField emailInput;
    public InputField passwordInput;
    public Button registerButton;
    public string url = "http://localhost/Cloud-Proyecto/php-api/register_user.php"; 

    public TextMeshProUGUI statusText; 

    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClicked);
        statusText.text = ""; 
    }

    public void OnRegisterClicked()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Por favor, completa todos los campos.";
            return;
        }

        RegistroData data = new RegistroData(username, email, password);
        StartCoroutine(SendRegisterRequest(data));
    }

    IEnumerator SendRegisterRequest(RegistroData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            statusText.text = "Error de conexión: " + request.error;
        }
        else
        {
            string response = request.downloadHandler.text;
            statusText.text = "Respuesta del servidor: " + response;

            if (response.Contains("OK"))
            {
                statusText.text = "Registro exitoso. Puedes iniciar sesión ahora.";
            }
            else
            {
                statusText.text = "Error en el registro: " + response;
            }
        }
    }

    [System.Serializable]
    public class RegistroData
    {
        public string username;
        public string email;
        public string password;

        public RegistroData(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}
