using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Code
{
    //luu  thông tin session vào section
    //cần thuộc tính Serializable =>   tuần tự hoá ra nhị phân
    [Serializable]
    public class UserSession
    {
        public string UserName { set; get; }

    }
}