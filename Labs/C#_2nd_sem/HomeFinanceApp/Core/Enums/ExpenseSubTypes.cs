using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Core.Enums
{
        internal enum ExpenseSubTypes
        {
            Food = 11,

            // Подтипы для Utilities
            Electricity = 21,
            Water = 22,
            Gas = 23,
            Internet = 24,

            // Подтипы для Clothing
            MenClothing = 31,
            WomenClothing = 32,
            ChildrenClothing = 33,
            Shoes = 34,

            // Подтипы для Entertainment
            Cinema = 41,
            Concerts = 42,
            Games = 43,
            Subscriptions = 44,

            // Подтипы для Education
            School = 51,
            Courses = 52,
            Books = 53,
            Tutoring = 54,

            // Подтипы для Cosmetics
            FaceCare = 61,
            HairCare = 62,
            Makeup = 63,
            Perfume = 64,

            // Подтипы для Hobby
            Sports = 71,
            Collecting = 72,
            Handmade = 73,
            Photography = 74
        }
}
