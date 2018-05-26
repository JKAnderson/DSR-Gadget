using System.Collections.Generic;

namespace DSR_Gadget
{
    class DSRInfusion
    {
        public string Name;
        public int Value;
        public int MaxUpgrade;
        public bool Restricted;

        private DSRInfusion(string name, int value, int maxUpgrade, bool restricted)
        {
            Name = name;
            Value = value;
            MaxUpgrade = maxUpgrade;
            Restricted = restricted;
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<DSRInfusion> All = new List<DSRInfusion>()
        {
            new DSRInfusion("Normal", 000, 15, false),
            new DSRInfusion("Chaos", 900, 5, true),
            new DSRInfusion("Crystal", 100, 5, false),
            new DSRInfusion("Divine", 600, 10, false),
            new DSRInfusion("Enchanted", 500, 5, true),
            new DSRInfusion("Fire", 800, 10, false),
            new DSRInfusion("Lightning", 200, 5, false),
            new DSRInfusion("Magic", 400, 10, false),
            new DSRInfusion("Occult", 700, 5, true),
            new DSRInfusion("Raw", 300, 5, true),
        };
    }
}
