using System.Collections.Generic;
using System.Drawing;
using System;
using MusicSchool.Students;
using MusicSchool.Teachers;
using System.Threading.Tasks;

namespace MusicSchool
{
    public interface IObserver
    {
        Task MoveStudent(Student student);
    }
    public class MusicSchool
    {
        private readonly List<Classroom> _classrooms = new List<Classroom>();
        private readonly List<Student> _students = new List<Student>();
        private readonly List<Teacher> _teachers = new List<Teacher>();

        public List<IObserver> observers { get; set; } = new List<IObserver>();


        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public async Task Notify(Student s)
        {
            foreach (var o in observers)
                await o.MoveStudent(s);
        }

        public event Action<Student> StudentMoved;
        public event Action<Student> StudentAdded;
        public event Action LessonsStarted;
        public event Action LessonsStopped;

        public IReadOnlyList<Classroom> Classrooms => _classrooms;
        public IReadOnlyList<Student> Students => _students;
        public IReadOnlyList<Teacher> Teachers => _teachers;

        public MusicSchool()
        {
            // Инициализация фабрик
            IStudentFactory studentFactory = new StudentFactory();
            ITeacherFactory teacherFactory = new TeacherFactory();

            // Создание и добавление классов
            _classrooms.Add(new Classroom(new Point(300, 10)));
            _classrooms.Add(new Classroom(new Point(700, 300)));
            _classrooms.Add(new Classroom(new Point(300, 300)));

            // Создание и добавление учителей
            _teachers.Add(teacherFactory.CreateTeacher(0));
            _teachers.Add(teacherFactory.CreateTeacher(1));

            // Создание и добавление студентов
            CreateStudents(studentFactory);
        }

        private void CreateStudents(IStudentFactory studentFactory)
        {
            Random random = new Random();
            int N = random.Next(3, 5);
            for (int i = 0; i < N; i++)
            {
                // Создание ученика с помощью фабрики
                Student newStudent = studentFactory.CreateStudent(i);
                _students.Add(newStudent);
            }
        }

        public async Task StartMusicLessons()
        {
            LessonsStarted?.Invoke();
            while (true)
            {
                List<Task> movementTasks = new List<Task>();
                int i = 0;

                await Task.Delay(1000);

                foreach (var student in Students)
                {

                    Classroom temp = Classrooms[i % Classrooms.Count];

                    Task task = Notify(student);

                    temp.Students.Add(student);
                    student.Study();
                    StudentMoved?.Invoke(student);
                    temp.Students.Remove(student);
                    movementTasks.Add(task);
                    i++;

                }

                await Task.WhenAll(movementTasks);
                await Task.Delay(1000);
                LessonsStopped?.Invoke();
            }
            
        }
    }
}
