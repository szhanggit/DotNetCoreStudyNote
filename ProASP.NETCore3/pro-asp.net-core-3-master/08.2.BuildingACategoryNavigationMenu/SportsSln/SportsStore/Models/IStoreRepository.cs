using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        /*
Without the IQueryable<T> interface, I would have to retrieve all of the
Product objects from the database and then discard the ones that I don’t want, which becomes an expensive operation as the
amount of data used by an application increases. It is for this reason that the IQueryable<T> interface is typically used instead
of IEnumerable<T> in database repository interfaces and classes.         
         */
    }
}
