using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    class DSROffsets
    {
        public static readonly IntPtr CheckVersion = (IntPtr)0x140000080;

        public IntPtr GroupMaskAddr = (IntPtr)0x141AA1DB0;
        public enum GroupMask
        {
            Map = 0x0,
            Objects = 0x1,
            Characters = 0x2,
            SFX = 0x3,
            Cutscenes = 0x4,
        }

        public IntPtr GraphicsDataPtr = (IntPtr)0x141BDE5C0;
        public static int GraphicsDataOffset = 0x738;
        public enum GraphicsData
        {
            FilterOverride = 0x34D,
            FilterBrightnessR = 0x350,
            FilterBrightnessG = 0x354,
            FilterBrightnessB = 0x358,
            FilterSaturation = 0x35C,
            FilterContrastR = 0x360,
            FilterContrastG = 0x364,
            FilterContrastB = 0x368,
            FilterHue = 0x36C,
        }

        public IntPtr ChrClassWarpPtr = (IntPtr)0x141CEA4B8;
        public enum ChrClassWarp
        {
            LastBonfire = 0xB24,
            StableX = 0xB90,
            StableY = 0xB94,
            StableZ = 0xB98,
            StableAngle = 0xB9C,
        }

        public IntPtr WorldChrBasePtr = (IntPtr)0x141CEE830;
        public enum WorldChrBase
        {
            ChrData1 = 0x68,
            DeathCam = 0x70,
        }

        public enum ChrData1
        {
            ChrMapData = 0x48,
            ChrFlags1 = 0x284,
            Health = 0x3D8,
            MaxHealth = 0x3DC,
            Stamina = 0x3E8,
            MaxStamina = 0x3EC,
            ChrFlags2 = 0x514,
        }

        [Flags]
        public enum ChrFlags1
        {
            ForceSetOmission = 0x00000001,
            DisableHPGauge = 0x00000008,
            SetEventGenerate = 0x00000010,
            ForcePlayAnimation = 0x00000100,
            ForceUpdateNextFrame = 0x00000200,
            NoGravity = 0x00004000,
            SetSuperArmor = 0x00010000,
            FirstPerson = 0x00100000,
            ToggleDraw = 0x00800000,
            // Can't die, but still take damage from killboxes and trigger deathcams
            SetDeadMode = 0x02000000,
            // Doesn't prevent healing
            DisableDamage = 0x04000000,
            // Super armor and disable damage, still die to killbox
            EnableInvincible = 0x08000000,
        }

        [Flags]
        public enum ChrFlags2
        {
            DrawHit = 0x00000004,
            // Can't die, killboxes and death cams do nothing
            NoDead = 0x00000020,
            // Also prevents healing
            NoDamage = 0x00000040,
            NoHit = 0x00000080,
            NoAttack = 0x00000100,
            NoMove = 0x00000200,
            NoStaminaConsumption = 0x00000400,
            NoMpConsumption = 0x00000800,
            DrawDirection = 0x00004000,
            NoUpdate = 0x00008000,
            DrawCounter = 0x00200000,
            NoGoodsConsume = 0x01000000,
        }

        public enum ChrMapData
        {
            ChrAnimData = 0x18,
            ChrPosData = 0x28,
            ChrMapFlags = 0x104,
            Warp = 0x108,
            WarpX = 0x110,
            WarpY = 0x114,
            WarpZ = 0x118,
            WarpAngle = 0x124,
        }

        [Flags]
        public enum ChrMapFlags : uint
        {
            DisableMapHit = 0x00000010,
        }

        public enum ChrAnimData
        {
            AnimSpeed = 0xA8,
        }

        public enum ChrPosData
        {
            PosAngle = 0x4,
            PosX = 0x10,
            PosY = 0x14,
            PosZ = 0x18,
        }

        public IntPtr ChrDbgAddr = (IntPtr)0x141CEE849;
        public enum ChrDbg
        {
            PlayerNoDead = 0x0,
            PlayerExterminate = 0x1,
            AllNoStaminaConsume = 0x2,
            AllNoMpConsume = 0x3,
            AllNoArrowConsume = 0x4,
            AllNoMagicQtyConsume = 0x5,
            PlayerHide = 0x6,
            PlayerSilence = 0x7,
            AllNoDead = 0x8,
            AllNoDamage = 0x9,
            AllNoHit = 0xA,
            AllNoAttack = 0xB,
            AllNoMove = 0xC,
            AllNoUpdateAI = 0xD,
            PlayerReload = 0x12,
        }

        public IntPtr MenuManPtr = (IntPtr)0x141CFF7C8;
        public enum MenuMan
        {
            MenuKick = 0x24C,
        }

        public IntPtr ChrClassBasePtr = (IntPtr)0x141D00F50;
        public static int ChrData2Offset = 0x10;
        public enum ChrData2
        {
            Vitality = 0x40,
            Attunement = 0x48,
            Endurance = 0x50,
            Strength = 0x58,
            Dexterity = 0x60,
            Intelligence = 0x68,
            Faith = 0x70,
            Humanity = 0x84,
            Resistance = 0x88,
            SoulLevel = 0x90,
            Souls = 0x94,
            Gender = 0xCA,
            Class = 0xCE,
        }

        public static DSROffsets V101 = new DSROffsets();

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

        public static readonly Dictionary<int, DSRVersion> Versions = new Dictionary<int, DSRVersion>()
        {
            [0x6AA8592B] = new DSRVersion("1.01", V101),
        };
    }
}
