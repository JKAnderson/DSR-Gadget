using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DSR_Gadget
{
    class DSRItemCategory
    {
        public string Name;
        public int ID;
        public List<DSRItem> Items;

        private DSRItemCategory(string name, int id, string itemList, bool showIDs)
        {
            Name = name;
            ID = id;
            Items = new List<DSRItem>();
            foreach (string line in Regex.Split(itemList, "[\r\n]+"))
                Items.Add(new DSRItem(line, showIDs));
            Items.Sort();
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<DSRItemCategory> All = new List<DSRItemCategory>()
        {
            new DSRItemCategory("Armor", 0x10000000, Properties.Resources.Armor, false),
            new DSRItemCategory("Consumables", 0x40000000, Properties.Resources.Consumables, false),
            new DSRItemCategory("Key Items", 0x40000000, Properties.Resources.KeyItems, false),
            new DSRItemCategory("Melee Weapons", 0x00000000, Properties.Resources.MeleeWeapons, false),
            new DSRItemCategory("Ranged Weapons", 0x00000000, Properties.Resources.RangedWeapons, false),
            new DSRItemCategory("Rings", 0x20000000, Properties.Resources.Rings, false),
            new DSRItemCategory("Shields", 0x00000000, Properties.Resources.Shields, false),
            new DSRItemCategory("Spells", 0x40000000, Properties.Resources.Spells, false),
            new DSRItemCategory("Spell Tools", 0x00000000, Properties.Resources.SpellTools, false),
            new DSRItemCategory("Upgrade Materials", 0x40000000, Properties.Resources.UpgradeMaterials, false),
            new DSRItemCategory("Usable Items", 0x40000000, Properties.Resources.UsableItems, false),
            new DSRItemCategory("Mystery Weapons", 0x00000000, Properties.Resources.MysteryWeapons, true),
            new DSRItemCategory("Mystery Armor", 0x10000000, Properties.Resources.MysteryArmor, true),
            new DSRItemCategory("Mystery Goods", 0x40000000, Properties.Resources.MysteryGoods, true),
        };
    }
}
