using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DSR_Gadget
{
    class DSRProcess
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static DSRProcess GetProcess()
        {
            DSRProcess result = null;
            Process[] candidates = Process.GetProcessesByName("DarkSoulsRemastered");
            if (candidates.Length > 0)
                result = new DSRProcess(candidates[0]);
            return result;
        }

        private readonly Process process;
        private readonly DSRInterface dsrInterface;
        private readonly DSROffsets offsets;

        public readonly int ID;
        public readonly string Version;
        public readonly bool Valid;

        public static readonly Dictionary<int, string> versions = new Dictionary<int, string>()
        {
            [0x4869400] = "1.01",
            [0x496BE00] = "1.01.1",
        };

        public DSRProcess(Process candidate)
        {
            process = candidate;
            ID = process.Id;
            Version = "Unknown";
            Valid = false;

            dsrInterface = new DSRInterface(process);
            if (dsrInterface != null)
            {
                int size = process.MainModule.ModuleMemorySize;
                if (versions.ContainsKey(size))
                    Version = versions[size];
                offsets = new DSROffsets();

                try
                {
                    offsets.CamManBasePtr = dsrInterface.AOBScan(DSROffsets.CamManBaseAOB, 3, 7);
                    offsets.GroupMaskAddr = dsrInterface.AOBScan(DSROffsets.GroupMaskAOB, 2, 7);
                    offsets.GraphicsDataPtr = dsrInterface.AOBScan(DSROffsets.GraphicsDataAOB, 3, 7);
                    offsets.ChrClassWarpPtr = dsrInterface.AOBScan(DSROffsets.ChrClassWarpAOB, 3, 7);
                    offsets.WorldChrBasePtr = dsrInterface.AOBScan(DSROffsets.WorldChrBaseAOB, 3, 7);
                    offsets.ChrDbgAddr = dsrInterface.AOBScan(DSROffsets.ChrDbgAOB, 2, 7);
                    offsets.MenuManPtr = dsrInterface.AOBScan(DSROffsets.MenuManAOB, 3, 7);
                    offsets.ChrClassBasePtr = dsrInterface.AOBScan(DSROffsets.ChrClassBaseAOB, 3, 7);

                    offsets.ItemGetAddr = dsrInterface.AOBScan(DSROffsets.ItemGetAOB);
                    offsets.BonfireWarpAddr = dsrInterface.AOBScan(DSROffsets.BonfireWarpAOB);
                    Valid = true;
                }
                catch (ArgumentException)
                {
                    Valid = false;
                }
            }
        }

        public void Close()
        {
            dsrInterface.Close();
        }

        public bool Alive()
        {
            return !process.HasExited;
        }

        public bool Loaded()
        {
            if (Valid)
            {
                IntPtr chrAnimData = dsrInterface.ResolveAddress(offsets.WorldChrBasePtr,
                    (int)DSROffsets.WorldChrBase.ChrData1, (int)DSROffsets.ChrData1.ChrMapData, (int)DSROffsets.ChrMapData.ChrAnimData);
                return chrAnimData != IntPtr.Zero;
            }
            else
                return false;
        }

        public bool Focused()
        {
            IntPtr hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out uint pid);
            return pid == process.Id;
        }

        private struct DSRPointers
        {
            public IntPtr CamMan, ChrAnimData, ChrClassWarp, ChrData1, ChrData2, ChrMapData, ChrPosData,
                GraphicsData, MenuMan, WorldChrBase;
        }
        private DSRPointers pointers;

        public void LoadPointers()
        {
            pointers.CamMan = dsrInterface.ResolveAddress(offsets.CamManBasePtr + DSROffsets.CamManOffset);
            pointers.ChrClassWarp = dsrInterface.ReadIntPtr(offsets.ChrClassWarpPtr);
            pointers.WorldChrBase = dsrInterface.ReadIntPtr(offsets.WorldChrBasePtr);
            pointers.ChrData1 = dsrInterface.ReadIntPtr(pointers.WorldChrBase + (int)DSROffsets.WorldChrBase.ChrData1);
            pointers.ChrMapData = dsrInterface.ReadIntPtr(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrMapData);
            pointers.ChrAnimData = dsrInterface.ReadIntPtr(pointers.ChrMapData + (int)DSROffsets.ChrMapData.ChrAnimData);
            pointers.ChrPosData = dsrInterface.ReadIntPtr(pointers.ChrMapData + (int)DSROffsets.ChrMapData.ChrPosData);
            pointers.ChrData2 = dsrInterface.ResolveAddress(offsets.ChrClassBasePtr, DSROffsets.ChrData2Offset);
            pointers.GraphicsData = dsrInterface.ResolveAddress(offsets.GraphicsDataPtr, DSROffsets.GraphicsDataOffset);
            pointers.MenuMan = dsrInterface.ReadIntPtr(offsets.MenuManPtr);
        }

        #region Player
        public int GetHealth()
        {
            return dsrInterface.ReadInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.Health);
        }

        public void SetHealth(int value)
        {
            dsrInterface.WriteInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.Health, value);
        }

        public int GetHealthMax()
        {
            return dsrInterface.ReadInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.MaxHealth);
        }

        public int GetStamina()
        {
            return dsrInterface.ReadInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.Stamina);
        }

        public void SetStamina(int value)
        {
            dsrInterface.WriteInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.Stamina, value);
        }

        public int GetStaminaMax()
        {
            return dsrInterface.ReadInt32(pointers.ChrData1 + (int)DSROffsets.ChrData1.MaxStamina);
        }

        public void GetPosition(out float x, out float y, out float z, out float angle)
        {
            x = dsrInterface.ReadFloat(pointers.ChrPosData + (int)DSROffsets.ChrPosData.PosX);
            y = dsrInterface.ReadFloat(pointers.ChrPosData + (int)DSROffsets.ChrPosData.PosY);
            z = dsrInterface.ReadFloat(pointers.ChrPosData + (int)DSROffsets.ChrPosData.PosZ);
            angle = dsrInterface.ReadFloat(pointers.ChrPosData + (int)DSROffsets.ChrPosData.PosAngle);
        }

        public void GetStablePosition(out float x, out float y, out float z, out float angle)
        {
            x = dsrInterface.ReadFloat(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.StableX);
            y = dsrInterface.ReadFloat(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.StableY);
            z = dsrInterface.ReadFloat(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.StableZ);
            angle = dsrInterface.ReadFloat(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.StableAngle);
        }

        public void PosWarp(float x, float y, float z, float angle)
        {
            dsrInterface.WriteFloat(pointers.ChrMapData + (int)DSROffsets.ChrMapData.WarpX, x);
            dsrInterface.WriteFloat(pointers.ChrMapData + (int)DSROffsets.ChrMapData.WarpY, y);
            dsrInterface.WriteFloat(pointers.ChrMapData + (int)DSROffsets.ChrMapData.WarpZ, z);
            dsrInterface.WriteFloat(pointers.ChrMapData + (int)DSROffsets.ChrMapData.WarpAngle, angle);
            dsrInterface.WriteBool(pointers.ChrMapData + (int)DSROffsets.ChrMapData.Warp, true);
        }

        public void SetNoGravity(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags1,
                (uint)DSROffsets.ChrFlags1.NoGravity, value);
        }

        public void SetNoCollision(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrMapData + (int)DSROffsets.ChrMapData.ChrMapFlags,
                (uint)DSROffsets.ChrMapFlags.DisableMapHit, value);
        }

        public bool GetDeathCam()
        {
            return dsrInterface.ReadBool(pointers.WorldChrBase + (int)DSROffsets.WorldChrBase.DeathCam);
        }

        public void SetDeathCam(bool value)
        {
            dsrInterface.WriteBool(pointers.WorldChrBase + (int)DSROffsets.WorldChrBase.DeathCam, value);
        }

        public int GetLastBonfire()
        {
            return dsrInterface.ReadInt32(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.LastBonfire);
        }

        public void SetLastBonfire(int value)
        {
            dsrInterface.WriteInt32(pointers.ChrClassWarp + (int)DSROffsets.ChrClassWarp.LastBonfire, value);
        }

        public void BonfireWarp()
        {
            byte[] asm = (byte[])DSRAssembly.BonfireWarp.Clone();
            byte[] bytes = BitConverter.GetBytes(offsets.ChrClassBasePtr.ToInt64());
            Array.Copy(bytes, 0, asm, 0x2, 8);
            bytes = BitConverter.GetBytes(offsets.BonfireWarpAddr.ToInt64());
            Array.Copy(bytes, 0, asm, 0x18, 8);
            dsrInterface.Execute(asm);
        }

        public void SetAnimSpeed(float value)
        {
            dsrInterface.WriteFloat(pointers.ChrAnimData + (int)DSROffsets.ChrAnimData.AnimSpeed, value);
        }

        public byte[] DumpCamMan()
        {
            return dsrInterface.ReadBytes(pointers.CamMan, 1024);
        }

        public void UndumpCamMan(byte[] bytes)
        {
            dsrInterface.WriteBytes(pointers.CamMan, bytes);
        }
        #endregion

        #region Stats
        public int GetClass()
        {
            return dsrInterface.ReadByte(pointers.ChrData2 + (int)DSROffsets.ChrData2.Class);
        }

        public void SetClass(int value)
        {
            dsrInterface.WriteByte(pointers.ChrData2 + (int)DSROffsets.ChrData2.Class, (byte)value);
        }

        public int GetHumanity()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Humanity);
        }

        public void SetHumanity(int value)
        {
            dsrInterface.WriteInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Humanity, value);
        }

        public int GetSouls()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Souls);
        }

        public void SetSouls(int value)
        {
            dsrInterface.WriteInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Souls, value);
        }

        public int GetSoulLevel()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.SoulLevel);
        }

        public int GetVitality()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Vitality);
        }

        public int GetAttunement()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Attunement);
        }

        public int GetEndurance()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Endurance);
        }

        public int GetStrength()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Strength);
        }

        public int GetDexterity()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Dexterity);
        }

        public int GetResistance()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Resistance);
        }

        public int GetIntelligence()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Intelligence);
        }

        public int GetFaith()
        {
            return dsrInterface.ReadInt32(pointers.ChrData2 + (int)DSROffsets.ChrData2.Faith);
        }

        public void LevelUp(int vit, int att, int end, int str, int dex, int res, int intel, int fth, int level)
        {
            throw new NotImplementedException("LevelUp is not AOB-ified");

            /*IntPtr stats = dsrInterface.Allocate(0x300);
            dsrInterface.WriteInt32(stats + 0x268 + 0x0, vit);
            dsrInterface.WriteInt32(stats + 0x268 + 0x4, att);
            dsrInterface.WriteInt32(stats + 0x268 + 0x8, end);
            dsrInterface.WriteInt32(stats + 0x268 + 0xC, str);
            dsrInterface.WriteInt32(stats + 0x268 + 0x10, dex);
            dsrInterface.WriteInt32(stats + 0x268 + 0x14, res);
            dsrInterface.WriteInt32(stats + 0x268 + 0x18, intel);
            dsrInterface.WriteInt32(stats + 0x268 + 0x1C, fth);
            dsrInterface.WriteInt32(stats + 0x268 + 0x20, GetHumanity());
            dsrInterface.WriteInt32(stats + 0x268 + 0x24, level);
            dsrInterface.WriteInt32(stats + 0x268 + 0x28, 0);
            dsrInterface.WriteInt32(stats + 0x268 + 0x2C, 0);
            dsrInterface.WriteInt32(stats + 0x268 + 0x30, GetSouls());

            byte[] asm = (byte[])DSRAssembly.LevelUp.Clone();
            byte[] bytes = BitConverter.GetBytes(stats.ToInt64() + 0x268);
            Array.Copy(bytes, 0, asm, 0x2, 8);
            bytes = BitConverter.GetBytes(stats.ToInt64());
            Array.Copy(bytes, 0, asm, 0xC, 8);

            dsrInterface.Execute(asm);
            dsrInterface.Free(stats);*/
        }
        #endregion

        #region Items
        public void GetItem(int category, int id, int quantity)
        {
            byte[] asm = (byte[])DSRAssembly.GetItem.Clone();

            byte[] bytes = BitConverter.GetBytes(category);
            Array.Copy(bytes, 0, asm, 0x1, 4);
            bytes = BitConverter.GetBytes(quantity);
            Array.Copy(bytes, 0, asm, 0x7, 4);
            bytes = BitConverter.GetBytes(id);
            Array.Copy(bytes, 0, asm, 0xD, 4);
            bytes = BitConverter.GetBytes(offsets.ChrClassBasePtr.ToInt64());
            Array.Copy(bytes, 0, asm, 0x19, 8);
            bytes = BitConverter.GetBytes(offsets.ItemGetAddr.ToInt64());
            Array.Copy(bytes, 0, asm, 0x46, 8);

            dsrInterface.Execute(asm);
        }
        #endregion

        #region Cheats
        public bool GetPlayerDeadMode()
        {
            return dsrInterface.ReadFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags1, (uint)DSROffsets.ChrFlags1.SetDeadMode);
        }

        public void SetPlayerDeadMode(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags1, (uint)DSROffsets.ChrFlags1.SetDeadMode, value);
        }

        public void SetPlayerNoDead(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.PlayerNoDead, value);
        }

        public void SetPlayerDisableDamage(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags1, (uint)DSROffsets.ChrFlags1.DisableDamage, value);
        }

        public void SetPlayerNoHit(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags2, (uint)DSROffsets.ChrFlags2.NoHit, value);
        }

        public void SetPlayerNoStamina(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags2, (uint)DSROffsets.ChrFlags2.NoStaminaConsumption, value);
        }

        public void SetPlayerSuperArmor(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags1, (uint)DSROffsets.ChrFlags1.SetSuperArmor, value);
        }

        public void SetPlayerHide(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.PlayerHide, value);
        }

        public void SetPlayerSilence(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.PlayerSilence, value);
        }

        public void SetPlayerExterminate(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.PlayerExterminate, value);
        }

        public void SetPlayerNoGoods(bool value)
        {
            dsrInterface.WriteFlag32(pointers.ChrData1 + (int)DSROffsets.ChrData1.ChrFlags2, (uint)DSROffsets.ChrFlags2.NoGoodsConsume, value);
        }

        public void SetAllNoArrow(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoArrowConsume, value);
        }

        public void SetAllNoMagicQty(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoMagicQtyConsume, value);
        }

        public void SetAllNoDead(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoDead, value);
        }

        public void SetAllNoDamage(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoDamage, value);
        }

        public void SetAllNoHit(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoHit, value);
        }

        public void SetAllNoStamina(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoStaminaConsume, value);
        }

        public void SetAllNoAttack(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoAttack, value);
        }

        public void SetAllNoMove(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoMove, value);
        }

        public void SetAllNoUpdateAI(bool value)
        {
            dsrInterface.WriteBool(offsets.ChrDbgAddr + (int)DSROffsets.ChrDbg.AllNoUpdateAI, value);
        }
        #endregion

        #region Graphics
        public void SetDrawMap(bool value)
        {
            dsrInterface.WriteBool(offsets.GroupMaskAddr + (int)DSROffsets.GroupMask.Map, value);
        }

        public void SetDrawObjects(bool value)
        {
            dsrInterface.WriteBool(offsets.GroupMaskAddr + (int)DSROffsets.GroupMask.Objects, value);
        }

        public void SetDrawCharacters(bool value)
        {
            dsrInterface.WriteBool(offsets.GroupMaskAddr + (int)DSROffsets.GroupMask.Characters, value);
        }

        public void SetDrawSFX(bool value)
        {
            dsrInterface.WriteBool(offsets.GroupMaskAddr + (int)DSROffsets.GroupMask.SFX, value);
        }

        public void SetDrawCutscenes(bool value)
        {
            dsrInterface.WriteBool(offsets.GroupMaskAddr + (int)DSROffsets.GroupMask.Cutscenes, value);
        }

        public void SetFilterOverride(bool value)
        {
            dsrInterface.WriteBool(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterOverride, value);
        }

        public void SetFilterValues(float brightR, float brightG, float brightB, float contR, float contG, float contB, float saturation, float hue)
        {
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterBrightnessR, brightR);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterBrightnessG, brightG);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterBrightnessB, brightB);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterContrastR, contR);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterContrastG, contG);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterContrastB, contB);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterSaturation, saturation);
            dsrInterface.WriteFloat(pointers.GraphicsData + (int)DSROffsets.GraphicsData.FilterHue, hue);
        }
        #endregion

        #region Hotkeys
        public void MenuKick()
        {
            dsrInterface.WriteInt32(pointers.MenuMan + (int)DSROffsets.MenuMan.MenuKick, 2);
        }

#if DEBUG
        public void HotkeyTest1()
        {

        }

        public void HotkeyTest2()
        {

        }
#endif
        #endregion
    }
}
