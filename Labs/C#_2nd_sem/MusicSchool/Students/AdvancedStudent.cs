using System.Drawing;
using System.Collections.Generic;

namespace MusicSchool.Students
{
    public class AdvancedStudent : Student
    {
        public AdvancedStudent(string name, int age, int learningRate, int knowledgeLevel,
            List<Subject> subjects, Direction direction, Point[] path)
            : base(name, age, 2 * learningRate, 2 * knowledgeLevel, subjects, direction, path)
        {
        }

        public override void Study()
        {
            KnowledgeLevel += 2;
        }
    }
}
