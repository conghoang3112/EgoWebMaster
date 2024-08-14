using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums.EnumAttribute;

namespace TLS.Common.Enums
{
    public enum NewsType
    {
        [EnumDisplayName(DisplayName ="KIẾN THỨC XÂY DỰNG BỂ BƠI")]
        KNOWLEDGE = 1,
        [EnumDisplayName(DisplayName = "DỊCH VỤ")]
        SERVICE =2,
        [EnumDisplayName(DisplayName = "CẬP NHẬT TIẾN ĐỘ CÔNG TRÌNH")]
        PROGRESS =3,
    }
}
