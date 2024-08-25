using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Data.SQLite.EF6;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using Widgets.MVP.WindowApps.RandomItemsDataModel.Models;

namespace Widgets.MVP.WindowApps.RandomItemsDataModel
{
    public class RandomItemsDbContext : DbContext
    {
        public DbSet<Config> Config { get; set; }
        public DbSet<Items> Items { get; set; }
        public string path;
        /// <summary>
        /// 初始化实体链接
        /// </summary>
        /// <param name="path">数据库路径</param>
        public RandomItemsDbContext(string path) : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder()
            { DataSource = path, ForeignKeys = true }
                        .ConnectionString
        }, true)
        {
            this.path = path;
        }



        public void Initialize()
        {
            Database.Initialize(force: false);
        }


        /// <summary>
        /// 加载实体并绑定实体类
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>().HasKey(c => c.ID);
            modelBuilder.Entity<Items>().HasKey(c => c.ID);
            // 其他实体类的主键定义
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<RandomItemsDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// 预加载数据库对象
        /// </summary>
        /// <returns></returns>
        public void PreloadDb()
        {
            object _ = Config.ToList();
            _ = Items.ToList();

        }
    }
}
