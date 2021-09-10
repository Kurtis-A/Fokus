using System;

namespace Fokus
{
    public class ActivityViewModel
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public int Calories { get; set; }
    }
}