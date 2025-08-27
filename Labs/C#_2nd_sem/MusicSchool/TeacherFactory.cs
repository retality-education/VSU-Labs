using System;
using System.Collections.Generic;
using System.Drawing;



namespace MusicSchool
{
    public class TeacherFactory : ITeacherFactory
    {
        private readonly Random _random;

        private readonly List<string> _teachersNames;

        public TeacherFactory()
        {
            _random = new Random(DateTime.Now.Millisecond);

            _teachersNames = new List<string> { "Учитель 1", "Учитель 2", "Учитель 3", "Учитель 4" };
        }

        public Teacher CreateTeacher(int type)
        {
            Teacher teacher;

            Subject subject = (Subject)_random.Next(Enum.GetValues(typeof(Subject)).Length);
            Direction direction = (Direction)_random.Next(Enum.GetValues(typeof(Direction)).Length);

            if (type == 0)
            {
                teacher = new InstructorTeacher(_teachersNames[_random.Next(_teachersNames.Count)], _random.Next(25, 60),
                    subject, direction, _random.Next(1, 10),
                    new Point[] { new Point(50, 50), new Point(100, 150), new Point(200, 50), new Point(50, 50) });
            }
            else
            {
                teacher = new ProfessorTeacher(_teachersNames[_random.Next(_teachersNames.Count)], _random.Next(50, 90),
                    subject, direction, 2 * _random.Next(1, 10),
                    new Point[] { new Point(50, 50), new Point(100, 150), new Point(200, 50), new Point(50, 50) });
            }

            return teacher;
        }
    }
}

