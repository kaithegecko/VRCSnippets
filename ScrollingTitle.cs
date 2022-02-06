using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace VRCSnippets
{
    //There are probably much better ways to do this but I don't really care
    private static string FullTitle = "[VRCSnippets] [Why do I even play this game]";
    private static string TrimmedTitle = string.Empty;

    [DllImport("user32.dll")]
    private static extern bool SetWindowText(IntPtr hwnd, string lpString);
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string className, string windowName);

    private static IntPtr windowPtr = FindWindow(null, "VRChat");

    internal static void Start(){
        new Thread((() =>
        {
            Timer timr = new Timer() {AutoReset = true, Enabled = true, Interval = 1 * 1000};
            timr.Elapsed += TimrOnElapsed;
            timr.Start();
        })){IsBackground = true, Name = "VRCSnippets-TitleThread"}.Start();
    }
    private static void TimrOnElapsed(object sender, ElapsedEventArgs e)
    {
        if (string.IsNullOrEmpty(CutTitle)) CutTitle = FullTitle;
        CutTitle = CutTitle.Remove(0, 1);
        SetTitles(CutTitle);
    }
}