using System.Drawing.Text;
using System.Drawing;
using System.IO;

namespace HiDesktopTests
{
    public partial class Form1 : Form
    {
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
            try
            {
                //从外部文件加载字体文件
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(@"C:\Users\Administrator\source\repos\HiDesktop\HiDesktop\bin\Debug\net9.0-windows\Fonts\HLFont.ttf");

                //定义成新的字体对象
                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                Font myFont = new Font(myFontFamily, label1.Font.Size, FontStyle.Regular);

                //将字体显示到控件
                label1.Font = myFont;

            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "异常：", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
