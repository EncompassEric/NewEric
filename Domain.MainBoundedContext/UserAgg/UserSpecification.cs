using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Seedwork.Specification;

namespace Domain.MainBoundedContext.UserAgg
{
    public class UserSpecification
    {
        public static Specification<User> BySearchOption()
        {
            Specification<User> spec = new DirectSpecification<User>(t => true);

            return spec;
        }
    }
}
