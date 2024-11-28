using NMCMotionSDK;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using VisionHelper;

namespace mainfrm
{
    using NMC = NMCSDKLib;
    using Status = NMCSDKLib.MC_STATUS;
    public class Machine
    {
        public static frmImage imgform;

        public Machine()
        {
            io = IO.Getinstance();
            drillMotor = new Motor(eY.DrillOn);
            convMotor = new Motor(eY.ConveyOn);
            xAxis = new Servo(Global.XAxisName, Global.XAxisNodeNum);
            yAxis = new Servo(Global.YAxisName, Global.YAxisNodeNum);
            teachTable = new TeachData(Global.TeachingFileSavePath + "Teach.txt");
            pickVac = new Vacuum(eX.VacOn1, eX.VacOn2, eY.VacOn, eY.VacOff);
            loadcyl = new DualCylinder(eX.LoadCylOn, eX.LoadCylOff, eY.LoadCylOn, eY.LoadCylOff);
            workcyl = new Cylinder(eX.WorkCylDown, eX.WorkCylUp, eY.WorkCylOn);
            movecyl = new Cylinder(eX.MoveCylOn, eX.MoveCylOff, eY.MoveCylOn);
            uldcyl = new Cylinder(eX.UldCylOn, eX.UldCylOff, eY.UldCylOn);
            pickCyl = new Cylinder(eX.PickCylDown, eX.PickCylUp, eY.PickCylOn);
            tower = new LampTower();
            drillTimer = new Stopwatch();
            convTimer = new Stopwatch();
            LoadRecipe();
            vm = new visionViewModel();
            vision = new Vision(OpenCV_picture, find_picture, vm);
        }
        public event Action<string> SendMsgHandler;
        public void OnMsg(string msg) => SendMsgHandler?.Invoke(msg);

        public event Action<string> sendColorHandler;
        public void ColorMsg(string msg) => sendColorHandler?.Invoke(msg);
        //써보자...
        public event Action<int, int, int> SendCntHandler;
        public void onCntMsg(int num1, int num2, int num3) => SendCntHandler?.Invoke(num1, num2, num3);
        public event Action<EqpStatus> SendEQPStatusHandler;
        public void EQPMsg(EqpStatus msg) => SendEQPStatusHandler?.Invoke(msg);

        //이미지 결과값 불러오기 위해서 machine에 추가
        Vision vision;
        visionViewModel vm;
        PictureBox OpenCV_picture;
        PictureBox find_picture;
        //

        IO io;
        public static EqpStatus eqpStatus = EqpStatus.Stop;
        Servo xAxis, yAxis;
        TeachData teachTable;
        IActuator pickVac, pickCyl;
        IActuator loadcyl, workcyl, movecyl, uldcyl, drillMotor, convMotor;
        //public EqpStatus status;
        Stopwatch drillTimer, convTimer;
        LampTower tower;
        Cylinder cylinder;
        Motor motor;
        Thread runThread;
        bool brunThread = false;
        EqpCmd cmd;
        AutoStep loadStep, workStep, uldStep;
        public static int cntOK = 0;
        public static int cntRV = 0;
        public static int cntRJ = 0;
        public static Recipe recipetype;
        public static bool[] OK = new bool[9];
        public static bool[] RV = new bool[9];

        bool bflagIdle = false;
        bool bWorkExist = false;
        bool bLoadExist = false;
        bool bUldExist = false;
        ProductType ptype;
        public Servo XAxis => xAxis;
        public Servo YAxis => yAxis;
        public TeachData TeachTable => teachTable;
        public IActuator PickVac => pickVac;
        public IActuator PickCyl => pickCyl;
        public IActuator LoadCyl => loadcyl;
        public IActuator WorkCyl => workcyl;
        public IActuator MoveCyl => movecyl;
        public IActuator UldCyl => uldcyl;
        public IActuator DrillMotor => drillMotor;
        public IActuator ConvMotor => convMotor;
        public Recipe RecipeType => recipetype;
        public LampTower lamptower => tower;
        public Vision CV_Vision => vision;
        public visionViewModel Vm => vm;
        public PictureBox Picture => OpenCV_picture;
        public PictureBox Picture2 => find_picture;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public Vision vision { get; set; }
        //쓰레드 생성
        public bool Create()
        {
            Thread.Sleep(500);
            brunThread = true;
            runThread = new Thread(Run);
            runThread.IsBackground = true;
            runThread.Start();
            return true;
        }
        public void LoadRecipe()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            var selectedrecipe = (from Recipe in recipes
                                  where Recipe.Name == "base"
                                  select Recipe).ToList();

