using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowBGChange : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;

    private void Start()
    {
        try
        {
            rawImage.texture = BGManager.Instance.GetPCWallpaper();
        }
        catch(Exception e)
        {
            print("�� �ҷ����� ���ȭ����;;");
            print($"Error: {e.Message}");
        }
    }
}
