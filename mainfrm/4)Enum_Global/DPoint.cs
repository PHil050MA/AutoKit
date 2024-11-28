using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class DPoint
    {
        private double x;
        private double y;
        public DPoint(double _x, double _y) {
            this.x = _x;
            this.y = _y;
        }
        public double X {
            get => this.x;
            set => this.x = value;
        }
        public double Y {
            get => this.y;
            set => this.y = value;
        }
        public void SetValue(double _x, double _y) {
            this.x = _x;
            this.y = _y;
        }
        public override string ToString() => this.x.ToString() + "," + this.y.ToString();
    }
}