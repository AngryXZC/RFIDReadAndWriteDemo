using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Reader;
using System.Windows.Forms;
using SampleServer.Tool;

namespace SampleServer
{
    internal class SingleDevice
    {

        /// <summary>
        /// 当前要写的标签
        /// </summary>
        RXInventoryTag writeTag;
        /// <summary>
        /// 当前写入的数据
        /// </summary>
        string[] codes;
        /// <summary>
        /// 是否连接
        /// </summary>
        bool isConnected=false;
        /// <summary>
        /// 是否开启循环读取数据
        /// </summary>
        public bool isLoop = false;
        /// <summary>
        /// 错误信息
        /// </summary>
        private  string strException = string.Empty;
        /// <summary>
        /// 串口号
        /// </summary>
        string strComPort;
        /// <summary>
        /// 波特率
        /// </summary>
        int nBaudrate = 115200;
        public ReaderMethod reader { get; }
        public bool IsConnected { get => isConnected; set => isConnected = value; }
        public RXInventoryTag WriteTag { get => writeTag; set => writeTag = value; }


        /// <summary>
        /// 单例模式
        /// </summary>
        private static SingleDevice singleDevice = new SingleDevice();
        private SingleDevice()
        {
            INIParser ini = new INIParser();
            ini.Open(@"config.ini");
            strComPort = ini.ReadValue("COM", "COM", "NULL");
            ini.Close();
            WriteTag = null;
            //strComPort = ConfigurationManager.AppSettings["COM"];
            reader = new ReaderMethod();
            reader.m_OnInventoryTag = onInventoryTag;
            reader.m_OnInventoryTagEnd = onInventoryTagEnd;
            reader.m_OnExeCMDStatus = onExeCMDStatus;
            //reader.m_RefreshSetting = refreshSetting;
            reader.m_OnOperationTag = onOperationTag;
            reader.m_OnOperationTagEnd = onOperationTagEnd;
            //reader.m_OnFastSwitchAntInventoryTagEnd = onFastSwitchAntInventoryTagEnd;
            //reader.m_OnGetInventoryBufferTagCount = onGetInventoryBufferTagCount;
            //reader.m_OnInventory6BTag = onInventory6BTag;
            //reader.m_OnInventory6BTagEnd = onInventory6BTagEnd;
            //reader.m_OnRead6BTag = onRead6BTag;
            //reader.m_OnWrite6BTag = onWrite6BTag;
            //reader.m_OnLock6BTag = onLock6BTag;
            //reader.m_OnLockQuery6BTag = onLockQuery6BTag;
            //reader.ReceiveCallback = onReceiveCallback;

        }
        public static SingleDevice getInstance()  { return singleDevice; }
        #region 设备的一系列操作函数
        /// <summary>
        /// 打开读写器
        /// </summary>
        public void connect()
        {
            //Processing serial port to connect reader.
           
            int nRet = reader.OpenCom(strComPort, nBaudrate, out strException);
            if (nRet != 0)
            {
                string strLog = "Connection failed, failure cause: " + strException;
                IsConnected=false;
                Console.WriteLine(strLog);
               
            }
            else
            {
                string strLog = "Connect" + strComPort + "@" + nBaudrate.ToString();
                Console.WriteLine("读写器已连接:"+strLog);
                reader.SetBeeperMode((byte)0xFF, (byte)0x00);
                IsConnected=true;
              
            }
        }
        /// <summary>
        /// 关闭读写器
        /// </summary>
        public void disconnect() {
            if (IsConnected)
            {
                reader.CloseCom();
                IsConnected=false;
            }
            else {
                MessageBox.Show("请检查读写器连接！");
            }
        }
        /// <summary>
        /// 开始盘存
        /// </summary>
        /// <returns></returns>
        public bool startInventoryReal() 
        {
            if (reader.InventoryReal((byte)0xFF, (byte)0xFF) != 0)
            {
                return false;
            }
            else {
                isLoop = true;
                return true;
            }
           
           
        }
        /// <summary>
        /// 停止盘存
        /// </summary>
        public void stopInventory() {
            isLoop = false;
        }
        /// <summary>
        /// 写操作
        /// </summary>
        /// <param name="inputTag">写的编号</param>
        public void writeCurrentTag(string inputTag) {
           
            string[] reslut = DataConvert.StringToStringArray(WriteTag.strEPC, 2); 
            byte[] btAryEpc = DataConvert.StringArrayToByteArray(reslut, reslut.Length);
            int match=reader.SetAccessEpcMatch(0xFF, 0x00, Convert.ToByte(btAryEpc.Length), btAryEpc);
            codes= DataConvert.StringToStringArray(inputTag, 2);
            int writeRes=reader.WriteTag((byte)0xFF, DataConvert.StringToByteArray("00 00 00 00 00"), (byte)0x01, (byte)0x02, (byte)4, DataConvert.StringArrayToByteArray(codes,codes.Length));
           

        }
        #endregion




