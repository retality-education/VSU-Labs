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
    public partial class StudentControl : UserControl
    {

        // Свойство для хранения связанного студента
        public Student AssociatedStudent { get; private set; }

        // Конструктор, который принимает объект Student
        public StudentControl(Student student)
        {
            AssociatedStudent = student;
            InitializeControl();
        }

        // Метод для инициализации контрола
        private void InitializeControl()
        {
            // Установка размера контрола
            this.Size = new Size(140, 160);
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Установка изображения студента в зависимости от его направления
            switch (AssociatedStudent.Direction)
            {
                case Direction.Classical:
                    this.BackgroundImage = Properties.Resources.ученица;
                    break;
                case Direction.Jazz:
                    this.BackgroundImage = Properties.Resources.ученик; 
                    break;
                case Direction.Pop:
                    this.BackgroundImage = Properties.Resources.ученица2; 
                    break;
                case Direction.Rock:
                    this.BackgroundImage = Properties.Resources.ученик2; 
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.ученик; 
                    break;
            }
        }

        // Переопределение метода OnPaint для отрисовки информации о студенте
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Отрисовка имени и уровня знаний студента
            string info = $"{AssociatedStudent.Name}\nУровень: {AssociatedStudent.KnowledgeLevel}";
            using (Font font = new Font("Arial", 10))
            {
                SizeF textSize = g.MeasureString(info, font);
                PointF textLocation = new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
                g.DrawString(info, font, Brushes.Black, textLocation);
            }
        }

    }
}


