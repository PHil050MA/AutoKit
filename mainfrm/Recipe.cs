using System;

namespace mainfrm
{
    [Serializable]
    public class Recipe
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string OK { get; set; }
        public string RV { get; set; }
        public string RJ { get; set; }
    }
}
