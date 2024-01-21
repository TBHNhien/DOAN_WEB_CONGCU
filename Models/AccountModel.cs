using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AccountModel
    {
        private OnlineShopDbContext context;

        public AccountModel()
        {
            context = new OnlineShopDbContext();
        }
        public bool Login(string userName, string password)
        {
            //truyền tham số vào bằng cách tạo ra đối tượng
            object[] sqlParams = 
            {
                new SqlParameter ("@UserName",userName),
                new SqlParameter ("@Password", password)
            };
            var res = context.Database.SqlQuery<bool>("Sp_Account_Login @UserName,@Password", sqlParams).SingleOrDefault();  //thu tuc SqlQuery
            return res;
        }

    }
}
