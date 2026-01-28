//using MaterialSkin;
//using MaterialSkin.Controls;

using ReaLTaiizor.Colors;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace HiDesktopTests
{
    public partial class Form1 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        ContextMenuStrip? m;
        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            if (Font != null)
            {
                MessageBox.Show(Font.ToString());
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {
            Refresh();
            Thread.Sleep(1000);

            Refresh();
            Thread.Sleep(1000);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    //从外部文件加载字体文件
            //    PrivateFontCollection font = new PrivateFontCollection();
            //    font.AddFontFile(@"C:\Users\Administrator\source\repos\HiDesktop\HiDesktop\bin\Debug\net9.0-windows\Fonts\HLFont.ttf");

            //    //定义成新的字体对象
            //    FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
            //    Font myFont = new Font(myFontFamily, label1.Font.Size, FontStyle.Regular);

            //    //将字体显示到控件
            //    label1.Font = myFont;

            //}
            //catch (InvalidCastException ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), "异常：", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            
            materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.LIGHT);
            Refresh();
            PoisonStyleManager p = new();
            p.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            PoisonColors.Custom = ColorTranslator.FromHtml("#445862");
            poisonProgressSpinner1.UseCustomBackColor = true;
            poisonProgressSpinner1.Value = 0;
            poisonProgressSpinner1.BackColor = this.BackColor;
            poisonProgressSpinner1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Custom;
            poisonProgressSpinner1.Speed = 2F;
            poisonProgressSpinner1.LineWidthRatio = 20;
            CheckForIllegalCrossThreadCalls = false;
            Thread animePPSUpdater = new(new ThreadStart(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 80; i++)
                    {
                        poisonProgressSpinner1.Value++;
                        Thread.Sleep(10);
                    }
                    for (int i = 0; i < 80; i++)
                    {
                        poisonProgressSpinner1.Value--;
                        Thread.Sleep(10);
                    }
                }
            }));
            animePPSUpdater.Start();
            //int status = 1;
            //poisonProgressBar1.MarqueeAnimationSpeed = 50;
            //poisonProgressBar1.ProgressBarMarqueeWidth = 0;
            //poisonProgressBar1.EnableMaterialMarqueeStyleSpeed = true;
            //poisonProgressBar1.MaterialStyleMarqueeSpeedRatio = 3;
            
            //System.Windows.Forms.Timer t = new System.Windows.Forms.Timer { Interval = 250 };
            //t.Tick += ((sender, e) =>
            //{
            //    if (status > 4)
            //    {
            //        status = 0;
            //        poisonProgressBar1.MarqueeAnimationSpeed = 25;
            //        return;
            //    }
            //    if (status == 0)
            //    {
            //        poisonProgressBar1.MarqueeAnimationSpeed = 100;
            //    }
            //    status++;
            //});
            //t.Start();
            //Thread animePBUpdater = new(new ThreadStart(() =>
            //{


            //}));
            //animePBUpdater.Start();
        }

        
        private void t1Method(object? sender, EventArgs e)
        {
            //MessageBox.Show("ok!");
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            //poisonProgressSpinner1.Value = poisonProgressSpinner1.Value == 100 ? 0 : poisonProgressSpinner1.Value + 10;
            //MaterialContextMenuStrip mcms = new();
            //poisonProgressBar1.Invoke(() =>
            //{
            //    poisonProgressBar1.MarqueeAnimationSpeed = 10;
            //});
            //mcms.Items.Add(new ToolStripMenuItem("B"));
            //this.ContextMenuStrip = mcms;
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = "HiDesktopTest";
            m = new MaterialContextMenuStrip();
            ToolStripMenuItem t1 = new("t1");
            m.Items.Add(t1);
            notifyIcon.ContextMenuStrip = m;
            t1.Click += new EventHandler(t1Method);
            notifyIcon.Click += new EventHandler((object? sender, EventArgs e) =>
            {
                m.Show(Cursor.Position.X, Cursor.Position.Y);
            });
            notifyIcon.Visible = true;
        }
    }
}
