using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using LandscapeDesign.Controller;
using LandscapeDesign.Models;
using LandscapeDesign.View;

namespace LandscapeDesign
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            // Инициализация консоли для отладки
            AllocConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            City city = new City();
            FormView formView = new FormView();
            LandscapeController landscapeController = new LandscapeController(city, formView);
            Application.Run(formView);
        }
    }
}