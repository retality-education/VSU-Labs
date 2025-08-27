using System;
using System.Collections.Generic;
using System.Drawing;


namespace MusicSchool
{
    public abstract class Student : MovingObject, IStudiable
    {
        public string Name { get; }

        public int Age { get; }

        public int LearningRate { get; protected set; }

        public int KnowledgeLevel { get; protected set; }

        public List<Subject> Subjects { get; }

        public Direction Direction { get; }

        protected Student(string name, int age, int learningRate, int knowledgeLevel,
            List<Subject> subjects, Direction direction, Point[] path) : base(path)
        {
            Name = name;
            Age = age;
            LearningRate = learningRate;
            KnowledgeLevel = knowledgeLevel;
            Subjects = subjects;
            Direction = direction;
        }

        public abstract void Study();
    }
}

