using System;
using System.Collections.Generic;

namespace DSR_Gadget
{
    internal class DSROffsets
    {
        public const string CamManBaseAOB = "48 8B 05 ? ? ? ? 48 63 D1 48 8B 44 D0 08 C3";
        public const int CamManOffset = 0x10;

        public const string ChrFollowCamAOB = "48 8B 0D ? ? ? ? E8 ? ? ? ? 48 8B 4E 68 48 8B 05 ? ? ? ? 48 89 48 60";
        public const int ChrFollowCamOffset1 = 0;
        public const int ChrFollowCamOffset2 = 0x60;
        public const int ChrFollowCamOffset3 = 0x60;

        public const string GroupMaskAOB = "80 3D ? ? ? ? 00 BE 00 00 00 80";
        public enum GroupMask
        {
            Map = 0x0,
            Objects = 0x1,
            Characters = 0x2,
            SFX = 0x3,
            Cutscenes = 0x4,
        }

        public const string GraphicsDataAOB = "48 8B 05 ? ? ? ? 48 8B 48 08 48 8B 01 48 8B 40 58";
        public const int GraphicsDataOffset1 = 0;
        public const int GraphicsDataOffset2 = 0x738;
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

        public const string ChrClassWarpAOB = "48 8B 05 ? ? ? ? 66 0F 7F 80 ? ? ? ? 0F 28 02 66 0F 7F 80 ? ? ? ? C6 80";
        public const int ChrClassWarpOffset1 = 0;
        public int ChrClassWarpBoost = 0x0;
        public enum ChrClassWarp
        {
            LastBonfire = 0xB24,
            StableX = 0xB90,
            StableY = 0xB94,
            StableZ = 0xB98,
            StableAngle = 0xBA4,
        }

        public const string WorldChrBaseAOB = "48 8B 05 ? ? ? ? 48 8B 48 68 48 85 C9 0F 84 ? ? ? ? 48 39 5E 10 0F 84 ? ? ? ? 48";
        public const int WorldChrBaseOffset1 = 0;
        public enum WorldChrBase
        {
            ChrData1 = 0x68,
            DeathCam = 0x70,
        }

        public int ChrData1Boost1 = 0x0;
        public int ChrData1Boost2 = 0x0;
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

        public const string ChrDbgAOB = "80 3D ? ? ? ? 00 48 8B 8F ? ? ? ? 0F B6 DB";
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

        public const string MenuManAOB = "48 8B 05 ? ? ? ? 89 88 28 08 00 00 85 C9 ? ? C7 80 34 08 00 00 FF FF FF FF C3";
        public const int MenuManOffset1 = 0;
        public enum MenuMan
        {
            MenuKick = 0x24C,
        }

        public const string ChrClassBaseAOB = "48 8B 05 ? ? ? ? 48 85 C0 ? ? F3 0F 58 80 AC 00 00 00";
        public const int ChrData2Offset1 = 0;
        public const int ChrData2Offset2 = 0x10;
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

        public const string EventFlagsAOB = "48 8B 0D ? ? ? ? 99 33 C2 45 33 C0 2B C2 8D 50 F6";
        public const int EventFlagsOffset1 = 0;
        public const int EventFlagsOffset2 = 0;

        public const string ItemGetAOB = "48 89 5C 24 18 89 54 24 10 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 F9";

        public const string BonfireWarpAOB = "48 89 5C 24 08 57 48 83 EC 20 48 8B D9 8B FA 48 8B 49 08 48 85 C9 0F 84 ? ? ? ? E8 ? ? ? ? 48 8B 4B 08";

        public static DSROffsets GetOffsets(int moduleSize)
        {
            DSROffsets result = new DSROffsets();
            int version = versions.ContainsKey(moduleSize) ? versions[moduleSize] : 100;

            if (version > 1)
            {
                result.ChrClassWarpBoost = 0x10;
            }

            if (version > 2)
            {
                result.ChrData1Boost1 = 0x20;
                result.ChrData1Boost2 = 0x10;
            }

            return result;
        }

        private static readonly Dictionary<int, int> versions = new Dictionary<int, int>()
        {
            [0x4869400] = 0, // 1.01
            [0x496BE00] = 1, // 1.01.1
            [0x37CB400] = 2, // 1.01.2
            [0x3817800] = 3, // 1.03
        };
    }
}