            if ( selectedrecipe.Count > 0 )
            {
                recipetype = selectedrecipe[0];
            }
        }
        public void SetRecipe(Recipe recipe) => recipetype = recipe;
        void Run()
        {
            try
            {
                OnMsg($"STATUS 스레드 시작 , Recipe Load ");
                while ( brunThread )
                {
                    io.IOUpdate();
                    tower.LampUpdate(eqpStatus);
                    EQPRun();
                    Thread.Sleep(100);
                }
            }
            catch ( Exception ) { }
            finally
            {
                brunThread = false;
            }
        }
        void EQPRun()
        {
            switch ( cmd )
            {
                case EqpCmd.None:
                break;
                case EqpCmd.EStop:
                eqpStatus = EqpStatus.Error;
                cmd = EqpCmd.None;
                break;
                case EqpCmd.Initial:
                eqpStatus = EqpStatus.Ready;
                cmd = EqpCmd.None;
                break;
                case EqpCmd.ContiCycleRun:
                case EqpCmd.CycleStop:
                LoadSEQ();
                TestSEQ();
                //언로드도 짜야됨.
                break;

                EQPMsg(eqpStatus);

            }
        }
        //int nAutoloadStep = 0, nOldAutoLoadStep = 1;
        AutoStep OldAutoLoadStep = AutoStep.Step00;
        Stopwatch LoadtimeoutWatch = new Stopwatch();

