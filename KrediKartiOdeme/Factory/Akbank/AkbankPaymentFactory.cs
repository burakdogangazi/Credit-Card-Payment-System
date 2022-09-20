using DataAccess.Repositories;

namespace KrediKartiOdeme.Factory.Akbank
{
    public class AkbankPaymentFactory : IPaymentFactory
    {
        public IPayment GetPayment()
        {
            var repo = new EfBankConfigurationDAL();
            var bankConf = repo.Get(x => x.BankName.Equals("Akbank")).First();
            return new VakifbankPayment(bankConf.TerminalNumber, bankConf.CustomerName, bankConf.CustomerPassword);
        }

        public IValidity GetValidity()
        {
            var repo = new EfBankConfigurationDAL();
            var bankConf = repo.Get(x => x.BankName.Equals("Akbank")).First();
            return new VakifbankValidity(bankConf.TerminalNumber, bankConf.CustomerName, bankConf.CustomerPassword);
        }
    }
}
