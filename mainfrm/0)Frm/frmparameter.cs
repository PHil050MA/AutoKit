namespace mainfrm
{
    public partial class frmparameter : Form
    {
        public frmparameter(ServoParam xAxis, ServoParam yAxis)
        {
            InitializeComponent();
            xAxisParam = xAxis;
            yAxisParam = yAxis;
            CreateListView();
        }
        ServoParam xAxisParam, yAxisParam;

        private void lvX_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            if ( lv != null )
            {
                string axis = lv.Parent.Text; //tostring 확인하고 수정해야함.
                string name = lv.SelectedItems[0].SubItems[0].Text;
                string value = lv.SelectedItems[0].SubItems[1].Text;

                frmPopup f = new frmPopup(axis, name, value);
                if ( f.ShowDialog() == DialogResult.OK )
                {
                    double data;
                    if ( double.TryParse(f.Value, out data) )
                    {
                        lv.SelectedItems[0].SubItems[1].Text = data.ToString("F2");
                        ServoParam.eList key = (ServoParam.eList)lv.SelectedItems[0].Index;
                        xAxisParam[key] = data;
                        FileHelper.FileHelper.fileFullPath = Global.ParamFileSavePath + "X축" + "_Param.txt";
                        xAxisParam.Save();
                    }
                    else
                    {
                        MessageBox.Show("유효값이 아닙니다.", "주의");
                    }
                }
            }
        }

        private void lvY_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            if ( lv != null )
            {
                string axis = lv.Parent.Text; //tostring 확인하고 수정해야함.
                string name = lv.SelectedItems[0].SubItems[0].Text;
                string value = lv.SelectedItems[0].SubItems[1].Text;

                frmPopup f = new frmPopup(axis, name, value);
                if ( f.ShowDialog() == DialogResult.OK )
                {
                    double data;
                    if ( double.TryParse(f.Value, out data) )
                    {
                        lv.SelectedItems[0].SubItems[1].Text = data.ToString("F2");
                        ServoParam.eList key = (ServoParam.eList)lv.SelectedItems[0].Index;
                        yAxisParam[key] = data;
                        FileHelper.FileHelper.fileFullPath = Global.ParamFileSavePath + "Y축" + "_Param.txt";
                        yAxisParam.Save();
                    }
                    else
                    {
                        MessageBox.Show("유효값이 아닙니다.", "주의");
                    }
                }
            }
        }

        void CreateListView()
        {
            for ( ServoParam.eList key = 0; key < ServoParam.eList.Max; key++ )
            {
                string paramName = key.ToString();
                string paramValue_X = xAxisParam[key].ToString("F2");
                string paramValue_Y = yAxisParam[key].ToString("F2");
                ListViewItem lvi = new ListViewItem(paramName);
                lvi.SubItems.Add(paramValue_X);
                lvX.Items.Add(lvi);
                lvi = new ListViewItem(paramName);
                lvi.SubItems.Add(paramValue_Y);
                lvY.Items.Add(lvi);
            }


        }
    }

}
