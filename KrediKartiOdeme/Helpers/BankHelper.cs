using DataAccess.Repositories;
using KrediKartiOdeme.Model;

namespace KrediKartiOdeme.Helpers
{
    public static class BankHelper
    {
        public static Result ToBankConfiguration(this int id, EfBankConfigurationDAL dal)
        {              
            var conf = dal.Get(id);

            if (conf == null)
                return new Result(ApplicationMessages.BANK_NOT_FOUND);

            return new Result(conf);
        }
    }
}
