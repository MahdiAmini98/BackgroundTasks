namespace Hangfire.Infrastructures.Abstraction
{
    public interface ISmsIocService
    {
        public void IrancellSms(string phoneNumber);
        public void HamrahAvalSms(string phoneNumber);
        public void RightelSms(string phoneNumber);
    }
}
