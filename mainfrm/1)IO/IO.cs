using NMCMotionSDK;

using System;
using System.Threading;
namespace mainfrm
{
    using NMC = NMCSDKLib;
    using Status = NMCSDKLib.MC_STATUS;
    public class IO
    {
        private IO()
        {
            inputs = new bool[Global.IOMax];
            outputs = new bool[Global.IOMax];
        }
        public static IO Getinstance()
        {
            if (io == null) io = new IO();
            return io;
        }

        static IO io;
        bool[] inputs;
        bool[] outputs;
        Thread runthread;
        bool bRunthread;
        public bool[] X=> inputs;
        public bool[] Y => outputs;
        public event Action<string> SendMsgHandler;
        public void OnMsg(string msg) => SendMsgHandler?.Invoke(msg);
        public bool BoardInit()
        {
            Status ms = NMC.MC_MasterInit(Global.BoardID);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO Board 초기화 중 에러 : {ms}");
                return false;
            }
            OnMsg($"IO Board 초기화 상태 : {ms}");
            return true;
        }
        public bool BoardRun()
        {
            Status ms = NMC.MC_MasterInit(Global.BoardID);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO Board 초기화 에러 발생 : {ms}");
                return false;
            }
            OnMsg($"IO Board 초기화 정상 진행 : {ms}");
            return true;
        }

        public bool On(eY bit)
        {
            uint offset = (uint)((int)bit / 8);
            byte bitOffset = (byte)((int)bit % 8);
            Status ms = NMC.MC_IO_WRITE_BIT(Global.BoardID, Global.eCatAddr, offset, bitOffset, true);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO On 실패 : {ms}");
                return false;
            }
            return true;
        }

        public bool Off(eY bit)
        {
            uint offset = (uint)((int)bit / 8);
            byte bitOffset = (byte)((int)bit % 8);
            Status ms = NMC.MC_IO_WRITE_BIT(Global.BoardID, Global.eCatAddr, offset, bitOffset, false);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO off 실패 : {ms}");
                return false;
            }
            return true;
        }

        public bool IOUpdate()
        {
            uint indata = 0;
            uint outdata = 0;
            Status ms = NMC.MC_IO_READ_DWORD(Global.BoardID, Global.eCatAddr, 1, 4, ref indata);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO In 리딩 실패 : {ms}");
                return false;
            }
            ms = NMC.MC_IO_READ_DWORD(Global.BoardID, Global.eCatAddr, 0, 0, ref outdata);
            if (ms != Status.MC_OK)
            {
                OnMsg($"IO Out 리딩 실패 : {ms}");
                return false;
            }
            for (int i = 0; i < Global.IOMax; i++)
            {
                int mask = 1 << i;
                inputs[i] = ((mask & indata) > 0) ? true : false;
                outputs[i] = ((mask & outdata) > 0) ? true : false;
            }
            return true;
        }

        public void Run()
        {
            OnMsg($"io스레드 시작");
            try
            {
                while (IOUpdate())
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception) { }
            finally
            {
                OnMsg("IO스레드 정지");
                bRunthread = false;
            }
        }

        public void Start()
        {
            if (bRunthread == false)
            {
                runthread = new Thread(Run);
                runthread.IsBackground = true;
                bRunthread = true;
                runthread.Start();
            }
        }
    }
}
