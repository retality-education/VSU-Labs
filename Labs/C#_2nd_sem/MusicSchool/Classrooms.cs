using MusicSchool.Students;
using MusicSchool.Teachers;
using System.Collections.Generic;
using System.Drawing;

namespace MusicSchool
{
    public class Classroom
    {
        public Teacher Teacher { get; set; }

        public Subject Subject { get; set; }

        public List<Student> Students { get; set; }

        public Point Location { get; set; }

        public Classroom(Point location)
        {
            Students = new List<Student>();
            Location = location;
        }
    }
}

