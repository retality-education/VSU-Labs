using System;
using System.Collections.Generic;
using System.Drawing;


namespace MusicSchool.Students
{
    class StudentFactory : IStudentFactory
    {
        private readonly Random _random;

        private readonly List<string> _studentNames;

        public StudentFactory()
        {
            _random = new Random();

            _studentNames = new List<string> { "Ученик 1", "Ученик 2", "Ученик 3", "Ученик 4", "Ученик 5" };
        }

        public Student CreateStudent(int type)
        {
            List<Subject> subjects = new List<Subject>();
            for (int i = 0; i < 2; i++)
            {
                subjects.Add((Subject)_random.Next(
                    Enum.GetValues(typeof(Subject)).Length)
                    );
            }

            Direction direction = (Direction)_random.Next(Enum.GetValues(typeof(Direction)).Length);

            if (type == 0)
            {
                return new AdvancedStudent(_studentNames[_random.Next(_studentNames.Count)], _random.Next(8, 16),
                    _random.Next(2, 5), _random.Next(1, 10), subjects, direction,
                    new Point[] { new Point(100, 100), new Point(420, 460), new Point(460, 100), new Point(850, 400), new Point(100, 100) });
            }
            if (type == 1)
            {
                return new BeginnerStudent(_studentNames[_random.Next(_studentNames.Count)], _random.Next(8, 16),
                     _random.Next(2, 5), _random.Next(1, 10), subjects, direction,
                    new Point[] { new Point(100, 200), new Point(270, 400), new Point(380, 90), new Point(720, 400), new Point(100, 200) });
            }
            if (type == 2)
            {
                return new AdvancedStudent(_studentNames[_random.Next(_studentNames.Count)], _random.Next(8, 16),
                    _random.Next(2, 5), _random.Next(1, 10), subjects, direction,
                    new Point[] { new Point(100, 300), new Point(850, 400), new Point(420, 460), new Point(460, 100), new Point(100, 300) });
            }

            return new BeginnerStudent(_studentNames[_random.Next(_studentNames.Count)], _random.Next(8, 16),
                    _random.Next(2, 5), _random.Next(1, 10), subjects, direction,
                    new Point[] { new Point(100, 400), new Point(380, 90), new Point(720, 400), new Point(270, 400), new Point(100, 400) });
        }
    }
}
