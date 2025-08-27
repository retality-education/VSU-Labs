using System;
using System.Windows.Forms;

namespace MusicSchool
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form1 = new Form1();
           
            var musicSchool = new MusicSchool();

            var musicSchoolControl = new MusicSchoolControl(musicSchool, form1);

            var musicSchoolController = new MusicSchoolController(musicSchool, musicSchoolControl);

            Application.Run(form1);
        }
    }
}
