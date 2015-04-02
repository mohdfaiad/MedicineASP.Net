using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENetCare.Web.UserControl
{
    public class BarCodeAddValidateEventArgs : EventArgs
    {
        public Package Package { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}