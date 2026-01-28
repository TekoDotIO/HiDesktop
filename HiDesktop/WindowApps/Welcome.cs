using ReaLTaiizor.Colors;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Widgets.MVP
{
    public partial class Welcome : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public Welcome()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            //materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.LIGHT);
            //Refresh();
            if (Font != null)
            {
                MessageBox.Show(Font.ToString());
            }
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Timer t = new Timer { Interval = 10 };

            //t.Tick += (e, sender) =>
            //{
            //    label2
            //};
            label2.Visible = false;
            AgreedCB.Visible = false;
            StartConfigBtn.Enabled = true;
        }

        private void StartConfigBtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectTab(1);
        }
    }
}
