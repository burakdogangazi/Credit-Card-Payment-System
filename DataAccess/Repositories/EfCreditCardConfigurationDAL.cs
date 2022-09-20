using DataAccess.Basics;
using Entities.DbEntities;

namespace DataAccess.Repositories

{
    public class EfCreditCardConfigurationDAL : EfRepository<CreditCardConfiguration, CreditCardContext>
    {
        public CreditCardConfiguration Find(string binNumber)
        {
            using (var c = new CreditCardContext())
            {
                return c.CreditCardConfigurations.FirstOrDefault(x => x.BinNumber.Equals(binNumber));
            }
        }
    }
}
