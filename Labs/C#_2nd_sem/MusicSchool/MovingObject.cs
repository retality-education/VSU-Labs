using System;
using System.Timers;
using System.Drawing;
using System.Threading;

namespace MusicSchool
{
    // класс для представления движущегося объекта
    public class MovingObject
    {
        public Point Location { get; set; } // текущее местоположения объекта

        public Point[] PathToPurpose { get; set; } // массив точек определяющих путь объекта

        private int _currentStep; // текущий шаг на пути

        public MovingObject(Point[] path)
        {
            PathToPurpose = path; // инициализация пути
            Location = PathToPurpose[0]; // установка начального местоположения
            _currentStep = 0; // начало движения с первого шага
        }
    }
}
