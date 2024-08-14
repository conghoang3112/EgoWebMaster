using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.Common.Enums.EnumAttribute
{
    public class EnumDisplayNameAttribute : Attribute
    {
        private string _displayName;
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }
    }
}
