using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRClass
    {
        private static Regex classEntryRx = new Regex(@"^(?<id>\S+) (?<sl>\S+) (?<vit>\S+) (?<att>\S+) (?<end>\S+) (?<str>\S+) (?<dex>\S+) (?<res>\S+) (?<int>\S+) (?<fth>\S+) (?<name>.+)$");

        public string Name;
        public byte ID;
        public int SoulLevel;
        public int Vitality;
        public int Attunement;
        public int Endurance;
        public int Strength;
        public int Dexterity;
        public int Resistance;
        public int Intelligence;
        public int Faith;

        private DSRClass(string config)
        {
            Match classEntry = classEntryRx.Match(config);
            Name = classEntry.Groups["name"].Value;
            ID = Convert.ToByte(classEntry.Groups["id"].Value);
            SoulLevel = Convert.ToInt32(classEntry.Groups["sl"].Value);
            Vitality = Convert.ToInt32(classEntry.Groups["vit"].Value);
            Attunement = Convert.ToInt32(classEntry.Groups["att"].Value);
            Endurance = Convert.ToInt32(classEntry.Groups["end"].Value);
            Strength = Convert.ToInt32(classEntry.Groups["str"].Value);
            Dexterity = Convert.ToInt32(classEntry.Groups["dex"].Value);
            Resistance = Convert.ToInt32(classEntry.Groups["res"].Value);
            Intelligence = Convert.ToInt32(classEntry.Groups["int"].Value);
            Faith = Convert.ToInt32(classEntry.Groups["fth"].Value);
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<DSRClass> All = new List<DSRClass>();

        static DSRClass()
        {
            foreach (string line in Regex.Split(Properties.Resources.Classes, "[\r\n]+"))
                All.Add(new DSRClass(line));
        }
    }
}
