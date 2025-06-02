using UnityEngine;

public class DataController : MonoBehaviour
{
    [SerializeField] private ProgressData data;


    private void Start()
    {
        if (data != null)
        {
            string loadData = SaveData.Load("Data1.json");
            data = JsonUtility.FromJson<ProgressData>(loadData);
        }
    }
}
