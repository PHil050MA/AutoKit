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
    public partial class frmTeach : Form
    {
        public frmTeach(Machine _machine)
        {
            InitializeComponent();
            machine = _machine;
            CreateListView();
        }
        Machine machine;
        void CreateListView()
        {
            lvTeachTable.Items.Clear();
            for ( TeachData.eList key = 0; key < TeachData.eList.Max; key++ )
            {
                string pos = key.ToString();
                string xValue = machine.TeachTable[key].X.ToString("F2");
                string yValue = machine.TeachTable[key].Y.ToString("F2");

                ListViewItem lvi = new ListViewItem(pos);
                lvi.SubItems.Add(xValue);
                lvi.SubItems.Add(yValue);
                lvTeachTable.Items.Add(lvi);

            }
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if ( b != null )
            {
                if ( b.Text == "▲" ) machine.YAxis.MoveVelNeg();
                else if ( b.Text == "▼" ) machine.YAxis.MoveVelPos();
                else if ( b.Text == "◀" ) machine.XAxis.MoveVelNeg();
                else machine.XAxis.MoveVelPos();
            }
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if ( b != null )
            {
                if ( b.Text == "▲" || b.Text == "▼" ) machine.YAxis.Stop();
                else
                {
                    machine.XAxis.Stop();
                }
            }
        }

        private void btnInchMove_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if ( b != null )
            {
                double dist;
                if ( b.Parent == gbX )
                {
                    double.TryParse(tbXMove.Text, out dist);
                    machine.XAxis.MoveRelative(dist);
                }
                else if ( b.Parent == gbY )
                {
                    double.TryParse(tbYMove.Text, out dist);
                    machine.YAxis.MoveRelative(dist);
                }
            }
        }
        private void btnLoadTeach_Click(object sender, EventArgs e)
        {
            double curPosX = machine.XAxis.Info.ActPos;
            double curPosY = machine.YAxis.Info.ActPos;
            machine.TeachTable[TeachData.eList.LoadPos].SetValue(curPosX, curPosY);
            lvTeachTable.Items[(int)TeachData.eList.LoadPos].SubItems[1].Text = curPosX.ToString("F");
            lvTeachTable.Items[(int)TeachData.eList.LoadPos].SubItems[2].Text = curPosY.ToString("F");
            machine.TeachTable.Save();
        }
        private void btnTeach_Click(object sender, EventArgs e)
        {
            double curPosX = machine.XAxis.Info.ActPos;
            double curPosY = machine.YAxis.Info.ActPos;
            for ( TeachData.eList key = TeachData.eList.pos1_1; key < TeachData.eList.Max; key++ )
            {
                int xIdx = ((int)key - 1) % 7;
                int yIdx = ((int)key - 1) / 7;
                double posX = curPosX + (Global.TeachOffset * xIdx);
                double posY = curPosY + (Global.TeachOffset * yIdx);
                machine.TeachTable[key].SetValue(posX, posY);
                lvTeachTable.Items[(int)key].SubItems[1].Text = posX.ToString("F");
                lvTeachTable.Items[(int)key].SubItems[2].Text = posY.ToString("F");
            }
            machine.TeachTable.Save();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if ( lvTeachTable.SelectedItems.Count == 1 )
            {
                TeachData.eList pos = (TeachData.eList)lvTeachTable.SelectedItems[0].Index;
                machine.MultMoveAbs(pos);
            }
            else
                MessageBox.Show("하나만 선택해주세요", "오류");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            machine.PickCyl.Off();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            machine.PickCyl.On();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            machine.PickVac.On();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            machine.PickVac.Off();
        }


    }
}
