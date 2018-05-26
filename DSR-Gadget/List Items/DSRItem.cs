using System;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRItem : IComparable<DSRItem>
    {
        public enum Upgrade
        {
            None = 0,
            Unique = 1,
            Armor = 2,
            Infusable = 3,
            InfusableRestricted = 4,
            PyroFlame = 5,
            PyroFlameAscended = 6,
        }

        private static Regex itemEntryRx = new Regex(@"^\s*(?<id>\S+)\s+(?<limit>\S+)\s+(?<upgrade>\S+)\s+(?<name>.+)$");

        private bool mystery;

        public string Name;
        public int ID;
        public int StackLimit;
        public Upgrade UpgradeType;

        public DSRItem(string config, bool showID)
        {
            Match itemEntry = itemEntryRx.Match(config);
            ID = Convert.ToInt32(itemEntry.Groups["id"].Value);
            StackLimit = Convert.ToInt32(itemEntry.Groups["limit"].Value);
            UpgradeType = (Upgrade)Convert.ToInt32(itemEntry.Groups["upgrade"].Value);
            mystery = showID;
            if (showID)
                Name = ID.ToString() + ": " + itemEntry.Groups["name"].Value;
            else
                Name = itemEntry.Groups["name"].Value;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(DSRItem other)
        {
            if (mystery)
                return ID.CompareTo(other.ID);
            else
                return Name.CompareTo(other.Name);
        }
    }
}
