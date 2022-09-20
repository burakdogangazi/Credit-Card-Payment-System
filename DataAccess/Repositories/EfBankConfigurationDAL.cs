using DataAccess.Basics;
using Entities.DbEntities;

namespace DataAccess.Repositories

{
    public class EfBankConfigurationDAL:EfRepository<BankConfiguration, CreditCardContext>
    {
    }
}
