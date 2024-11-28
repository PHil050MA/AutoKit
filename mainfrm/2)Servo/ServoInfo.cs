using System;
using System.Threading;

using NMC = NMCMotionSDK.NMCSDKLib;
using NMCStatus = NMCMotionSDK.NMCSDKLib.MC_STATUS;

namespace mainfrm
{
    public class ServoInfo
    {
        public ServoInfo(ushort _axis)
        {
            axis = _axis;
            start();
        }

        ushort axis;
        public event Action<string> MsgHandler;
        public void OnMsg(string msg) => MsgHandler?.Invoke(msg);

        double actPos, cmdPos, actVel, cmdVel;
        bool[] status = new bool[(int)AxisStatus.Map.Max];
        public double ActPos => actPos;
        public double CmdPos => cmdPos;
        public double ActVel => actVel;
        public double CmdVel => cmdVel;
        public bool this[AxisStatus.Map idx]=> status[(int)idx]; 

        Thread runthread;
        bool bRunthread;

        bool Update()
        {
            uint data = 0;
            NMCStatus[] ms = new NMCStatus[5];
            ms[0] = NMC.MC_ReadActualPosition(Global.BoardID, axis, ref actPos);
            ms[1] = NMC.MC_ReadCommandedPosition(Global.BoardID, axis, ref cmdPos);
            ms[2] = NMC.MC_ReadActualVelocity(Global.BoardID, axis, ref actVel);
            ms[3] = NMC.MC_ReadActualVelocity(Global.BoardID, axis, ref cmdVel);
            ms[4] = NMC.MC_ReadAxisStatus(Global.BoardID, axis, ref data);
            foreach (NMCStatus s in ms)
            {
                if (s != NMCStatus.MC_OK)
                {
                    OnMsg($"위치값 모니터링 보드io 에러 : {s}");
                    return false;
                }
            }
            for (int i = 0; i < (int)AxisStatus.Map.Max; i++)
            {
                int mask = 1 << i;
                status[i] = ((data & mask) > 0);
            }
            return true;
        }
        void Run()
        {
            OnMsg($"서보 쓰레드 시작");
            try
            {
                while (Update())
                {
                    Thread.Sleep(100);
                }
            }
            catch (Exception) { }
            finally
            {
                OnMsg($"쓰레드 종료");
                bRunthread = false;
            }
        }
        public void start()
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
