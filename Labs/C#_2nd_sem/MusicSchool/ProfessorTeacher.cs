using System.Drawing;



namespace MusicSchool
{
    public class ProfessorTeacher : Teacher
    {
        public ProfessorTeacher(string name, int age, Subject subject,
            Direction direction, int countOfWorkedHours, Point[] path)
            : base(name, age, subject, direction, countOfWorkedHours, path)
        {
        }

        public override void Teach()
        {
            CountOfWorkedHours += 2;
        }
    }
}
