using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

namespace SeveredChainsLaunch
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string lpFileName);

        private const int SW_HIDE = 0;

        private const string BAT_NAME = "launch.bat";

        private static void Main(string[] args)
        {
            try
            {
                IntPtr handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }
            catch { }

            try
            {
                UnblockFile(BAT_NAME);
                Process.Start(BAT_NAME);
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 2)
            {
                string appName = Process.GetCurrentProcess().ProcessName;
                MessageBox.Show($"'{appName}.exe' needs to be in the same directory as '{BAT_NAME}'",
                                $"File not found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                //Display nothing because the user manually canceled another dialog box
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unknown error occured:\n{ex.StackTrace}",
                                $"Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        private static bool UnblockFile(string filePath)
        {
            string zoneStreamPath = $"{filePath}:Zone.Identifier";
            return DeleteFile(zoneStreamPath); // Returns true if the file was blocked
        }
    }
}