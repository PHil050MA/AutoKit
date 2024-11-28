using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class Motor : IActuator
    {
        public Motor(eY _onBit) {
            io = IO.Getinstance();
            onBit = _onBit;
        }
        IO io;
        eY onBit,offBit;

        public void On() => io.On(onBit);

        public void Off() => io.Off(onBit);
       
    }
}
