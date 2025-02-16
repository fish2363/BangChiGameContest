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

    private bool isExecute = true;
    private bool isMoveTime;

    private string serchFile;

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

    public bool DuplicateCheck()
    {
        if (isExecute) return false;
        return isExecute;
    }

    public void GGMSite(string appName,string appDescript)
    {
        if (Answer(appDescript, appName) == 1)
        {
            Process.Start("https://ggm-h.goeay.kr/ggm-h/main.do");
            isExecute = false;
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
    public void Tip(string appName, string appDescript)
    {
        
    }

    public void Rewind(string appName, string appDescript)
    {

    }

    public void Setting(string appName, string appDescript)
    {

    }

    public void Mandle(string appName, string appDescript)
    {

    }
    private void Start()
    {
        FileInfo file = new FileInfo(@"C:\Users\김연호\Documents\마왕의 위치정보\item.txt");
        if (file.Exists)
        {
            File.Delete(@"C:\Users\김연호\Documents\마왕의 위치정보\item.txt");
        }
    }

    private void Update()
    {
        if (isMoveTime)
        {
            FileInfo file = new FileInfo(serchFile);
            if (file.Exists)  //해당 파일이 없으면 생성하고 파일 닫기
            {
                UnityEngine.Debug.Log("성공");
                isMoveTime = false;
            }
        }
    }

    public void SerchFile(string appName, string appDescript)
    {

        string npcFolder_path = CreateDirectory("마왕의 위치정보");
        string itemFolder_path = CreateDirectory("버려진 폴더");

        //string itemFolder_path = desktop_path + @"\버려진 폴더";
        //DirectoryInfo itemDirectoryInfo = new DirectoryInfo(itemFolder_path);
        //if (itemDirectoryInfo.Exists != true)
        //    itemDirectoryInfo.Create();

        string path = itemFolder_path + @"\item.txt";
        //var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        //writer.Close();

        FileInfo file = new FileInfo(path);
        if (!file.Exists)  //해당 파일이 없으면 생성하고 파일 닫기
        {
            FileStream fs = file.Create();
            fs.Close();
        }

        isMoveTime = true;
        SetSeachFile(npcFolder_path + @"\item.txt");
        //string file_name = "ScreenShot.png";
        //string source_path = Application.dataPath + @"\ScreenShot.png";
        //string target_path = itemFolder_path;

        //string source_file = Path.Combine(source_path, file_name);
        //string dest_file = Path.Combine(target_path, file_name);

        //File.Copy(source_file, dest_file, true);

        //System.IO.File.Move(source_file, dest_file);

        //ProcessStartInfo startInfo = new ProcessStartInfo(path)
        //{
        //    UseShellExecute = true
        //};
        //Process.Start(startInfo);

        UnityEngine.Debug.Log(npcFolder_path + @"\item.txt");
        Process.Start("explorer.exe", $"{npcFolder_path}");
        Process.Start("explorer.exe", $"{itemFolder_path}");
    }

    private void SetSeachFile(string ss)
    {
        serchFile = ss;
    }

    private string CreateDirectory(string directoryName)
    {
        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string folder_path = desktop_path + @"\"+directoryName;
        DirectoryInfo npcDirectoryInfo = new DirectoryInfo(folder_path);
        if (npcDirectoryInfo.Exists != true)
            npcDirectoryInfo.Create();

        return folder_path;
    }

    public void Password(string appName, string appDescript)
    {
        if(isfirewall)
        {
            MessageBox(GetWindowHandle(), appName, appDescript, (uint)(0x00000000L | 0x00000030L));
            return;
        }
    }

    public void Photo(string appName, string appDescript)
    {
        if (Answer(appDescript, appName) == 1)
        {
            string filePath = Application.dataPath + @"\ScreenShot.png";
            ProcessStartInfo startInfo = new ProcessStartInfo(filePath)
            {
                UseShellExecute = true
            };
            Process.Start(startInfo);

            string strPath = Application.dataPath + @"\ScreenShot.png";

            print(strPath);

            //Process.Start($"ms-photos:viewer?fileName={strPath}");
        }
        else
            return;
    }


}
