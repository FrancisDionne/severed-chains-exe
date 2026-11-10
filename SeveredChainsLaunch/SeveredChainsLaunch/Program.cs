namespace SeveredChainsLaunch
{
    internal class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern System.IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;

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
                System.Diagnostics.Process.Start("launch.bat");
            }
            catch
            {
                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                System.Windows.Forms.MessageBox.Show($"'{appName}.exe' needs to be in the same directory as 'launch.bat'", 
                                                     "File not found", 
                                                     System.Windows.Forms.MessageBoxButtons.OK, 
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }
            System.Environment.Exit(0);
        }
    }
}