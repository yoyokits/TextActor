using System;

namespace TextActor.Models
{
    public class Item
    {
        private static int _idCounter;

        public int Id { get; set; } = _idCounter++;
        public string Text { get; set; }
        public string Description { get; set; }
    }
}