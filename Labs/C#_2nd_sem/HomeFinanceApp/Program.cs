using HomeFinanceApp.Controllers;
using HomeFinanceApp.Models;
using HomeFinanceApp.Views;
using HomeFinanceApp.Views.Forms;
using System.Runtime.InteropServices;

namespace HomeFinanceApp
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
            
            var family = new Family();
            var financeForm = new FinanceForm();
            var financeController = new FinanceController(family, financeForm);

            Application.Run(financeForm);

        }

    }
}