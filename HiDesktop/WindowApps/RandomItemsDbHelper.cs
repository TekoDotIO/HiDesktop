using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Widgets.MVP.WindowApps
{
    public partial class RandomItemsDbHelper : Form
    {
        public RandomItemsDbHelper()
        {
            InitializeComponent();
        }

        private void TestConnectBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
