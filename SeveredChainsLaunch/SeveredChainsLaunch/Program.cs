
namespace SeveredChainsLaunch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Diagnostics.Process.Start("launch.bat");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Make sure this .exe is in the same folder as launch.bat", 
                                                     "File not found", 
                                                     System.Windows.Forms.MessageBoxButtons.OK, 
                                                     System.Windows.Forms.MessageBoxIcon.Error);
            }
            System.Environment.Exit(0);
        }
    }
}
