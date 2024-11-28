using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class Vacuum : IActuator
    {
        public Vacuum(eX _isOnBit1, eX _isOnBit2, eY _onBit, eY _OffBit) {
            isOnBit1 = _isOnBit1;
            isOnBit2 = _isOnBit2;
            onBit = _onBit;
            offBit = _OffBit;
            io = IO.Getinstance();
        }
        eX isOnBit1, isOnBit2;
        eY onBit, offBit;
        IO io;

        public void On() {
            io.On(onBit);
            io.Off(offBit);
        }

        public void Off() {
            io.On(offBit);
            io.Off(onBit);
        }
        public bool IsOn() => io.X[(int)isOnBit1] && io.X[(int)isOnBit2];
        public bool IsOff() => !io.X[(int)isOnBit1] && !io.X[(int)isOnBit1];
    }
}
