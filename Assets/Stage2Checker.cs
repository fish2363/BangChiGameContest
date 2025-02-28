using UnityEngine;

public class Stage2Checker : MonoBehaviour
{
    public static Stage2Checker Instance;
    public bool isSavePoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void SavePoint()
    {
        isSavePoint = true;
    }

    public void SaveClear()
    {
        isSavePoint = false;
    }

    public bool GetSaveData()
    {
        return isSavePoint;
    }
}
