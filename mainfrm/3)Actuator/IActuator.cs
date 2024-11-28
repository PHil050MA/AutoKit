namespace mainfrm
{
    public interface IActuator
    {
        void On();
        void Off();
        bool IsOn();
        bool IsOff();
    }
}
