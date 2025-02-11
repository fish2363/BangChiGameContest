using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowSceneLogic : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] //내부적으로 uint로 정리되어 있어서 무조건 써야함
    private static extern int MessageBox(IntPtr hwnd, string lpText, string lpCaption, uint flags);

    private static extern IntPtr GetActiveWindow();

    private static IntPtr windowHandle;

    public static IntPtr GetWindowHandle()
    {
        if (windowHandle == null)
        {
            windowHandle = GetActiveWindow();
        }
        return windowHandle;
    }

    private int Answer(string appName, string appDescript)
    {
        int answer;
        answer = MessageBox(GetWindowHandle(), appName, appDescript, (uint)(0x00000001L | 0x00000030L));
        return answer;
    }

    public void GGMSite(string appName,string appDescript)
    {
        
        if (Answer(appDescript, appName) == 1)
        {
            Process.Start("https://ggm-h.goeay.kr/ggm-h/main.do");
        }
        else
            return;
    }

    public void FishProfile(string appName, string appDescript)
    {
        if (Answer(appDescript, appName) == 1)
        {
            Process.Start("https://ggm.gondr.net/user/profile/352");
        }
        else
            return;
    }
    public void LCMProfile(string appName, string appDescript)
    {
        if (Answer(appDescript, appName) == 1)
        {
            Process.Start("https://ggm.gondr.net/user/profile/327");
        }
        else
            return;
    }
}
