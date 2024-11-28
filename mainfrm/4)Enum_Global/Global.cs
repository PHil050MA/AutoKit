using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public enum EqpStatus
    {
        Ready, Stop, Error, Run, Idle
    }
    public enum EqpCmd
    {
        None, EStop, Initial, ContiCycleRun, CycleStop
    }
    public enum WorkInfo
    {
        //물체 색깔 입력 필요
        
    }
    public enum AutoStep
    {
        Ready, Step00, Step10, Step20, Step30, Step40, Step50, Step60, Step70, Step80, Step90, Step100,
        Step110, Step120, Step130, Step140, Step150, Step160, Step170, Step180, Step190, Step200,
        Step210, Step220, Step230, Step240, Step250, Step260, Step270, Step280, Step290, step300, step310, step320,
        step330, step340, StepOK =11, StepRV=21, StepRJ=31,
        Complete=36
    }

    public class Global
    {
        public const ushort BoardID = 0;
        public const ushort eCatAddr = 1;
        public const int IOMax = 32;
        public const int IORawCnt = 4;
        public const int IOColCnt = 8;
        public const string XAxisName = "X축";
        public const string YAxisName = "Y축";
        public const int XAxisNodeNum = 2;
        public const int YAxisNodeNum = 3;
        public const string ParamFileSavePath = "Param\\";
        public const string TeachingFileSavePath = "Teach\\";
        public const double TeachOffset = 34000.0;
    }
}
