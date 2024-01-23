using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryModel
    {
        private OnlineShopDbContext context;

        public CategoryModel()
        {
            context = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            var list = context.Database.SqlQuery<Category>("Sp_Category_ListAll").ToList();

            return list;
        }

        public int Create(string name, string alias, int? parentID, int? order, bool? status)
        {
            object[] parameters =
            {
                new SqlParameter("@Name",name) ,
                new SqlParameter("@Alias",alias) ,
                new SqlParameter("@ParentID",parentID) ,
                new SqlParameter("@Order",order) ,
                new SqlParameter("@Status",status)
            };

            var res = context.Database.ExecuteSqlCommand("Sp_Category_Insert @Name , @Alias , @ParentID , @Order , @Status", parameters);
            return res;
        }
    }
}
