namespace maze_form
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}