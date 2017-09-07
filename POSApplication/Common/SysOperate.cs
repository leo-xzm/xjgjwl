using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace POSApplication.Common
{
    public class SysOperate
    {
        /// <summary> 
        /// 设置程序开机启动 
        /// 或取消开机启动 
        /// </summary> 
        /// <param name="started">设置开机启动，或者取消开机启动</param> 
        /// <param name="exeName">注册表中程序的名字</param> 
        /// <param name="path">开机启动的程序路径</param> 
        /// <returns>开启或则停用是否成功</returns> 
        public static bool runWhenStart(bool started, string exeName, string path)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//打开注册表子项 
            if (key == null)//如果该项不存在的话，则创建该子项 
            {
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            if (started == true)
            {
                try
                {
                    key.SetValue(exeName, path);//设置为开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    key.DeleteValue(exeName);//取消开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;

            public long Luid;

            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]

        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]

        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,

            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool ExitWindowsEx(int flg, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;

        internal const int TOKEN_QUERY = 0x00000008;

        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        internal const int EWX_LOGOFF = 0x00000000;

        internal const int EWX_SHUTDOWN = 0x00000001;

        internal const int EWX_REBOOT = 0x00000002;

        internal const int EWX_FORCE = 0x00000004;

        internal const int EWX_POWEROFF = 0x00000008;

        internal const int EWX_FORCEIFHUNG = 0x00000010;

        /// <summary>
        /// flag=0关机，=1重启
        /// </summary>
        /// <param name="i"></param>
        public static void DoExitWin(int flag)
        {
            bool ok;

            TokPriv1Luid tp;

            IntPtr hproc = GetCurrentProcess();

            IntPtr htok = IntPtr.Zero;

            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);

            tp.Count = 1;

            tp.Luid = 0;

            tp.Attr = SE_PRIVILEGE_ENABLED;

            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);

            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

            if(flag == 0)
                ok = ExitWindowsEx(EWX_SHUTDOWN, 0);

            else if(flag == 1)
                ok = ExitWindowsEx(EWX_REBOOT, 0);
        }
    }
}
