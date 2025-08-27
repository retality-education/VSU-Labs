namespace MusicSchool.Students
{
    public interface IStudentFactory
    {
        Student CreateStudent(int type);
    }
}
