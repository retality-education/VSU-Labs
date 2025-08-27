using Production.Controller;
using Production.Models;
using Production.View;

namespace Production
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var facility = new ProductionFacility();
            var view = new MainForm();
            var controller = new ProductionController(facility, view);
 
            Application.Run(view);
        }
     

    }
}