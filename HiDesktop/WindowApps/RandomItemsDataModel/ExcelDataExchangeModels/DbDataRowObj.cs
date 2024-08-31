using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.MVP.WindowApps.RandomItemsDataModel.ExcelDataExchangeModels
{
    public class DbDataRowObj
    {
        public DbDataRowObj() { }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public int PoolWeight { get; set; }
    }
}
