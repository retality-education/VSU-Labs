namespace MusicSchool.Teachers
{
    public interface ITeacherFactory
    {
        Teacher CreateTeacher(int type);
    }
}
