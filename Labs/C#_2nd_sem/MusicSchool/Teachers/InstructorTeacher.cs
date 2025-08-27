using System.Drawing;

namespace MusicSchool.Teachers
{
    public class InstructorTeacher : Teacher
    {
        public InstructorTeacher(string name, int age, Subject subject,
            Direction direction, int countOfWorkedHours, Point[] path)
            : base(name, age, subject, direction, countOfWorkedHours, path)
        {
        }

        public override void Teach()
        {
            CountOfWorkedHours++;
        }
    }
}
