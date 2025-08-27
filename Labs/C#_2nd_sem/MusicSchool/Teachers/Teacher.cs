using System.Drawing;

namespace MusicSchool.Teachers
{
    public abstract class Teacher :  ITeaching
    {
        public string Name { get; }

        public int Age { get; }

        public Subject Subject { get; }

        public Direction Direction { get; }

        public int CountOfWorkedHours { get; protected set; }

        protected Teacher(string name, int age, Subject subject, Direction direction, int countOfWorkedHours, Point[] path)
            : base()
        {
            Name = name;
            Age = age;
            Subject = subject;
            Direction = direction;
            CountOfWorkedHours = countOfWorkedHours;
        }

        public abstract void Teach();
    }
}

