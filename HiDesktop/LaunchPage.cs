using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace HiDesktop
{
    public partial class LaunchPage : Form
    {
        public LaunchPage()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Thread thread = new Thread(new ThreadStart(Initializate));
            thread.Start();
        }

        void Initializate()
        {
            progressBar.Value = 20;
            ProcessText.Text = "程序正在启动-Program loading...";
            Thread MainThread = new Thread(new ThreadStart(Program.MainProcess));
            Thread.Sleep(2500);
            progressBar.Value = 75;
            ProcessText.Text = "线程构建完成-Thread built... ";
            MainThread.Start();
            Thread.Sleep(3000);
            progressBar.Value = 100;
            ProcessText.Text = "启动完成-Finished";
            Thread.Sleep(1000);
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
