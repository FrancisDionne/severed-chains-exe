namespace SeveredChainsLaunch
{
    internal class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern System.IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;

        private const string BAT_NAME = "launch.bat";

        static void Main(string[] args)
        {
            try
            {
                System.IntPtr handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
            }
            catch { }

            try
            {
                System.Diagnostics.Process.Start(BAT_NAME);
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 2)
            {
                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                System.Windows.Forms.MessageBox.Show($"'{appName}.exe' needs to be in the same directory as '{BAT_NAME}'",
                                                     "File not found",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                //Display nothing because the user manually canceled another dialog box
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"An unknown error occured:\n{ex.StackTrace}",
                                                     "Error",
                                                     System.Windows.Forms.MessageBoxButtons.OK,
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }

            System.Environment.Exit(0);
        }
    }
}