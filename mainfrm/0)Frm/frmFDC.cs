using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FdcHelper;
namespace mainfrm
{
    public partial class frmFDC : Form
    {
        public frmFDC(Machine _machine, FdcTask _fdc, List<FdcConfig> _config, List<FdcSVID> _svid)
        {
            InitializeComponent();
            machine = _machine;
            fdc = _fdc;
            config = _config;
            svid = _svid;
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
            gridConfig.DataSource = null;
            gridSVID.DataSource = null;
            gridConfig.DataSource = config;
            gridSVID.DataSource = svid;

            fdc = new FdcTask(config[0], svid);

            fdc.Start();
            tmFDC.Start();
        }

        //public frmTeach(Machine _machine) {
        //    InitializeComponent();
        //    machine = _machine;
        //    CreateListView();
        //}
        Machine machine;
        FdcTask fdc;
        List<FdcConfig> config;
        List<FdcSVID> svid;

        private void tmFDC_Tick(object sender, EventArgs e)
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
            gridSVID.DataSource = null;
            gridSVID.DataSource = svid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileHelper.FileHelper.Save_Json<List<FdcSVID>>(svid, "FDC_svid.txt");
            FileHelper.FileHelper.Save_Json<List<FdcConfig>>(config, "FDC_Config.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            svid = FileHelper.FileHelper.Load_Json<List<FdcSVID>>("FDC_svid.txt");
            config = FileHelper.FileHelper.Load_Json<List<FdcConfig>>("FDC_Config.txt");
        }
    }
}
