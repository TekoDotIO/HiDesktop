using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.MVP.WindowApps.RandomItemsDataModel.Models
{
    public class Config
    {
        /// <summary>
        /// 配置项索引（主键）
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 配置项键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 配置项值
        /// </summary>
        public string Value { get; set; }
    }
}
