using System.Runtime.InteropServices;

internal class Program
{
    internal delegate bool ConsoleCtrlHandlerDelegate(int sig);
    [DllImport("Kernel32")]
    internal static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandlerDelegate handler, bool add);
    internal static ConsoleCtrlHandlerDelegate _consoleCtrlHandler;
}