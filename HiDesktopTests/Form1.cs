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
            label1.Text = "��������";
            Refresh();
            Thread.Sleep(1000);

            label1.Text = "GetAwayGetAwayGetAwayGetAway";
            Refresh();
            Thread.Sleep(1000);
            label1.Text = "��������õ�";
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //���ⲿ�ļ����������ļ�
                PrivateFontCollection font = new PrivateFontCollection();
                font.AddFontFile(@"C:\Users\Administrator\source\repos\HiDesktop\HiDesktop\bin\Debug\net9.0-windows\Fonts\HLFont.ttf");

                //������µ��������
                FontFamily myFontFamily = new FontFamily(font.Families[0].Name, font);
                Font myFont = new Font(myFontFamily, label1.Font.Size, FontStyle.Regular);

                //��������ʾ���ؼ�
                label1.Font = myFont;

            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "�쳣��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
