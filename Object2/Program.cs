namespace Object2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //if (args.Length == 3)
            //{

            //    int intValue = int.Parse(args[0]);
           //     double doubleValue1 = double.Parse(args[1]);
            //    double doubleValue2 = double.Parse(args[2]);

            //    Application.Run(new Object2(intValue, doubleValue1, doubleValue2));
           // }
            //else
            //{
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Object2(0, 0, 0));
            //}

                
        }
    }
}