        #region Reader中的委托

        void onReceiveCallback(byte[] btAryReceiveData)
        {
            string str = "";
            for (int i = 0; i < btAryReceiveData.Length; i++)
            {
                str += Convert.ToString(btAryReceiveData[i], 16) + "  ";
            }
            //注释掉没用的输出！
            Console.WriteLine("cmd data ： " + str);
        }

        void refreshSetting(ReaderSetting readerSetting)
        {
            Console.WriteLine("Version:" + readerSetting.btMajor + "." + readerSetting.btMinor);
        }

        void onExeCMDStatus(byte cmd, byte status)
        {
            if (isLoop && (cmd == CMD.REAL_TIME_INVENTORY))
            {
                reader.InventoryReal((byte)0xFF, (byte)0xFF);
            }
          
            Console.WriteLine("CMD execute CMD:" + CMD.format(cmd) + "++Status code:" + ERROR.format(status));
            Console.WriteLine("cmd:"+cmd);
            Console.WriteLine("status"+status);
            if (CMD.WRITE_TAG == cmd&& status == ERROR.TAG_WRITE_ERROR)
            {       
                    string[] reslut = DataConvert.StringToStringArray(WriteTag.strEPC, 2);
                    byte[] btAryEpc = DataConvert.StringArrayToByteArray(reslut, reslut.Length);
                    reader.SetAccessEpcMatch(0xFF, 0x00, Convert.ToByte(btAryEpc.Length), btAryEpc);
                    reader.WriteTag((byte)0xFF, DataConvert.StringToByteArray("00 00 00 00 00"), (byte)0x01, (byte)0x02, (byte)4, DataConvert.StringArrayToByteArray(codes, codes.Length));
            }
        }

        void onInventoryTag(RXInventoryTag tag)
        {
            Console.WriteLine("调用盘点！");

            WriteTag = tag;
            
            Console.WriteLine("Inventory EPC:" + tag.strEPC);
            Console.WriteLine("Inventory Ant:" + tag.btAntId);

        }

        void onInventoryTagEnd(RXInventoryTagEnd tagEnd)
        {
            if (isLoop)
            {
                reader.InventoryReal((byte)0xFF, (byte)0xFF);
            }
            else {
                Console.WriteLine("关闭盘点！");
            }
        }

        void onFastSwitchAntInventoryTagEnd(RXFastSwitchAntInventoryTagEnd tagEnd)
        {
            Console.WriteLine("Fast Inventory end:" + tagEnd.mTotalRead);
        }

        void onInventory6BTag(byte nAntID, String strUID)
        {
            Console.WriteLine("Inventory 6B Tag:" + strUID);
        }

        void onInventory6BTagEnd(int nTagCount)
        {
            Console.WriteLine("Inventory 6B Tag:" + nTagCount);
        }

        void onRead6BTag(byte antID, String strData)
        {
            Console.WriteLine("Read 6B Tag:" + strData);
        }

        void onWrite6BTag(byte nAntID, byte nWriteLen)
        {
            Console.WriteLine("Write 6B Tag:" + nWriteLen);
        }

        void onLock6BTag(byte nAntID, byte nStatus)
        {
            Console.WriteLine("Lock 6B Tag:" + nStatus);
        }

        void onLockQuery6BTag(byte nAntID, byte nStatus)
        {
            Console.WriteLine("Lock query 6B Tag:" + nStatus);
        }

        void onGetInventoryBufferTagCount(int nTagCount)
        {
            Console.WriteLine("Get Inventory Buffer Tag Count" + nTagCount);
        }

        void onOperationTag(RXOperationTag tag)
        {
            Console.WriteLine("Operation Tag" + tag.strData);
        }

        void onOperationTagEnd(int operationTagCount)
        {
            Console.WriteLine("Operation Tag End" + operationTagCount);
            if (operationTagCount==1) {
                //开启下一标签的写入
                MessageBox.Show("写入成功！");
                WriteTag = null;
                //disconnect();
                //connect();
                startInventoryReal();
            }

        }

        #endregion

    }
}
