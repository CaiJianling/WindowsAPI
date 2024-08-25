using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.Tools
{
    public class PowerChanger
    {

        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        /// <summary>
        /// 注销
        /// </summary>
        public static void ExitWindows()
        {
            ExitWindowsEx(0,0);
        }

        [DllImport("user32")]
        public static extern void LockWorkStation();
        /// <summary>
        /// 锁定
        /// </summary>
        public static void LockWindows()
        {
            LockWorkStation();
        }

        /// <summary>
        /// 关机
        /// </summary>
        /// <param name="tSecond">延时秒</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Shutdown(int tSecond = 0)
        {
            if (tSecond < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tSecond), "时间不能为负数。");
            }
            string shutdownCommand = $"/c shutdown /s /t {tSecond}";
            ExecuteCommandInCommandPrompt(shutdownCommand);
        }

        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="tSecond">延时秒</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Reboot(int tSecond = 0)
        {
            if (tSecond < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tSecond), "时间不能为负数。");
            }
            string rebootCommand = $"/c shutdown /r /t {tSecond}";
            ExecuteCommandInCommandPrompt(rebootCommand);
        }

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        /// <summary>
        /// 睡眠
        /// </summary>
        /// <param name="tSecond">延时秒</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Sleep(int tSecond = 0)
        {
            if (tSecond < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tSecond), "时间不能为负数。");
            }
            SetSuspendState(false, true, true);
        }

        // 定时器
        private static Timer timer;

        /// <summary>
        /// 休眠
        /// </summary>
        /// <param name="tSecond">延时秒</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Hibernate(int tSecond = 0)
        {
            if (tSecond < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tSecond), "时间不能为负数。");
            }
            else if (tSecond == 0)
            {
                SetSuspendState(true, true, true);
            }
            else
            {
                // 如果计时器已经存在，先取消它
                if (timer != null)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();
                }

                // 设置一个新的计时器
                timer = new Timer(state =>
                {
                    string hibernateCommand = $"/c shutdown /h";
                    ExecuteCommandInCommandPrompt(hibernateCommand);
                }, null, tSecond * 1000, Timeout.Infinite); // 将秒转换为毫秒
            }
        }

        /// <summary>
        /// 取消休眠计时器
        /// </summary>
        public static void CancelHibernate()
        {
            // 取消休眠计时器
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                timer = null;
            }
        }

        /// <summary>
        /// 取消关机
        /// </summary>
        public static void CancelShutdown()
        {
            string cancelShutdownCommand = "/c shutdown /a";
            ExecuteCommandInCommandPrompt(cancelShutdownCommand);
        }

        /// <summary>
        /// 获取当前最大CPU功率百分比
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMaxCpuValue()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c powercfg -q SCHEME_CURRENT SUB_PROCESSOR PROCTHROTTLEMAX | find \"当前交流电源设置索引: \"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // int startIndex = output.IndexOf("当前交流电源设置索引: ") + "当前交流电源设置索引: ".Length;
            // string hexValue = output.Substring(startIndex).Trim();
            string hexValue = output.Substring(output.IndexOf(": ") + 2).Trim();
            int decimalValue = Convert.ToInt32(hexValue, 16);
            return decimalValue;
        }

        /// <summary>
        /// 设置最大CPU功率百分比
        /// </summary>
        /// <param name="value">百分比值</param>
        public static void SetMaxCpuValue(int value)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/c powercfg -setacvalueindex SCHEME_CURRENT SUB_PROCESSOR PROCTHROTTLEMAX {value} && powercfg -s SCHEME_CURRENT";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// 在命令提示符中执行命令
        /// </summary>
        /// <param name="command">执行的语句</param>
        private static void ExecuteCommandInCommandPrompt(string command)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = command;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
        }
    }
}