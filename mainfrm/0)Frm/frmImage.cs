namespace mainfrm
{
    //밑에 있는 함수들 싹다 머신으로 보내버리고 라이브 이미지 보거나 하는것만 여기에다가 만들자.
    public partial class frmImage : Form
    {

        public static frmImage imgform;
        //public Vision vision;
        //public visionViewModel vm;

        Rectangle rect = new Rectangle();
        bool bDrawing = false;
        public frmImage(Machine _machine)
        {
            //딴데서 써먹어보려고...
            imgform = this;
            InitializeComponent();
            machine = _machine;
            machine.Picture = picture;
            machine.Picture2 = pictureBox2;
            machine.CV_Vision.Init();
            //vm = new visionViewModel();
            machine.CV_Vision = new Vision(picture, pictureBox2, machine.Vm);
            picture.SizeMode = PictureBoxSizeMode.AutoSize;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;

            //0번은 블루
            gridColorParam.DataSource = machine.Vm.ColorParam[0];
            gridROI.DataSource = machine.Vm.ROIParam;

            picture.MouseDown += Picture_MouseDown;
            picture.MouseMove += Picture_MouseMove;
            picture.MouseUp += Picture_MouseUp;
            picture.Paint += Picture_Paint;
            foreach ( var v in Enum.GetValues(typeof(ProductType)) )
            {
                cbProduct.Items.Add(v);
            }
            cbProduct.Text = ProductType.Blue.ToString();
            cbProduct.SelectedIndexChanged += CbProduct_SelectedIndexChanged;
        }
        Machine machine;

        private void CbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( Enum.TryParse<ProductType>(cbProduct.Text, out ProductType type) )
            {
                gridColorParam.DataSource = null;
                gridColorParam.DataSource = machine.Vm.ColorParam[(int)type];
            }
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Red), rect);
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            if ( bDrawing == false ) return;
            bDrawing = false;
            int width = e.X - rect.X;
            int height = e.Y - rect.Y;
            rect.Size = new System.Drawing.Size(width, height);
            picture.Invalidate();
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if ( bDrawing == false ) return;
            int width = e.X - rect.X;
            int height = e.Y - rect.Y;
            rect.Size = new System.Drawing.Size(width, height);
            picture.Invalidate();
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            rect.Location = new System.Drawing.Point(e.X, e.Y);
            bDrawing = true;
        }


        private void btnLive_Click(object sender, EventArgs e)
        {
            machine.CV_Vision.Live();
        }

        private void btnROISave_Click(object sender, EventArgs e)
        {
            if ( rect.Width < 1 || rect.Height < 1 ) return;
            machine.Vm.ROIParam[0].Value = rect.X;
            machine.Vm.ROIParam[1].Value = rect.Y;
            machine.Vm.ROIParam[2].Value = rect.Width;
            machine.Vm.ROIParam[3].Value = rect.Height;

            gridROI.DataSource = null;
            gridROI.DataSource = machine.Vm.ROIParam;

            machine.Vm.Save();
        }

        private void btnGetValue_Click(object sender, EventArgs e)
        {
            if ( rect.Width < 1 || rect.Height < 1 ) return;

            Enum.TryParse<ProductType>(cbProduct.Text, out ProductType type);
            machine.CV_Vision.GetMinMaxValue(rect, type);

            gridColorParam.DataSource = null;
            gridColorParam.DataSource = machine.Vm.ColorParam[(int)type];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            machine.Vm.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( machine.CV_Vision.Search(out ProductType? type) )
            {
                MessageBox.Show(type.ToString());
            }
            else
                MessageBox.Show($"찾지 못했습니다.");
        }
    }
}
