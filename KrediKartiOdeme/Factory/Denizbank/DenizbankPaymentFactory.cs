using DataAccess.Repositories;

namespace KrediKartiOdeme.Factory.Denizbank
{
    public class DenizbankPaymentFactory : IPaymentFactory
    {
        public IPayment GetPayment()
        {
            var repo = new EfBankConfigurationDAL();
            var bankConf = repo.Get(x => x.BankName.Equals("Denizbank")).First();
            return new VakifbankPayment(bankConf.TerminalNumber, bankConf.CustomerName, bankConf.CustomerPassword);
        }

        public IValidity GetValidity()
        {
            var repo = new EfBankConfigurationDAL();
            var bankConf = repo.Get(x => x.BankName.Equals("Denizbank")).First();
            return new VakifbankValidity(bankConf.TerminalNumber, bankConf.CustomerName, bankConf.CustomerPassword);
        }
    }
}
