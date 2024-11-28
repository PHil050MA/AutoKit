using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mainfrm
{
    public partial class frmIO : Form
    {
        public frmIO()
        {
            InitializeComponent();
            CreateUI();
            io = IO.Getinstance();
            tmUIUpdate.Start();
        }
        IO io;
        CheckBox[,] cbXs = new CheckBox[Global.IORawCnt, Global.IOColCnt];
        CheckBox[,] cbYs = new CheckBox[Global.IORawCnt, Global.IOColCnt];

        void CreateUI()
        {
            int width = 60, height = 30, offset = 10, idx = 0;
            for ( int i = 0; i < Global.IORawCnt; i++ )
            {
                for ( int j = 0; j < Global.IOColCnt; j++ )
                {
                    int xPos = offset + (width + offset) * j;
                    int yPos = offset * 3 + (height + offset) * i;

                    cbXs[i, j] = new CheckBox();
                    cbXs[i, j].Size = new Size(width, height);
                    cbXs[i, j].Location = new Point(xPos, yPos);
                    cbXs[i, j].Text = "X" + idx.ToString("00");
                    cbXs[i, j].Appearance = Appearance.Button;
                    cbXs[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    gbX.Controls.Add(cbXs[i, j]);

                    cbYs[i, j] = new CheckBox();
                    cbYs[i, j].Size = new Size(width, height);
                    cbYs[i, j].Location = new Point(xPos, yPos);
                    cbYs[i, j].Appearance = Appearance.Button;
                    cbYs[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    cbYs[i, j].Text = "Y" + idx.ToString("00");
                    gbY.Controls.Add(cbYs[i, j]);
                    cbYs[i, j].Click += FrmIO_Click;
                    idx++;
                }
            }
            int gwidth = cbXs[Global.IORawCnt - 1, Global.IOColCnt - 1].Right + offset;
            int gheight = cbXs[Global.IORawCnt - 1, Global.IOColCnt - 1].Bottom + offset;
            gbX.Size = new Size(gwidth, gheight);
            gbY.Size = new Size(gwidth, gheight);
            gbY.Location = new Point(gbX.Left, gbX.Bottom + offset);
        }

        private void FrmIO_Click(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if ( sender != null )
            {
                int bit = int.Parse(c.Text.Substring(1));
                if ( io.Y[bit] ) io.Off((eY)bit);
                else
                {
                    io.On((eY)bit);
                }
            }
        }

        void UIUpdate()
        {
            int idx = 0;
            for ( int i = 0; i < Global.IORawCnt; i++ )
            {
                for ( int j = 0; j < Global.IOColCnt; j++ )
                {
                    cbXs[i, j].Checked = io.X[idx];
                    cbYs[i, j].Checked = io.Y[idx];
                    idx++;
                }
            }
        }

        private void tmUIUpdate_Tick(object sender, EventArgs e)
        {
            UIUpdate();
        }
    }
}
