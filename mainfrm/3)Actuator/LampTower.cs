using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainfrm
{
    public class LampTower
    {
        public LampTower()
        {
            io = IO.Getinstance();
        }
        eX isOnBit;
        eY onBit, offBit;
        IO io;

        //LampRedOn,
        //LampYelOn,
        //LampGreenOn,
        public void LampUpdate(EqpStatus status)
        {
            if ( status == EqpStatus.Ready )
            {
                io.On(eY.LampGreenOn);
                io.On(eY.LampYelOn);
                io.Off(eY.LampRedOn);
            }
            else if ( status == EqpStatus.Error )
            {
                io.Off(eY.LampGreenOn);
                io.Off(eY.LampYelOn);
                io.On(eY.LampRedOn);
            }
            else if ( status == EqpStatus.Run )
            {
                io.On(eY.LampGreenOn);
                io.Off(eY.LampYelOn);
                io.Off(eY.LampRedOn);
            }
            else if ( status == EqpStatus.Stop )
            {
                io.On(eY.LampRedOn);
                io.Off(eY.LampYelOn);
                io.Off(eY.LampGreenOn);
            }
            else if ( status == EqpStatus.Idle )
            {
                io.On(eY.LampYelOn);
                io.Off(eY.LampGreenOn);
                io.Off(eY.LampRedOn);
            }
        }
        public bool IsOn(eY ey) => io.Y[(int)ey];
    }
}
