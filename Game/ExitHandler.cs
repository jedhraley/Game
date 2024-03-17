
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

class Program
{
    public delegate bool ConsoleCtrlHandlerDelegate(int sig);
    [DllImport("Kernel32")]
    public static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandlerDelegate handler, bool add);
    public static ConsoleCtrlHandlerDelegate _consoleCtrlHandler;
}