        public void LoadSEQ()
        {
            if ( loadStep == AutoStep.Ready || loadStep == AutoStep.Step00 )
            {
                LoadtimeoutWatch.Restart();
            }
            else if ( loadStep != OldAutoLoadStep )
            {
                OldAutoLoadStep = loadStep;
                LoadtimeoutWatch.Restart();
            }
            else
            {
                if ( LoadtimeoutWatch.ElapsedMilliseconds > 20 * 1000 )
                {
                    OnMsg($"LoadSEQ --> {loadStep}에서 타임아웃 발생");
                    eqpStatus = EqpStatus.Error;
                    OnMsg($"설비 상태 : {eqpStatus}");
                    CmdError();
                }
            }
            switch ( loadStep )
            {
                case AutoStep.Ready:
                if ( bWorkExist == false )
                {
                    //eqpStatus = EqpStatus.Idle;
                    //OnMsg($"설비 상태 : {eqpStatus}");
                }
                break;

                case AutoStep.Step00:
                if ( cmd == EqpCmd.CycleStop && bWorkExist == false )
                {
                    loadStep = AutoStep.Ready;
                    eqpStatus = EqpStatus.Ready;
                    cmd = EqpCmd.None;
                    EQPMsg(eqpStatus);
                    OnMsg($"설비 상태 : {eqpStatus}");
                }
                else if ( io.X[(int)eX.LoadSensor] )
                {

                    LoadCyl.On();
                    OnMsg("로더실린더 동작");
                    eqpStatus = EqpStatus.Run;
                    OnMsg($"설비 상태 : {eqpStatus}");
                    loadStep++;
                }
                else if ( bWorkExist == false )
                {
                    if ( bflagIdle == false )
                    {
                        bflagIdle = true;
                        OnMsg($"설비 상태 : {eqpStatus}");
                    }
                    eqpStatus = EqpStatus.Idle;
                    EQPMsg(eqpStatus);
                }
                break;
                case AutoStep.Step10:
                if ( LoadCyl.IsOn() )
                {
                    bflagIdle = false;
                    bLoadExist = true;
                    LoadCyl.Off();
                    loadStep++;
                }
                break;
                case AutoStep.Step20:
                if ( LoadCyl.IsOff() )
                {
                    WorkCyl.On();
                    loadStep++;
                }
                break;
                case AutoStep.Step30:
                if ( WorkCyl.IsOn() )
                {
                    DrillMotor.On();
                    OnMsg("드릴 가공 동작");
                    drillTimer.Restart();
                    loadStep++;
                }
                break;
                case AutoStep.Step40:
                if ( drillTimer.ElapsedMilliseconds > 1000 )
                {
                    DrillMotor.Off();
                    WorkCyl.Off();
                    loadStep++;
                }
                break;
                case AutoStep.Step50:
                if ( bWorkExist == false )
                {
                    OnMsg("판정필요하여 moveCyl 동작");
                    MoveCyl.On();
                    loadStep++;
                }
                break;
                case AutoStep.Step60:
                if ( MoveCyl.IsOn() )
                {
                    MoveCyl.Off();
                    loadStep++;
                }
                break;
                case AutoStep.Step70:
                if ( MoveCyl.IsOff() )
                {
                    OnMsg("loadStep 완료");
                    loadStep = AutoStep.Complete;
                }
                break;
                case AutoStep.Complete:
                bLoadExist = false;
                bWorkExist = true;
                if ( cmd == EqpCmd.CycleStop )
                {
                    loadStep = AutoStep.Ready;
                    OnMsg($"설비 상태 : {eqpStatus}");
                }
                else
                    loadStep = AutoStep.Step00;
                break;
            }
        }
        AutoStep OldAutoWorkStep = AutoStep.Step00;
        Stopwatch WorktimeoutWatch = new Stopwatch();
        void TestSEQ()
        {
            if ( workStep == AutoStep.Ready || workStep == AutoStep.Step00 )
            {
                LoadtimeoutWatch.Restart();
            }
            else if ( workStep != OldAutoWorkStep )
            {
                OldAutoWorkStep = workStep;
                WorktimeoutWatch.Restart();
            }
            else
            {
                if ( WorktimeoutWatch.ElapsedMilliseconds > 15 * 1000 )
                {
                    OnMsg($"TestSEQ --> {workStep}에서 타임아웃 발생");
                    eqpStatus = EqpStatus.Error;
                    CmdError();
                }
            }
            switch ( workStep )
            {
                case AutoStep.Ready:
                break;
                case AutoStep.Step00:
                if ( bWorkExist )
                {
                    if ( vision.Search(out ProductType? objType) )
                    {
                        switch ( objType )
                        {
                            case ProductType.Blue:
                            case ProductType.Silver:
                            case ProductType.Beige:
                            ptype = (ProductType)objType;
                            workStep++;
                            OnMsg($"판정 결과 : {objType}");
                            ColorMsg($"판정 결과 : {ptype.ToString()}");
                            break;
                        }
                    }
                    else
                    {
                        CmdError();
                        OnMsg($"판정 실패! 설비 확인 부탁드립니다.");
                        eqpStatus = EqpStatus.Error;
                        OnMsg($"설비 상태 : {eqpStatus}");
                    }
                }
                break;
                case AutoStep.Step10:
                //여기서 테스트 해줘야된다.
                ConvMotor.On();
                convTimer.Restart();
                workStep++;
                break;

                case AutoStep.Step20:
                //ok 100와 rv 200 , rj 300 나누기
                if ( ptype.ToString() == recipetype.OK.ToString() )
                {
                    workStep = AutoStep.StepOK;
                }
                else if ( ptype.ToString() == recipetype.RV.ToString() )
                {
                    workStep = AutoStep.StepRV;
                }
                else
                    workStep = AutoStep.StepRJ;
                break;

                //step 300~320 RJ 단계
                case AutoStep.StepRJ:
                if ( convTimer.ElapsedMilliseconds > 1200 )
                {
                    ConvMotor.Off();
                    UldCyl.On();
                    workStep++;
                }
                break;
                case AutoStep.step310:
                if ( UldCyl.IsOn() )
                {
                    UldCyl.Off();
                    workStep++;
                    cntRJ++;
                    onCntMsg(cntOK, cntRV, cntRJ);
                }
                break;
                case AutoStep.step320:
                workStep = AutoStep.Complete;
                break;
                //RJ 완료

                //양품 step 100~
                case AutoStep.StepOK:
                if ( io.X[(int)eX.UldSensor] )
                {
                    Thread.Sleep(100);
                    convMotor.Off();
                    workStep++;
                }
                break;
                case AutoStep.Step110:
                if ( io.X[(int)eX.UldSensor] )
                {
                    int idx = 0;
                    while ( idx < 9 )
                    {
                        if ( OK[idx] == false )
                        {
                            OK[idx] = true;
                            string EnumName = Enum.GetName(typeof(TeachData.eListGroup1), idx + 1);
                            Enum.TryParse<TeachData.eList>(EnumName, out TeachData.eList eListNum);
                            WorkMove(TeachData.eList.LoadPos, eListNum);
                            workStep++;
                            cntOK++;
                            onCntMsg(cntOK, cntRV, cntRJ);

                            break;
                        }
                        else
                        {
                            idx++;
                        }
                    }
                    if ( idx == 9 )
                    {
                        OnMsg($"OK 트레이가 가득 찼습니다. 설비 확인 필요.");
                        //eqpStatus = EqpStatus.Error;
                        CmdError();
                    }

                }
                break;
                case AutoStep.Step120:
                if ( !io.X[(int)eX.UldSensor] && PickCyl.IsOff() && PickVac.IsOff() )
                {
                    workStep = AutoStep.Complete;
                }
                break;

                //RV 진행               
                case AutoStep.StepRV:
                if ( io.X[(int)eX.UldSensor] )
                {
                    Thread.Sleep(100);
                    convMotor.Off();
                    workStep++;
                }
                break;
                case AutoStep.Step210:
                if ( io.X[(int)eX.UldSensor] )
                {
                    int idx = 0;
                    while ( idx < 9 )
                    {
                        if ( RV[idx] == false )
                        {
                            RV[idx] = true;
                            string EnumName = Enum.GetName(typeof(TeachData.eListGroup2), idx + 1);
                            Enum.TryParse<TeachData.eList>(EnumName, out TeachData.eList eListNum);
                            WorkMove(TeachData.eList.LoadPos, eListNum);
                            workStep++;
                            cntRV++;
                            onCntMsg(cntOK, cntRV, cntRJ);
                            break;
                        }
                        else
                        {
                            idx++;
                        }
                    }
                    if ( idx >= 9 )
                    {
                        //에러
                        OnMsg($"RV 트레이가 가득 찼습니다. 설비 확인 필요.");
                        CmdError();
                    }

                }
                break;
                case AutoStep.Step220:
                if ( !io.X[(int)eX.UldSensor] && PickCyl.IsOff() && PickVac.IsOff() )
                {
                    workStep = AutoStep.Complete;
                }
                break;
                case AutoStep.Complete:
                bWorkExist = false;
                if ( cmd == EqpCmd.CycleStop && bLoadExist == false )
                {
                    eqpStatus = EqpStatus.Ready;
                    EQPMsg(eqpStatus);
                    cmd = EqpCmd.None;
                    workStep = AutoStep.Ready;
                }
                else
                {
                    if ( io.X[(int)eX.LoadSensor] == false && bWorkExist == false && bLoadExist == false )
                        eqpStatus = EqpStatus.Idle;
                    workStep = AutoStep.Step00;
                }
                break;
            }
        }
        public bool WorkMove(TeachData.eList from, TeachData.eList to)
        {
            int AutoStep = 0, oldAutoStep = 1, timeCnt = 0;
            Stopwatch timeoutWatch = new Stopwatch();
            OnMsg($"{from}에서 {to}로 이동");
            while ( true )
            {
                Thread.Sleep(100);
                if ( AutoStep != oldAutoStep )
                {
                    oldAutoStep = AutoStep;
                    timeCnt = 0;
                    timeoutWatch.Restart();
                }
                else
                {
                    if ( timeoutWatch.ElapsedMilliseconds > 30 * 1000 )
                    {
                        OnMsg($"{AutoStep}에서 타임아웃 발생");
                        eqpStatus = EqpStatus.Error;
                        OnMsg($"설비 상태 : {eqpStatus}");
                        return false;
                    }
                }
                switch ( AutoStep )
                {
                    case 0:
                    if ( pickCyl.IsOff() && pickVac.IsOff() )
                    {
                        MultMoveAbs(from);
                        OnMsg($"받기 위치 이동");
                        Thread.Sleep(1000);
                        AutoStep++;
                    }
                    break;
                    case 1:
                    if ( XAxis.Info[AxisStatus.Map.StandStill] == true && YAxis.Info[AxisStatus.Map.StandStill] == true )
                    {
                        pickCyl.On();
                        OnMsg($"받기 위치에서 하강");
                        AutoStep++;
                    }
                    break;
                    case 2:
                    if ( pickCyl.IsOn() == true )
                    {
                        pickVac.On();
                        OnMsg($"받기 위치에서 흡착");
                        AutoStep++;
                    }
                    break;
                    case 3:
                    if ( pickVac.IsOn() )
                    {
                        pickCyl.Off();
                        Thread.Sleep(1000);
                        AutoStep++;
                    }
                    break;
                    case 4:
                    if ( pickCyl.IsOff() || pickVac.IsOn() )
                    {
                        MultMoveAbs(to);
                        Thread.Sleep(500);
                        OnMsg($"보내기 위치 이동");
                        AutoStep++;
                    }
                    break;
                    case 5:
                    if ( XAxis.Info[AxisStatus.Map.StandStill] == true && YAxis.Info[AxisStatus.Map.StandStill] == true && pickVac.IsOn() )
                    {
                        pickCyl.On();
                        OnMsg($"보내기 위치에서 하강");
                        AutoStep++;
                    }
                    break;
                    case 6:
                    if ( pickCyl.IsOn() )
                    {
                        pickVac.Off();
                        AutoStep++;
                    }
                    break;
                    case 7:
                    if ( pickVac.IsOff() )
                    {
                        pickCyl.Off();
                        AutoStep++;
                    }
                    break;
                    case 8:
                    if ( pickCyl.IsOff() )
                    {
                        OnMsg($"피커 동작 완료");
                        return true;
                    }
                    break;
                    default:
                    return false;
                }
            }
            return true;
        }
        public bool CmdInit()
        {
            //cmd = EqpCmd.Initial;
            //eqpStatus == EqpStatus.Ready ||
            if ( eqpStatus == EqpStatus.Run || eqpStatus == EqpStatus.Idle )
            {
                return false;
            }
            LoadCyl.Off();
            WorkCyl.Off();
            MoveCyl.Off();
            PickCyl.Off();
            UldCyl.Off();
            DrillMotor.Off();
            ConvMotor.Off();
            for ( int i = 0; i < 9; i++ )
            {
                OK[i] = false;
                RV[i] = false;
            }
            //주의필요함......sdc.hsh
            eqpStatus = EqpStatus.Ready;
            workStep = loadStep = AutoStep.Ready;
            OnMsg($"설비 상태 : {eqpStatus}");
            bWorkExist = bWorkExist = false;
            cmd = EqpCmd.Initial;
            EQPMsg(eqpStatus);
            return true;
        }
        public bool CmdEStop()
        {
            DrillMotor.Off();
            ConvMotor.Off();
            eqpStatus = EqpStatus.Stop;
            cmd = EqpCmd.EStop;
            OnMsg($"설비 상태 : {eqpStatus}");
            EQPMsg(eqpStatus);
            return true;
        }

