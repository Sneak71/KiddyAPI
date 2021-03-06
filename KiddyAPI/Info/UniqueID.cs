﻿/////////////////////////////////////////////////////////
//// Developer : Whiro
//// For for educational purposes
//////////////////////////////////////////////////////////
using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace KiddyAPI.Info
{
    /// <summary>
    /// Class to get UniqueId
    /// </summary>
    public class UniqueID
    {
        /// <summary>
        /// Get hash summ hardware id
        /// </summary>
        /// <returns>Hash in string value</returns>
        public static string GetUniqueHardwaeId()
        {
            StringBuilder sb = new StringBuilder();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["NumberOfCores"]);
                sb.Append(queryObj["ProcessorId"]);
                sb.Append(queryObj["Name"]);
                sb.Append(queryObj["SocketDesignation"]);
            }

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["Manufacturer"]);
                sb.Append(queryObj["Name"]);
                sb.Append(queryObj["Version"]);

            }

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["Product"]);
            }

            var bytes = Encoding.ASCII.GetBytes(sb.ToString());
            SHA256Managed sha = new SHA256Managed();

            byte[] hash = sha.ComputeHash(bytes);

            return BitConverter.ToString(hash);
        }
    }
}
