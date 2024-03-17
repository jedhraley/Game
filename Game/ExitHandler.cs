using System.Runtime.InteropServices;

class Program
{
    public delegate bool ConsoleCtrlHandlerDelegate(int sig);
    [DllImport("Kernel32")]
    public static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandlerDelegate handler, bool add);
    public static ConsoleCtrlHandlerDelegate _consoleCtrlHandler;
}