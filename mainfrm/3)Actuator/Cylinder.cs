namespace mainfrm
{
    public class Cylinder : IActuator
    {
        public Cylinder(eX _isOnBit, eX _isOffBit, eY _onBit)
        {
            isOnBit = _isOnBit;
            isOffBit = _isOffBit;
            onBit = _onBit;
            io = IO.Getinstance();
        }
        eX isOnBit, isOffBit;
        eY onBit;
        IO io;
        public void On() => io.On(onBit);
        public void Off() => io.Off(onBit);
        public bool IsOn() => io.X[(int)isOnBit] && !io.X[(int)isOffBit];
        public bool IsOff() => !io.X[(int)isOnBit] && io.X[(int)isOffBit];
    }
}
