using DataAccess.Basics;
using Entities.DbEntities;

namespace DataAccess.Repositories
{
    public class EfCustomerDAL : EfRepository<Customer, CreditCardContext>
    {
        public object Find(string v)
        {
            throw new NotImplementedException();
        }

        public bool Any(string identityNumber)
        {
            using(var c = new CreditCardContext())
            {
                return c.Customers.Any(x => x.CustomerName.Equals(identityNumber));
            }
        }
    } 
}
