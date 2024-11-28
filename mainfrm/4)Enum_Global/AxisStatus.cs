using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class AxisStatus
    {
        public static string[] names = new string[4] { "ActPos", "CmdPos", "ActVel", "CmdVel" };

        public enum Map
        {
            ErrorStop,
            Disabled,
            Stopping,
            StandStill,
            DiscreteMotion,
            ContinuousMotion,
            SynchronizedMotion,
            Homing,
            SWLimitSwitchPosEvent,
            SWLimitSwitchNegEvent,
            ConstantVelocity,
            Accelerating,
            Decelerating,
            DirectionPositive,
            DirectionNegative,
            LimitSwitchNeg,
            LimitSwitchPos,
            HomeAbsSwitch,
            HWLimitSwitchPosEvent,
            HWLimitSwitchNegEvent,
            DriveFault,
            SensorStop,
            ReadyForPowerOn,
            PowerOn,
            IsHomed,
            AxisWarning,
            MotionComplete,
            Gearing,
            GroupMotion,
            BufferFull,
            Reserved1,
            Reserved2,
            Max
        }
    }
   
}
