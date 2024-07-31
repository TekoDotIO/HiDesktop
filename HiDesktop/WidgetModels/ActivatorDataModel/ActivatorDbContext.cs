using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite.EF6;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.Entity.Core;
using Widgets.MVP.WidgetModels.ActivatorDataModel.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using SQLite.CodeFirst;

namespace Widgets.MVP.WidgetModels.ActivatorDataModel
{
    internal class ActivatorDbContext : DbContext
    {
        public DbSet<Config> Config { get; set; }
        public DbSet<Repo> Repo { get; set; }
        public string path;
        /// <summary>
        /// 初始化实体链接
        /// </summary>
        /// <param name="path">数据库路径</param>
        public ActivatorDbContext(string path) : base(new SQLiteConnection()
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
            modelBuilder.Entity<Repo>().HasKey(c => c.ID);
            // 其他实体类的主键定义
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ActivatorDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// 异步预加载数据库对象
        /// </summary>
        /// <returns></returns>
        public async Task PreloadDb()
        {
            Task preloadTask = Task.Run(() => {
                object _ = Config.ToList();
                _ = Repo.ToList();
            });

            await preloadTask;
        }
    }
}
