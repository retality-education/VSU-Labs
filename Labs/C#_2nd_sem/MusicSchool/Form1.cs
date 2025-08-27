using System;
using System.Windows.Forms;

namespace MusicSchool
{
    public partial class Form1 : Form
    {
        public MusicSchoolController Controller;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Controller.StartLessons();
        }
    }
}
