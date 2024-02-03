﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["OnlineShopDbContext"].ToString();

            return db.Categories.Where(x => x.Status == true).ToList();
        }
    }
}
