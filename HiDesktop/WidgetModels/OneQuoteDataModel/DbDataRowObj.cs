using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.MVP.WidgetModels.OneQuoteDataModel
{
    public class DbDataRowObj
    {
        public DbDataRowObj() { }
        public int ID { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Ahead { get; set; }
        public string TextFont { get; set; }
        public string AuthorFont { get; set; }
        public string ColorID { get; set; }

    }
}
