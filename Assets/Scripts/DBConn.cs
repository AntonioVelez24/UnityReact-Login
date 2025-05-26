using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DBConn : MonoBehaviour
{
    [SerializeField] private string url = "http://localhost/prueba.php";
    [SerializeField] private Data userdata;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        string jsonData = JsonUtility.ToJson(userdata);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
        // Crear datos JSON

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
          request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
        }
    }
}
[System.Serializable]
public class Data
{
    public string username;
    public string email;
    public string password;
}