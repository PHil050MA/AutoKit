using NMCMotionSDK;

using System;
using System.Threading;
namespace mainfrm
{
    using NMC = NMCSDKLib;
    using Status = NMCSDKLib.MC_STATUS;
    public class Servo
    {
        public Servo(string _name, ushort _axis)
        {
            name = _name;
            axis = _axis;
            info = new ServoInfo(axis);
            param = new ServoParam(Global.ParamFileSavePath + name + "_Param.txt");
        }
        string name;
        ushort axis;
        ServoInfo info;
        public event Action<string> MsgHandler;
        public void OnMsg(string msg) => MsgHandler?.Invoke(msg);

        ServoParam param;
        public ServoInfo Info =>info;
        public ServoParam Param => param;

        public bool On()
        {
            Status ms = NMC.MC_Power(Global.BoardID, axis, true);
            if (ms != Status.MC_OK)
            {
                OnMsg($"Servo On Error : {ms}");
                return false;
            }
            OnMsg($"Servo On");
            return true;
        }
        public bool Off()
        {
            Status ms = NMC.MC_Power(Global.BoardID, axis, false);
            if (ms != Status.MC_OK)
            {
                OnMsg($"Servo Off Error : {ms}");
                return false;
            }
            OnMsg($"Servo Off");
            return true;
        }
        public bool Reset()
        {
            Status ms = NMC.MC_Reset(Global.BoardID, axis);
            if (ms != Status.MC_OK)
            {
                OnMsg($"Reset Error : {ms}");
                return false;
            }
            OnMsg($"Reset");
            return true;
        }
        public bool EStop()
        {
            Status ms = NMC.MC_Stop(Global.BoardID, axis, true, 5000, 50000);
            if (ms != Status.MC_OK)
            {
                OnMsg($"Stop Error : {ms}");
                return false;
            }
            OnMsg($"Stop");
            return true;
        }

        public bool MoveVelNeg()
        {
            Status ms = NMC.MC_MoveVelocity(Global.BoardID, axis,
                                            param[ServoParam.eList.JogVel],
                                            param[ServoParam.eList.JogAcc],
                                            param[ServoParam.eList.JogDec],
                                            param[ServoParam.eList.JogJerk],
                                            NMC.MC_DIRECTION.mcNegativeDirection,
                                            NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            return true;
        }
        public bool MoveVelPos()
        {
            Status ms = NMC.MC_MoveVelocity(Global.BoardID
                                            , axis
                                            , param[ServoParam.eList.JogVel]
                                            , param[ServoParam.eList.JogAcc]
                                            , param[ServoParam.eList.JogDec]
                                            , param[ServoParam.eList.JogJerk]
                                            , NMC.MC_DIRECTION.mcPositiveDirection
                                            , NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            return true;
        }
        public bool MoveRelative(double dist_mm)
        {
            double dist_um = dist_mm * 1000;
            Status ms = NMC.MC_MoveRelative(Global.BoardID, axis, dist_um,
                                          param[ServoParam.eList.JogVel],
                                          param[ServoParam.eList.JogAcc],
                                          param[ServoParam.eList.JogDec],
                                          param[ServoParam.eList.JogJerk],
                                          NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            return true;
        }
        public bool MoveAbsolute(double pos_mm)
        {
            double pos_um = pos_mm * 1000;
            Status ms = NMC.MC_MoveAbsolute(Global.BoardID, axis, pos_um,
                                          param[ServoParam.eList.JogVel],
                                          param[ServoParam.eList.JogAcc],
                                          param[ServoParam.eList.JogDec],
                                          param[ServoParam.eList.JogJerk],
                                          NMC.MC_DIRECTION.mcCurrentDirection,
                                          NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            return true;
        }
        public bool Stop()
        {
            Status ms = NMC.MC_Halt(Global.BoardID, axis,
                                           param[ServoParam.eList.JogDec],
                                           param[ServoParam.eList.JogJerk],
                                           NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            return true;
        }
        public bool Home()
        {
            Status ms = NMC.MC_Home(Global.BoardID, axis, 0, NMC.MC_BUFFER_MODE.mcAborting);
            if (ms != Status.MC_OK)
            {
                return false;
            }
            OnMsg($"{name} 초기화 시작");
            Thread.Sleep(500);
            int timer = 0;
            while (info[AxisStatus.Map.IsHomed] == false)
            {
                Thread.Sleep(100);
                if (++timer > 200)
                {
                    OnMsg($"{name} 타임아웃 발생");
                    return false;
                }
            }
            OnMsg($"초기화 완료");
            return true;
        }
    }
}
