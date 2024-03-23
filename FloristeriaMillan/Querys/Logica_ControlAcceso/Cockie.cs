using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace FloristeriaMillan.Querys.Logica_ControlAcceso
{
    public class Cockie
    {
        private const string ValidationKey = "D36495A9B6C293C5D402DF5A3A9D9A1142EDFEEF7D558DF0E6CCFA01A35A4E9A";
        private const string DecryptionKey = "0F19D682CF50C2D9AA309E17A9F56498249C78A8AF90FD4A";

        public string EncryptString(string text)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = MachineKey.Protect(clearBytes, ValidationKey, DecryptionKey);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] clearBytes = MachineKey.Unprotect(encryptedBytes, ValidationKey, DecryptionKey);
            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}