        public bool CmdError()
        {
            DrillMotor.Off();
            ConvMotor.Off();
            eqpStatus = EqpStatus.Error;
            cmd = EqpCmd.EStop;
            OnMsg($"설비 상태 : {eqpStatus}");
            EQPMsg(eqpStatus);
            return true;
        }

        public bool CmdContiCycleStart()
        {
            if ( eqpStatus != EqpStatus.Ready )
                return false;
            eqpStatus = EqpStatus.Run;
            workStep = loadStep = AutoStep.Step00;
            cmd = EqpCmd.ContiCycleRun;
            OnMsg($"설비 상태 : {eqpStatus}");
            EQPMsg(eqpStatus);
            return true;
        }
        public bool CmdCycleStop()
        {
            if ( !( eqpStatus == EqpStatus.Idle || eqpStatus == EqpStatus.Run ) )
                return false;
            //eqpStatus = EqpStatus.Run;
            cmd = EqpCmd.CycleStop;
            //EQPMsg(eqpStatus);
            return true;
        }

        public bool Home()
        {
            PickVac.Off();
            pickCyl.Off();
            Thread.Sleep(500);
            OnMsg($"Machine Home Start");
            bool xComplete = xAxis.Home();
            bool yComplete = yAxis.Home();
            if ( !xComplete || !yComplete )
            {
                OnMsg("home 실패");
                return false;
            }
            OnMsg("성공");
            return true;
        }
        public bool MultMoveAbs(TeachData.eList pos)
        {
            ushort[] axisArr = new ushort[2] { Global.XAxisNodeNum, Global.YAxisNodeNum };
            double[] posArr = new double[2] { teachTable[pos].X, teachTable[pos].Y };
            NMC.MC_DIRECTION[] dirArr = new NMC.MC_DIRECTION[2] { NMC.MC_DIRECTION.mcCurrentDirection, NMC.MC_DIRECTION.mcCurrentDirection };
            Status ms = NMC.MC_MoveAbsoluteMultiAxis(Global.BoardID, 2, axisArr, posArr,
                                                    xAxis.Param[ServoParam.eList.PosVel],
                                                    xAxis.Param[ServoParam.eList.PosAcc],
                                                    xAxis.Param[ServoParam.eList.PosDec],
                                                    xAxis.Param[ServoParam.eList.PosJerk], dirArr, 1);
            if ( ms != Status.MC_OK )
            {
                OnMsg("초기화 실패");
                return false;
            }
            return true;
        }

    }
}

