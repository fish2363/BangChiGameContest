using UnityEngine;

public class StageChecker : MonoBehaviour
{
    public static StageChecker Instance;
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