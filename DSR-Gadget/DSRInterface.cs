using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DSR_Gadget
{
    class DSRInterface
    {
        private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        private const uint MEM_COMMIT_RESERVE = 0x1000 | 0x2000;
        private const uint MEM_RELEASE = 0x8000;
        private const uint PAGE_READWRITE = 0x4;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(SafeProcessHandle hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(SafeProcessHandle hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern IntPtr VirtualAllocEx(SafeProcessHandle hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualFreeEx(SafeProcessHandle hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateRemoteThread(SafeProcessHandle hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        private static extern int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        public static DSRInterface Attach(Process process)
        {
            DSRInterface result = null;
            IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)process.Id);
            if (handle != IntPtr.Zero)
                result = new DSRInterface(new SafeProcessHandle(handle, true));
            return result;
        }


        private SafeProcessHandle handle;

        private DSRInterface(SafeProcessHandle setHandle)
        {
            handle = setHandle;
        }

        public void Close()
        {
            handle.Close();
        }

        private byte[] ReadProcessMemory(IntPtr address, uint size)
        {
            byte[] result = new byte[size];
            ReadProcessMemory(handle, address, result, size, 0);
            return result;
        }

        private bool WriteProcessMemory(IntPtr address, byte[] bytes)
        {
            return WriteProcessMemory(handle, address, bytes, (uint)bytes.Length, 0);
        }

        private IntPtr VirtualAllocEx(int size, uint protect = PAGE_READWRITE)
        {
            return VirtualAllocEx(handle, IntPtr.Zero, (uint)size, MEM_COMMIT_RESERVE, protect);
        }

        private bool VirtualFreeEx(IntPtr address)
        {
            return VirtualFreeEx(handle, address, 0, MEM_RELEASE);
        }

        private IntPtr CreateRemoteThread(IntPtr address)
        {
            return CreateRemoteThread(handle, IntPtr.Zero, 0, address, IntPtr.Zero, 0, IntPtr.Zero);
        }

        public IntPtr Allocate(int size)
        {
            return VirtualAllocEx(size);
        }

        public bool Free(IntPtr address)
        {
            return VirtualFreeEx(address);
        }

        public void Execute(byte[] asm)
        {
            IntPtr address = VirtualAllocEx(asm.Length, PAGE_EXECUTE_READWRITE);
            WriteProcessMemory(address, asm);
            IntPtr thread = CreateRemoteThread(address);
            WaitForSingleObject(thread, 0xFFFFFFFF);
            VirtualFreeEx(address);
        }

        public byte[] ReadBytes(IntPtr address, int size)
        {
            return ReadProcessMemory(address, (uint)size);
        }

        public bool ReadBool(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 1);
            return BitConverter.ToBoolean(bytes, 0);
        }

        public byte ReadByte(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 1);
            return bytes[0];
        }

        public float ReadFloat(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            return BitConverter.ToSingle(bytes, 0);
        }

        public double ReadDouble(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 8);
            return BitConverter.ToDouble(bytes, 0);
        }

        public short ReadInt16(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 2);
            return BitConverter.ToInt16(bytes, 0);
        }

        public ushort ReadUInt16(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public int ReadInt32(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public uint ReadUInt32(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public long ReadInt64(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 8);
            return BitConverter.ToInt64(bytes, 0);
        }

        public ulong ReadUInt64(IntPtr address)
        {
            byte[] bytes = ReadProcessMemory(address, 8);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public IntPtr ReadIntPtr(IntPtr address)
        {
            return (IntPtr)ReadInt64(address);
        }

        public IntPtr ResolveAddress(IntPtr address, params int[] offsets)
        {
            foreach (int offset in offsets)
                address = ReadIntPtr(address) + offset;
            return ReadIntPtr(address);
        }

        public bool ReadFlag32(IntPtr address, uint mask)
        {
            byte[] bytes = ReadProcessMemory(address, 4);
            uint flags = BitConverter.ToUInt32(bytes, 0);
            return (flags & mask) != 0;
        }


        public void WriteBool(IntPtr address, bool value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteByte(IntPtr address, byte value)
        {
            // Note: do not BitConverter.GetBytes this, stupid
            WriteProcessMemory(address, new byte[] { value });
        }

        public void WriteBytes(IntPtr address, byte[] bytes)
        {
            WriteProcessMemory(address, bytes);
        }

        public void WriteFloat(IntPtr address, float value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteInt16(IntPtr address, short value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteUInt16(IntPtr address, ushort value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteInt32(IntPtr address, int value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteUInt32(IntPtr address, uint value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteInt64(IntPtr address, long value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteUInt64(IntPtr address, ulong value)
        {
            WriteProcessMemory(address, BitConverter.GetBytes(value));
        }

        public void WriteFlag32(IntPtr address, uint mask, bool enable)
        {
            uint flags = ReadUInt32(address);
            if (enable)
                flags |= mask;
            else
                flags &= ~mask;
            WriteUInt32(address, flags);
        }
    }
}
