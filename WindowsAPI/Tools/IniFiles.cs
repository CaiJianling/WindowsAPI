﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.Tools
{
    internal class IniFiles
    {
        public string inipath;

        //声明API函数

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="INIPath">文件路径</param> 
        public IniFiles(string INIPath)
        {
            inipath = INIPath;
        }

        public IniFiles() { }

        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
        public void FindAndCreate(string inipath)
        {
            if (!File.Exists(inipath))
            {
                string iniFileName = inipath;
                FileStream fs;
                StreamWriter sw;
                if (File.Exists(iniFileName))
                //验证文件是否存在，有则追加，无则创建
                {
                    fs = new FileStream(iniFileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(iniFileName, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.Close();
                fs.Close();
                WritePrivateProfileString("Mail", "SMTP_SERVER", "", this.inipath);
                WritePrivateProfileString("Mail", "SMTP_PORT", "25", this.inipath);
                WritePrivateProfileString("Mail", "SMTP_USERNAME", "", this.inipath);
                WritePrivateProfileString("Mail", "SMTP_PASSWORD", "", this.inipath);
                WritePrivateProfileString("Mail", "FROM_ADDRESS", "", this.inipath);
                WritePrivateProfileString("Mail", "TO_ADDRESS", "", this.inipath);
                // return "第一次使用，已生成Config.ini";
            }
            else
            {
                // return "已存在Config.ini，出现异常请删除该文件后重启软件";
            }

        }
    }
}
