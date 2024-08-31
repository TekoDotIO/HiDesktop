using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.WindowApps.RandomItemsDataModel;
using Widgets.MVP.WindowApps.RandomItemsDataModel.ExcelDataExchangeModels;

namespace Widgets.MVP.WindowApps
{
    public partial class RandomItemsDbHelper : Form
    {
        public RandomItemsDbHelper()
        {
            InitializeComponent();
        }
        public RandomItemsDbHelper(string path)
        {
            InitializeComponent();
            PathBox.Text = path;
        }

        private void TestConnectBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.
                try
                {
                    string path = PathBox.Text;
                    RandomItemsDbExcelProcessor ep = new(path);
                    ep.Initialize();
                    Log.SaveLog("Loaded excel file.", "RandomItemHelper");
                    ep.BindDataToDataGridView(PreviewBox);
                    TipLabel.ForeColor = Color.Green;
                    TipLabel.Text = "连接成功建立！";
                }
                catch (Exception ex)
                {
                    TipLabel.ForeColor = Color.Red;
                    TipLabel.Text = "连接错误！";
                    var r = MessageBox.Show($"测试连接时出现异常：{ex}\n\n是否调试？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    Log.SaveLog($"Exception while testing conn:{ex}", "RandomItemHelper");
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }

                }

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.
                try
                {
                    string path = PathBox.Text;
                    if (File.Exists(path))
                    {
                        var r = MessageBox.Show("此位置的数据库已经存在，是否覆盖并重新建立？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.No)
                        {
                            return;
                        }

                    }
                    RandomItemsDbExcelProcessor.CreateExcelDb(path);
                    TipLabel.ForeColor = Color.Green;
                    TipLabel.Text = "文件已创建！";
                }
                catch (Exception ex)
                {
                    TipLabel.ForeColor = Color.Red;
                    TipLabel.Text = "创建发生错误！";
                    var r = MessageBox.Show($"创建时出现异常：{ex}\n\n是否调试？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    Log.SaveLog($"Exception while creating excel:{ex}", "RandomItemHelper");
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }

                }

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditorReadByIDBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.
                try
                {
                    string path = PathBox.Text;
                    RandomItemsDbExcelProcessor ep = new(path);
                    ep.Initialize();
                    Log.SaveLog("Loaded excel file.", "RandomItemHelper");
                    var data = ep.GetRowByID(Convert.ToInt32(EditorID.Text));
                    EditorName.Text = data.Name;
                    EditorTags.Text = data.Tags;
                    EditorPoolWeight.Text = data.PoolWeight.ToString();
                    ep.BindDataToDataGridView(PreviewBox);
                    TipLabel.ForeColor = Color.Green;
                    TipLabel.Text = "读取已完成。";
                }
                catch (Exception ex)
                {
                    TipLabel.ForeColor = Color.Red;
                    TipLabel.Text = "错误！";
                    var r = MessageBox.Show($"出现异常：{ex}\n\n是否调试？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    Log.SaveLog($"Exception:{ex}", "RandomItemHelper");
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }

                }

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditorReadByNameBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.
                try
                {
                    string path = PathBox.Text;
                    RandomItemsDbExcelProcessor ep = new(path);
                    ep.Initialize();
                    Log.SaveLog("Loaded excel file.", "RandomItemHelper");
                    var data = ep.GetRowByName(EditorName.Text);
                    EditorID.Text = data.ID.ToString();
                    EditorTags.Text = data.Tags;
                    EditorPoolWeight.Text = data.PoolWeight.ToString();
                    ep.BindDataToDataGridView(PreviewBox);
                    TipLabel.ForeColor = Color.Green;
                    TipLabel.Text = "读取已完成。";
                }
                catch (Exception ex)
                {
                    TipLabel.ForeColor = Color.Red;
                    TipLabel.Text = "错误！";
                    var r = MessageBox.Show($"出现异常：{ex}\n\n是否调试？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    Log.SaveLog($"Exception:{ex}", "RandomItemHelper");
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }

                }

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditorSaveBtn_Click(object sender, EventArgs e)
        {
            if (UseExcelBox.Checked)
            {
                //use excel.
                try
                {
                    string path = PathBox.Text;
                    RandomItemsDbExcelProcessor ep = new(path);
                    ep.Initialize();
                    Log.SaveLog("Loaded excel file.", "RandomItemHelper");
                    DbDataRowObj obj = new()
                    {
                        ID = Convert.ToInt32(EditorID.Text),
                        Name = EditorName.Text,
                        Tags = EditorTags.Text,
                        PoolWeight = Convert.ToInt32(EditorPoolWeight.Text)
                    };
                    ep.ModifyByID(obj);
                    ep.BindDataToDataGridView(PreviewBox);
                    TipLabel.ForeColor = Color.Green;
                    TipLabel.Text = "写入已完成。";
                }
                catch (Exception ex)
                {
                    TipLabel.ForeColor = Color.Red;
                    TipLabel.Text = "写入时发生错误！";
                    var r = MessageBox.Show($"写入时出现异常：{ex}\n\n是否调试？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    Log.SaveLog($"Exception:{ex}", "RandomItemHelper");
                    if (r == DialogResult.Yes)
                    {
                        throw;
                    }

                }

            }
            else
            {
                //use sqlite.
                MessageBox.Show("SQLite模块正在开发，敬请期待。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
