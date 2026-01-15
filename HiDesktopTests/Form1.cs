using System.Drawing.Text;
using System.Drawing;
using System.IO;

namespace HiDesktopTests
{
    public partial class Form1 : Form
    {
        ContextMenuStrip? m;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "你在这里";
            Refresh();
            Thread.Sleep(1000);

            label1.Text = "GetAwayGetAwayGetAwayGetAway";
            Refresh();
            Thread.Sleep(1000);
            label1.Text = "这个世界会好的";
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = "HiDesktopTest";
            m = new ContextMenuStrip();
            ToolStripMenuItem t1 = new("t1");
            m.Items.Add(t1);
            notifyIcon.ContextMenuStrip = m;
            t1.Click += new EventHandler(t1Method);
            notifyIcon.Click += new EventHandler((object? sender,EventArgs e) =>
            {
                m.Show(Cursor.Position.X, Cursor.Position.Y);
            });
            notifyIcon.Visible = true;
        }
        private void t1Method(object? sender, EventArgs e)
        {
            MessageBox.Show("ok!");
        }
    }
}
