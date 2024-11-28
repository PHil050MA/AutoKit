using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class DualCylinder :IActuator
    {
        public DualCylinder(eX _isOnbit, eX _isOffbit, eY _onBit, eY _offBit) {
            isOnBit = _isOnbit;
            isOffBit = _isOffbit;
            onBit = _onBit;
            offBit = _offBit;
            io = IO.Getinstance();
        }
        eX isOnBit, isOffBit;
        eY onBit, offBit;
        IO io;

        public void On() {
            io.On(onBit);
            io.Off(offBit);
        }
        public void Off() {
            io.Off(onBit);
            io.On(offBit);
        }
        public bool IsOn() => io.X[(int)isOnBit] && !io.X[(int)isOffBit] ;
        public bool IsOff() => !io.X[(int)isOnBit] && io.X[(int)isOffBit];
    }
}
