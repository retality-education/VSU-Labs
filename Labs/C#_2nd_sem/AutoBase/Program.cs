using AutoBase.Controller;
using AutoBase.Model;
using AutoBase.View;

namespace AutoBase
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #if DEBUG
                AllocConsole();
            #endif
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            AutoBaseForm autoBaseForm = new();
            AutoBaseModel autoBaseModel = new AutoBaseModel();
            AutoBaseController autoBaseController = new AutoBaseController(autoBaseModel, autoBaseForm);
            Application.Run(autoBaseForm);
        }
        // Импорт функции для создания консоли
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}