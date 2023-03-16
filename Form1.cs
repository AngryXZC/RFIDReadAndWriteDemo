using Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleServer
{
    public partial class Form1 : Form
    {
        

        public ReaderMethod reader = new ReaderMethod();

       
        SingleDevice singleDevice = null;
        public Form1()
        {
           
            InitializeComponent();
            singleDevice=SingleDevice.getInstance();
            refreshBtnStatus();
        }



        /// <summary>
        /// 连接读写器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Click(object sender, EventArgs e)
        {

           
            singleDevice.connect();
            if (singleDevice.IsConnected)
            {
                MessageBox.Show("读写器连接成功！");
                this.inputSample.Text = "0020231003000101";
                refreshBtnStatus();
            }
            else {
                MessageBox.Show("读写器连接失败！");
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            singleDevice.disconnect();
            if (!singleDevice.IsConnected)
            {
                MessageBox.Show("读写器断开成功！");
                refreshBtnStatus();
            }
            else
            {
                MessageBox.Show("读写器断开失败！");
            }
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            singleDevice.startInventoryReal();
            refreshBtnStatus();
        }

        private void stopInventoryBtn_Click(object sender, EventArgs e)
        {
            singleDevice.stopInventory();
            refreshBtnStatus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += Form1_FormClosing;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(singleDevice.IsConnected)
                singleDevice.disconnect();
            MessageBox.Show("即将关闭窗体。。。");
        }

        private void writeBtn_Click(object sender, EventArgs e)
        {
            if ((!singleDevice.IsConnected)) {
                MessageBox.Show("请检查设备连接！");
                return;
            }
            if ((singleDevice.WriteTag is null)) 
            {
                MessageBox.Show("读写器当前未读到任何标签！");
                return;
            }
            if (this.inputSample.Text.Length != 16)
            {
                MessageBox.Show("请输入16位样品编码！");
                return;
            }
            else {
                 singleDevice.writeCurrentTag(this.inputSample.Text);
            }
            //reader.WriteTag((byte)0xff, new byte[] { (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00 }, (byte)0x01, (byte)0x02, (byte)0x04, new byte[] { (byte)0x00, (byte)0x20, (byte)0x23, (byte)0x10, (byte)0x03, (byte)0x00, (byte)0x01, (byte)0x00 });
            // singleDevice.writeCurrentTag();
        }

        private void refreshBtnStatus() {
            this.InventoryBtn.Enabled = (singleDevice.IsConnected && !singleDevice.isLoop);
            this.stopInventoryBtn.Enabled = (singleDevice.IsConnected && singleDevice.isLoop);
            this.writeBtn.Enabled = (singleDevice.IsConnected);

        }
        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputSample_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && (!Char.IsControl(e.KeyChar)))

            {

                e.Handled = true;

            }

            
        }
    }
}
