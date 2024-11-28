namespace mainfrm
{
    public partial class frmMain : Form
    {
        public static frmMain form;
        public frmMain()
        {
            form = this;

            InitializeComponent();
            CreateListView();
            CreateComboBoxItem();
        }
        static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        Machine machine;
        //fdc 설정
        FdcTask fdc;
        List<FdcConfig> config = new List<FdcConfig>();
        List<FdcSVID> svid = new List<FdcSVID>();
        bool bAutoManual = false;

        private void frmMain_Load(object sender, EventArgs e)
        {
            IO io = IO.Getinstance();
            if ( io.BoardInit() == false || io.BoardRun() == false )
            {
                MessageBox.Show("보드 상태 확인해주세요. 종료");
                Application.Exit();
            }
            io.Start();
            machine = new Machine();
            io.SendMsgHandler += Machine_SendMsgHandler;
            machine.XAxis.MsgHandler += Machine_SendMsgHandler;
            machine.YAxis.MsgHandler += Machine_SendMsgHandler;
            machine.SendMsgHandler += Machine_SendMsgHandler;
            machine.SendCntHandler += Machine_SendCntHandler;
            machine.sendColorHandler += Machine_sendColorHandler;
            machine.SendEQPStatusHandler += Machine_SendEQPStatusHandler;
            if ( machine.Create() == false )
            {
                MessageBox.Show("상태 확인 실패. 종료");
                Application.Exit();
            }
            tmUpdate.Start();
            FDCCreate();
            machine.LoadRecipe();
            CreateRecipe();
            ControlLockFree(gbAuto, false);
            ControlLockFree(gbManual, true);
            RecipeList();
        }
        public EqpStatus eqpstatus = EqpStatus.Stop;
        private void Machine_SendEQPStatusHandler(EqpStatus msg)
        {
            eqpstatus = msg;
        }

        private void Machine_sendColorHandler(string msg)
        {
            if ( tbColor.InvokeRequired == true )
            {
                tbColor.Invoke(new Action(() =>
                {
                    string str = $"{DateTime.Now.ToString("HH:mm:ss")} : {msg}\r\n";
                    tbColor.AppendText(str);
                    log.Info(msg);
                }));
            }
            else
            {
                string str = $"{DateTime.Now.ToString("HH:mm:ss")} : {msg}\r\n";
                tbColor.AppendText(str);
                log.Info(msg);
            }
        }
        private void Machine_SendMsgHandler(string msg)
        {
            if ( tbMsg.InvokeRequired == true )
            {
                tbMsg.Invoke(new Action(() =>
                {
                    string str = $"{DateTime.Now.ToString("HH:mm:ss")} : {msg}\r\n";
                    tbMsg.AppendText(str);
                    log.Info(msg);
                }));
            }
            else
            {
                string str = $"{DateTime.Now.ToString("HH:mm:ss")} : {msg}\r\n";
                tbMsg.AppendText(str);
                log.Info(msg);
            }
        }
        private void Machine_SendCntHandler(int ok, int rv, int rj)
        {
            if ( lbTotalCnt.InvokeRequired == true )
            {
                lbTotalCnt.Invoke(new Action(() => { lbTotalCnt.Text = $"OK : {ok}  RV : {rv} RJ : {rj} Total : {ok + rv + rj}"; }));
            }
            else
            {
                lbTotalCnt.Text = $"누적 진행 횟수 : OK : {ok}  RV : {rv} RJ : {rj} Total : {ok + rv + rj}";
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            foreach ( Form f in Application.OpenForms )
            {
                if ( f is frmIO )
                {
                    MessageBox.Show($"이미 {f.Name}이 열려있습니다.");
                    return;
                }
            }
            frmFDC fio = new frmFDC(machine, fdc, config, svid);
            fio.Show();
        }

        private async void btnLive_Click(object sender, EventArgs e)
        {
            foreach ( Form f in Application.OpenForms )
            {
                if ( f is frmIO )
                {
                    MessageBox.Show($"이미 {f.Name}이 열려있습니다.");
                    return;
                }
            }
            frmImage fio = new frmImage(machine);
            fio.Show();
        }

        void FDCCreate()
        {
            config.Clear();
            svid.Clear();
            config.Add(new FdcConfig()
            {
                EqpID = "ACADEMY01",
                ModuleID = "ACADEMY01_PC02",
                ToolID = "ACADEMY01_PC02_NS01",
                TRID = "2"
            });
            svid.Add(new FdcSVID()
            {
                SV_Name = "X_Axis_Overload",
                SV_ID = "1000",
                SV_Value = machine.XAxis.Info.ActVel.ToString("F3")
            });
            svid.Add(new FdcSVID()
            {
                SV_Name = "Y_Axis_Overload",
                SV_ID = "1001",
                SV_Value = machine.YAxis.Info.ActVel.ToString("F3")
            });

            fdc = new FdcTask(config[0], svid);
            fdc.OnlineEventHandler += Fdc_OnlineEventHandler;
            fdc.Start();
        }
        private void Fdc_OnlineEventHandler(bool online)
        {
            cbOnline.Checked = online;
            cbOnline.Text = ( online ) ? "Online" : "Offline";
        }
        public void FDC_dataUpdate()
        {
            svid.Clear();
            svid.Add(new FdcSVID()
            {
                SV_Name = "X_Axis_Overload",
                SV_ID = "1000",
                SV_Value = machine.XAxis.Info.ActVel.ToString("F3")
            });
            svid.Add(new FdcSVID()
            {
                SV_Name = "Y_Axis_Overload",
                SV_ID = "1001",
                SV_Value = machine.YAxis.Info.ActVel.ToString("F3")
            });
        }


        public void LampBtnColorUpdate()
        {
            cbRed.BackColor = ( machine.lamptower.IsOn(eY.LampRedOn) ) ? Color.Red : Color.LightGray;
            cbYellow.BackColor = ( machine.lamptower.IsOn(eY.LampYelOn) ) ? Color.Yellow : Color.LightGray;
            cbGreen.BackColor = ( machine.lamptower.IsOn(eY.LampGreenOn) ) ? Color.Green : Color.LightGray;
        }
        void CreateListView()
        {
            foreach ( string s in AxisStatus.names )
            {
                ListViewItem lvi = new ListViewItem(s);
                lvi.SubItems.Add("");
                lvX.Items.Add(lvi);
                lvi = new ListViewItem(s);
                lvi.SubItems.Add("");
                lvY.Items.Add(lvi);
            }
        }
        void CreateRecipe()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            var selectedrecipe = (from Recipe in recipes
                                  where Recipe.Name == tbRecipeName.Text      // "base"
                                  select Recipe).ToList();

            if ( selectedrecipe.Count > 0 )
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = selectedrecipe;
            }



        }
        void CreateComboBoxItem()
        {
            for ( TeachData.eList i = 0; i < TeachData.eList.Max; i++ )
            {
                cbFrom.Items.Add(i.ToString());
                cbTo.Items.Add(i.ToString());
            }
        }
        void UpdateListView(ListView lv, ServoInfo info)
        {
            lv.Items[0].SubItems[1].Text = info.ActPos.ToString("F");
            lv.Items[1].SubItems[1].Text = info.CmdPos.ToString("F");
            lv.Items[2].SubItems[1].Text = info.ActVel.ToString("F");
            lv.Items[3].SubItems[1].Text = info.CmdVel.ToString("F");
        }
        void UpdateLimitSen()
        {
            cbXNegSen.Checked = machine.XAxis.Info[AxisStatus.Map.LimitSwitchNeg];
            cbXPosSen.Checked = machine.XAxis.Info[AxisStatus.Map.LimitSwitchPos];
            cbXHomeSen.Checked = machine.XAxis.Info[AxisStatus.Map.HomeAbsSwitch];

            cbYNegSen.Checked = machine.YAxis.Info[AxisStatus.Map.LimitSwitchNeg];
            cbYPosSen.Checked = machine.YAxis.Info[AxisStatus.Map.LimitSwitchPos];
            cbYHomeSen.Checked = machine.YAxis.Info[AxisStatus.Map.HomeAbsSwitch];
        }

        //Ready 모드, Recipe 선택, Online 상태
        void UpdateAutoView()
        {
            if ( cbOnline.Text == "Online" && machine.lamptower.IsOn(eY.LampGreenOn) && machine.lamptower.IsOn(eY.LampYelOn) && !bAutoManual )
            {
                rbAuto.Checked = true;
                bAutoManual = true;
            }

        }


        //1) IO 모니터 클릭
        private void button1_Click(object sender, EventArgs e)
        {
            foreach ( Form f in Application.OpenForms )
            {
                if ( f is frmIO )
                {
                    MessageBox.Show($"이미 {f.Name}이 열려있습니다.");
                    return;
                }
            }
            frmIO fio = new frmIO();
            fio.Show();
        }
        private void btnXOn_Click(object sender, EventArgs e)
        {
            machine.XAxis.On();
        }
        private void btnXOff_Click(object sender, EventArgs e)
        {
            machine.XAxis.Off();
        }
        private void btnXStop_Click(object sender, EventArgs e)
        {
            machine.XAxis.EStop();
        }
        private void btnXReset_Click(object sender, EventArgs e)
        {
            machine.XAxis.Reset();
        }
        private void btnYon_Click(object sender, EventArgs e)
        {
            machine.YAxis.On();
        }
        private void btnYOff_Click(object sender, EventArgs e)
        {
            machine.YAxis.Off();
        }
        private void btnYStop_Click(object sender, EventArgs e)
        {
            machine.YAxis.EStop();
        }
        private void btnYReset_Click(object sender, EventArgs e)
        {
            machine.YAxis.Reset();
        }
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if ( b != null )
            {
                if ( b.Text == "▲" ) { machine.YAxis.MoveVelNeg(); }
                else if ( b.Text == "▼" ) { machine.YAxis.MoveVelPos(); }
                else if ( b.Text == "◀" ) { machine.XAxis.MoveVelNeg(); }
                else if ( b.Text == "▶" ) { machine.XAxis.MoveVelPos(); }
            }
        }
        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if ( b != null )
            {
                if ( b.Text == "▲" || b.Text == "▼" ) { machine.YAxis.Stop(); }
                else { machine.XAxis.Stop(); }
            }
        }
        private async void button6_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.Enabled = false;
            //오래걸리는 작업은 task, await 걸어둔 이유는 오래걸릴걸 알아서 다른 작업하고 온다고 해놓고, 작업 다되서 신호주면 돌아와서 다음코드 진행한다. await 안해놓으면 초기화 중에 lock이 풀려버림
            //this.invoke 작업할 필요가 없음.
            await Task.Run(() => { machine.Home(); });
            b.Enabled = true;
        }
        private async void btnMove_Click(object sender, EventArgs e)
        {
            TeachData.eList from = (TeachData.eList)cbFrom.SelectedIndex;
            TeachData.eList to = (TeachData.eList)cbTo.SelectedIndex;

            Button b = sender as Button;
            b.Enabled = false;
            await Task.Run(() => { machine.WorkMove(from, to); });
            b.Enabled = true;
        }
        private void tmUpdate_Tick(object sender, EventArgs e)
        {
            UpdateListView(lvX, machine.XAxis.Info);
            UpdateListView(lvY, machine.YAxis.Info);
            UpdateLimitSen();
            LampBtnColorUpdate();
            UpdateAutoView();
            FDC_dataUpdate();
        }
        private void btnTeach_Click(object sender, EventArgs e)
        {
            foreach ( Form f in Application.OpenForms )
            {
                if ( f is frmIO )
                {
                    MessageBox.Show($"이미 {f.Name}이 열려있습니다.");
                    return;
                }
            }
            frmTeach fio = new frmTeach(machine);
            fio.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach ( Form f in Application.OpenForms )
            {
                if ( f is frmIO )
                {
                    MessageBox.Show($"이미 {f.Name}이 열려있습니다.");
                    return;
                }
            }
            frmparameter fio = new frmparameter(machine.XAxis.Param, machine.YAxis.Param);
            fio.Show();

        }
        private void btnLoadOn_Click(object sender, EventArgs e)
        {
            machine.LoadCyl.On();
        }
        private void btnLoadOff_Click(object sender, EventArgs e)
        {
            machine.LoadCyl.Off();
        }
        private void btnWorkCylDown_Click(object sender, EventArgs e)
        {
            machine.WorkCyl.On();
        }
        private void btnWorkCylUp_Click(object sender, EventArgs e)
        {
            machine.WorkCyl.Off();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            machine.MoveCyl.On();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            machine.MoveCyl.Off();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            machine.UldCyl.On();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            machine.UldCyl.Off();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            machine.DrillMotor.On();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            machine.DrillMotor.Off();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            machine.ConvMotor.On();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            machine.ConvMotor.Off();
        }

        //필요없을듯 바꾸자 -> 레시피 대표 설정하는걸로
        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            //List<Recipe> recipes = new List<Recipe>();
            //recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            //var selectedrecipe = (from Recipe in recipes
            //                      where Recipe.Name == tbRecipeName.Text
            //                      select Recipe).ToList();

            //if (selectedrecipe.Count > 0) {

            //    dataGridView1.DataSource = null;
            //    dataGridView1.DataSource = selectedrecipe;
            //}
            ////한번더 보고 
            //machine.LoadRecipe();
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            var selectedrecipe = (from Recipe in recipes
                                  where Recipe.Name == tbRecipeName.Text
                                  select Recipe).ToList();

            if ( selectedrecipe.Count > 0 )
            {
                tbRecipeName.Text = selectedrecipe[0].Name;
                cbOK.Text = selectedrecipe[0].OK;
                cbRV.Text = selectedrecipe[0].RV;
                cbRJ.Text = selectedrecipe[0].RJ;
                //machine.RecipeType = selectedrecipe[0];
                DialogResult dr = MessageBox.Show($"현재 레시피 {machine.RecipeType.Name}  변경 레시피 {selectedrecipe[0].Name} 진행하시겠습니까?",
                    "주의", MessageBoxButtons.YesNo);
                if ( dr == DialogResult.Yes )
                {
                    machine.SetRecipe(selectedrecipe[0]);
                }
            }
        }
        public void RecipeList()
        {
            cbRecipes.Items.Clear();
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");

            foreach ( var recipe in recipes )
            {
                cbRecipes.Items.Add(recipe.Name);
            }
        }
        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            if ( recipes.FindIndex(item => item.Name.Equals(tbRecipeName.Text)) >= 0 )
            {
                DialogResult dr = MessageBox.Show("이미 레시피가 존재합니다. 수정하시겠습니까?", "주의", MessageBoxButtons.YesNo);
                if ( dr == DialogResult.Yes )
                {
                    recipes.RemoveAt(recipes.FindIndex(item => item.Name.Equals(tbRecipeName.Text)));
                    recipes.Add(new Recipe()
                    {
                        Name = tbRecipeName.Text,
                        OK = cbOK.Text,
                        RJ = cbRJ.Text,
                        RV = cbRV.Text,
                        Time = DateTime.Now
                    });
                }
            }
            else
            {
                recipes.Add(new Recipe()
                {
                    Name = tbRecipeName.Text,
                    OK = cbOK.Text,
                    RJ = cbRJ.Text,
                    RV = cbRV.Text,
                    Time = DateTime.Now
                });
            }
            recipes.RemoveAt(recipes.FindIndex(item => item.Name.Equals("base")));
            recipes.Add(new Recipe()
            {
                Name = "base",
                OK = cbOK.Text,
                RJ = cbRJ.Text,
                RV = cbRV.Text,
                Time = DateTime.Now
            });
            FileHelper.FileHelper.Save_Serial<List<Recipe>>(recipes, "RecipePattern");
            RecipeList();
            CreateRecipe();
            MessageBox.Show($"{tbRecipeName.Text} 저장 했습니다.");
        }

        private void btnDelRecipe_Click(object sender, EventArgs e)
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");

            recipes.RemoveAt(recipes.FindIndex(item => item.Name.Equals(tbRecipeName.Text)));
            FileHelper.FileHelper.Save_Serial<List<Recipe>>(recipes, "RecipePattern");
            RecipeList();
            MessageBox.Show($"{tbRecipeName.Text} 삭제 했습니다.");
        }
        private void cbRecipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = FileHelper.FileHelper.Load_Serial<List<Recipe>>("RecipePattern");
            var selectedrecipe = (from Recipe in recipes
                                  where Recipe.Name == cbRecipes.Text
                                  select Recipe).ToList();

            if ( selectedrecipe.Count > 0 )
            {
                tbRecipeName.Text = selectedrecipe[0].Name;
                cbOK.Text = selectedrecipe[0].OK;
                cbRV.Text = selectedrecipe[0].RV;
                cbRJ.Text = selectedrecipe[0].RJ;
                //machine.RecipeType = selectedrecipe[0];
            }
            //한번더 보고 
            //machine.LoadRecipe();
            CreateRecipe();
        }

        private void btnContiCycle_Click(object sender, EventArgs e)
        {
            machine.CmdContiCycleStart();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            machine.CmdInit();
            bAutoManual = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            machine.CmdCycleStop();
        }

        private void btnEStop_Click(object sender, EventArgs e)
        {
            machine.CmdEStop();
        }

        //(machine.lamptower.IsOn(eY.LampGreenOn) && !machine.lamptower.IsOn(eY.LampYelOn) && !machine.lamptower.IsOn(eY.LampRedOn))
        //            || (!machine.lamptower.IsOn(eY.LampGreenOn) && machine.lamptower.IsOn(eY.LampYelOn) && !machine.lamptower.IsOn(eY.LampRedOn))
        //bool bflag = false;
        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if ( rbAuto.Checked == false )
            {
                if ( eqpstatus == EqpStatus.Run || eqpstatus == EqpStatus.Idle )
                {
                    MessageBox.Show("설비 진행 중! 정지 후 진행 부탁드립니다.", "실패");
                    rbAuto.Checked = true;
                    // bflag = true;
                    return;
                }

                if ( tbMsg.InvokeRequired == true )
                {
                    tbMsg.Invoke(new Action(() =>
                    {
                        string str = $"{DateTime.Now.ToString("HH:mm:ss")} : Manual모드로 변경합니다.\r\n";
                        tbMsg.AppendText(str);
                        log.Info("Manual모드로 변경합니다.");
                    }));
                }
                else
                {
                    string str = $"{DateTime.Now.ToString("HH:mm:ss")} : Manual모드로 변경합니다.\r\n";
                    tbMsg.AppendText(str);
                    log.Info("Manual모드로 변경합니다.");
                }
                ControlLockFree(gbAuto, false);
                ControlLockFree(gbManual, true);
            }
            else
            {
                //fdc 온라인 + 설비 Ready상태(초록색 + 노랑색)
                //cbOnline.Text != "Online" || !(machine.lamptower.IsOn(eY.LampGreenOn) && machine.lamptower.IsOn(eY.LampYelOn))
                if ( eqpstatus == EqpStatus.Error || eqpstatus == EqpStatus.Stop )
                {
                    MessageBox.Show("설비 연결 상태 확인 필요!", "실패");
                    rbManual.Checked = true;
                    return;
                }
                else
                {
                    //bflag = false;
                    if ( tbMsg.InvokeRequired == true )
                    {
                        tbMsg.Invoke(new Action(() =>
                        {
                            string str = $"{DateTime.Now.ToString("HH:mm:ss")} : Auto모드로 변경합니다.\r\n";
                            tbMsg.AppendText(str);
                        }));
                    }
                    else
                    {
                        string str = $"{DateTime.Now.ToString("HH:mm:ss")} : Auto모드로 변경합니다.\r\n";
                        tbMsg.AppendText(str);
                    }
                    ControlLockFree(gbAuto, true);
                    ControlLockFree(gbManual, false);
                }
            }
        }
        void ControlLockFree(Control g, bool lockMode)
        {
            foreach ( Control item in g.Controls )
            {
                item.Enabled = lockMode;
            }
        }

    }
}
