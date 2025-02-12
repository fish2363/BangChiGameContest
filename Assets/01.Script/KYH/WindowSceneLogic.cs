using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowSceneLogic : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] //내부적으로 uint로 정리되어 있어서 무조건 써야함
    private static extern int MessageBox(IntPtr hwnd, string lpText, string lpCaption, uint flags);

    private static extern IntPtr GetActiveWindow();

    private static IntPtr windowHandle;
    private bool isfirewall = true;

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
    public void Firewall(string appName, string appDescript)
    {
        if(!isfirewall)
        {
            if (Answer("방화벽을 켜시겠습니까?", "보안 시스템") == 1)
            {
                isfirewall = true;
            }
        }
        else
        {
            if (Answer(appDescript, appName) == 1)
            {
                isfirewall = false;
            }
        }
    }

    public void Password(string appName, string appDescript)
    {
        if(isfirewall)
        {
            MessageBox(GetWindowHandle(), appName, appDescript, (uint)(0x00000000L | 0x00000030L));
            return;
        }
        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        string saveFolder_path = desktop_path + "//Important Folders";
        DirectoryInfo directoryInfo = new DirectoryInfo(saveFolder_path);
        if (directoryInfo.Exists != true)
        {
            directoryInfo.Create();
        }
        string path = saveFolder_path + @"\Command.txt";
        print(path + "에다가 만들기 성공");
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.WriteLine(Environment.UserName);
        writer.Close();

        ProcessStartInfo startInfo = new ProcessStartInfo(path)
        {
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }

    public void Photo(string appName, string appDescript)
    {
        if (Answer(appDescript, appName) == 1)
        {
            string filePath = Application.dataPath + "/ScreenShot.png";
            ProcessStartInfo startInfo = new ProcessStartInfo(filePath)
            {
                UseShellExecute = true
            };
            Process.Start(startInfo);

            string strPath = Application.dataPath + "/ScreenShot.png";

            print(strPath);

            //Process.Start($"ms-photos:viewer?fileName={strPath}");
        }
        else
            return;
    }
}
