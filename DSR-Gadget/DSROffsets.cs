using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    class DSROffsets
    {
        public static readonly IntPtr CheckVersion = (IntPtr)0x0;

        public IntPtr WorldChrBase = (IntPtr)0x141CEE830;
        public int ChrDataOffset = 0x68;
        public enum ChrData
        {
            ChrFlags1 = 0x284,
        }

        public enum ChrFlags1
        {
            NoGravity = 0x00004000,
        }

        public static DSROffsets V11 = new DSROffsets();

        public struct DSRVersion
        {
            public readonly string Name;
            public readonly DSROffsets Offsets;

            public DSRVersion(string name, DSROffsets offsets)
            {
                Name = name;
                Offsets = offsets;
            }
        }

        public static readonly Dictionary<uint, DSRVersion> Versions = new Dictionary<uint, DSRVersion>()
        {
            [0x0] = new DSRVersion("1.1", V11),
        };
    }
}
