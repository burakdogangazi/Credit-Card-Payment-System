using DataAccess.Repositories;
using KrediKartiOdeme.Model;

namespace KrediKartiOdeme.Helpers
{
    public static class CustomerHelper
    {
        public static Result ToCustomer(this string identityNumber, EfCustomerDAL dal)
        {
            if (identityNumber.Length != 10 || identityNumber.Length != 11)
                return new Result(ApplicationMessages.IDENTITY_NUMBER_IS_NOT_VALID);

            var conf = dal.Find(identityNumber);

            if (conf == null)
                return new Result(ApplicationMessages.IDENTITY_NUMBER_NOT_FOUND);

            return new Result(conf);
        }
        public static Result ValidateCustomer(this string identityNumber, EfCustomerDAL dal)
        {
            if (identityNumber.Length != 10 && identityNumber.Length != 11)
                return new Result(ApplicationMessages.IDENTITY_NUMBER_IS_NOT_VALID);

            if (!dal.Any(identityNumber))
                return new Result(ApplicationMessages.IDENTITY_NUMBER_NOT_FOUND);

            return new Result();
        }
    }
}
