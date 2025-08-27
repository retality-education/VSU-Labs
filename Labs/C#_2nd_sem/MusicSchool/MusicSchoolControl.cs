using MusicSchool.Students;
using MusicSchool.Teachers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicSchool
{
    public partial class MusicSchoolControl : UserControl, IObserver
    {

        public Form1 form;
        private MusicSchool _musicSchool;
        private Dictionary<Student, StudentControl> _studentControls;

        

        public MusicSchoolControl(MusicSchool musicSchool, Form1 form)
        {
            this.form = form;
            _musicSchool = musicSchool;
            _studentControls = new Dictionary<Student, StudentControl>();
            InitializeComponent();
            AddTeacherImages();
            AddClassroomImages();
            InitializeStudentControls();
        }
       

        private void InitializeStudentControls()
        {
            foreach (var student in _musicSchool.Students)
            {
                StudentControl studentControl = new StudentControl(student);
                studentControl.Location = new Point(100, 100 + (_studentControls.Count * 100));
                form.Controls.Add(studentControl);
                _studentControls.Add(student, studentControl);
                studentControl.BringToFront();
            }
        }

        private void AddClassroomImages()
        {
            PictureBox classroom1Image = new PictureBox();
            classroom1Image.Image = Properties.Resources.музыкальный_класс;
            classroom1Image.Location = _musicSchool.Classrooms[0].Location;
            classroom1Image.Size = new Size(600, 250);
            form.Controls.Add(classroom1Image);

            PictureBox classroom2Image = new PictureBox();
            classroom2Image.Image = Properties.Resources.музыкальный_класс_2;
            classroom2Image.Location = _musicSchool.Classrooms[1].Location;
            classroom2Image.Size = new Size(600, 300);
            form.Controls.Add(classroom2Image);

            PictureBox classroom3Image = new PictureBox();
            classroom3Image.Image = Properties.Resources.музыкальный_класс_3;
            classroom3Image.Location = _musicSchool.Classrooms[2].Location;
            classroom3Image.Size = new Size(600, 300);
            form.Controls.Add(classroom3Image);
        }

        private void AddTeacherImages()
        {

            PictureBox teacher1Image = new PictureBox();
            teacher1Image.Image = Properties.Resources.учитель_2;
            teacher1Image.Location = new Point(360, 400);
            teacher1Image.Size = new Size(100, 120);
            form.Controls.Add(teacher1Image);
            teacher1Image.BackColor = Color.Transparent;

            PictureBox teacher2Image = new PictureBox();
            teacher2Image.Image = Properties.Resources.учитель;
            teacher2Image.Location = new Point(310, 130);
            teacher2Image.Size = new Size(100, 120);
            form.Controls.Add(teacher2Image);
            teacher2Image.BackColor = Color.Transparent;
        }

        public async Task MoveStudent(Student student)
        {
            var studentControl = _studentControls[student];
            await MoveStudentAsync(student, studentControl);
        }

        private async Task MoveStudentAsync(Student student, StudentControl studentControl)
        {
            for (int step = 0; step < student.PathToPurpose.Length - 1; step++)
            {
                Point startPoint = student.PathToPurpose[step];
                Point endPoint = student.PathToPurpose[step + 1];

                for (double t = 0.00; t < 1.0; t += 0.05)
                {
                    int newX = (int)(startPoint.X * (1 - t) + endPoint.X * t);
                    int newY = (int)(startPoint.Y * (1 - t) + endPoint.Y * t);

                    Invoke((Action)(() =>
                    {
                        studentControl.Location = new Point(newX, newY);
                        studentControl.Refresh();
                    }));

                    await Task.Delay(170);
                }
                await Task.Delay(800);

                Invoke((Action)(() =>
                {
                    studentControl.Invalidate();
                }));
            }
        }
    }
}


