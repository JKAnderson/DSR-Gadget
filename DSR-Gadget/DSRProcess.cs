using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
            Process[] t = Process.GetProcesses();
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

        public DSRProcess(Process candidate)
        {
            process = candidate;
            ID = process.Id;
            Version = "Unknown";
            Valid = false;

            dsrInterface = DSRInterface.Attach(process);
            if (dsrInterface != null)
            {
                uint versionValue = 0x0; //dsrInterface.ReadUInt32(DSROffsets.CheckVersion);
                if (DSROffsets.Versions.ContainsKey(versionValue))
                {
                    Version = DSROffsets.Versions[versionValue].Name;
                    offsets = DSROffsets.Versions[versionValue].Offsets;
                    Valid = offsets != null;
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
            return Valid && true;
        }

        public bool Focused()
        {
            IntPtr hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out uint pid);
            return pid == process.Id;
        }

        public void SetNoGravity(bool enable)
        {
            dsrInterface.WriteFlag32(dsrInterface.ResolveAddress(offsets.WorldChrBase, offsets.ChrDataOffset, (int)DSROffsets.ChrData.ChrFlags1),
                (uint)DSROffsets.ChrFlags1.NoGravity, enable);
        }
    }
}
