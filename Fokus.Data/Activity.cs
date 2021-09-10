using System;

namespace Fokus.Data
{
    public class Activity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public int Calories { get; set; }
    }
}