using System.Drawing;
using System.Collections.Generic;

namespace MusicSchool
{
    public class BeginnerStudent : Student
    {
        public BeginnerStudent(string name, int age, int learningRate, int knowledgeLevel,
            List<Subject> subjects, Direction direction, Point[] path)
            : base(name, age, learningRate, knowledgeLevel, subjects, direction, path)
        {
        }

        public override void Study()
        {
            KnowledgeLevel++;
        }
    }
}
