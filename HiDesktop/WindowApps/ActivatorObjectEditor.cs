using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Widgets.MVP.Essential_Repos;
using Widgets.MVP.WidgetModels.ActivatorDataModel.Models;

namespace Widgets.MVP.WidgetModels
{
    public partial class ActivatorObjectEditor : Form
    {
        public ActivatorDataModel.ActivatorDbContext dataSrc;
        public string ID = "";
        public string description = "";
        public string imgPath = "";
        public string action = "";
        public ActivatorObjectEditor()
        {
            InitializeComponent();
            this.Enabled = true;
            foreach (var item in Controls)
            {
                if (item.GetType() == typeof(Button))
                {
                    ((Button)item).Enabled = false;
                }
            }
            this.tips.Text = "正在验证数据库连接...";
            this.tips.ForeColor = Color.Yellow;
        }



        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new();
            fd.InitialDirectory = "C:/";
            fd.Filter = "图标文件|*.ico;*.png";
            fd.FilterIndex = 1;  // 默认显示图标文件类型
            fd.RestoreDirectory = true;  // 对话框关闭后是否还原当前目录
            if (fd.ShowDialog() == DialogResult.OK)
            {
                // 用户选择了文件
                string selectedFileName = fd.FileName;
                iconBox.Text = selectedFileName;
                try
                {
                    iconDisplay.Image = Bitmap.FromFile(selectedFileName);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"{ex}", "AOEditor");
                    MessageBox.Show($"应用图标时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private void ActivatorObjectEditor_Load(object sender, EventArgs e)
        {
            Thread loadingThread = new(new ThreadStart(() =>
            {
                try
                {
                    dataSrc.PreloadDb();
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"{ex}", "AOEditor");
                    Invoke((MethodInvoker)delegate ()
                    {
                        tips.Text = "数据库连接错误！检查日志文件以获取详细信息...";
                        tips.ForeColor = Color.Red;
                    });


                }
                Invoke((MethodInvoker)delegate ()
                {
                    idBox.Text = ID;
                    descriptionBox.Text = description;
                    actionBox.Text = action;
                    iconBox.Text = imgPath;
                    tips.Text = "数据库连接成功！";
                    tips.ForeColor = Color.Green;
                    Enabled = true;
                    foreach (var item in Controls)
                    {
                        if (item.GetType() == typeof(Button))
                        {
                            ((Button)item).Enabled = true;
                        }
                    }
                });

            }));
            loadingThread.IsBackground = true;
            loadingThread.Start();
        }

        private void iconBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iconDisplay.Image = Bitmap.FromFile(iconBox.Text);
            }
            catch (Exception ex)
            {
                Log.SaveLog($"{ex}", "AOEditor");
                MessageBox.Show($"应用图标时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        void Save()
        {
            try
            {
                int id = Convert.ToInt32(idBox.Text);
                var v = dataSrc.Repo.FirstOrDefault(p => p.ID == id);
                if (v != null)
                {
                    v.Description = descriptionBox.Text;
                    v.Action = actionBox.Text;
                    v.Icon = iconBox.Text;

                }
                else
                {
                    var dbSet = new ActivatorDataModel.Models.Repo();
                    dbSet.Description = descriptionBox.Text;
                    dbSet.Action = actionBox.Text;
                    dbSet.Icon = iconBox.Text;
                    dbSet.ID = Convert.ToInt32(idBox.Text);
                    dataSrc.Repo.Add(dbSet);
                }
                dataSrc.SaveChanges();
                MessageBox.Show("保存完成！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveLog($"保存时发生错误！\n{ex}", "AOEditor");
                //throw;
            }

        }


        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActivatorObjectEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveBtn.Enabled)
            {
                var r = MessageBox.Show("是否保存更改？", "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (r == DialogResult.Yes)
                {
                    Save();
                }
                if (r == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

        }

        private void loadFromDbBtn_Click(object sender, EventArgs e)
        {
            Repo v;
            try
            {
                int id = Convert.ToInt32(idBox.Text);
                v = dataSrc.Repo.FirstOrDefault(p => p.ID == id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索对象时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveLog($"{ex}", "AOEditor");
                return;
            }

            if (v != null)
            {
                idBox.Text = v.ID.ToString();
                iconBox.Text = v.Icon;
                descriptionBox.Text = v.Description;
                actionBox.Text = v.Action;
                try
                {
                    iconDisplay.Image = Bitmap.FromFile(iconBox.Text);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"{ex}", "AOEditor");
                    MessageBox.Show($"应用图标时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            else
            {
                MessageBox.Show($"数据库中未发现ID对应的数据记录。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void iconBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void iconBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    iconDisplay.Image = Bitmap.FromFile(iconBox.Text);
                }
                catch (Exception ex)
                {
                    Log.SaveLog($"{ex}", "AOEditor");
                    MessageBox.Show($"应用图标时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DelWithID_Click(object sender, EventArgs e)
        {
            Repo v;
            try
            {
                int id = Convert.ToInt32(idBox.Text);
                v = dataSrc.Repo.FirstOrDefault(p => p.ID == id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索对象时发生错误！\n{ex}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.SaveLog($"{ex}", "AOEditor");
                return;
            }

            if (v != null)
            {
                dataSrc.Repo.Remove(v);
                dataSrc.SaveChanges();
                MessageBox.Show("删除操作执行完成！", "删除操作", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"数据库中未发现ID对应的数据记录。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
