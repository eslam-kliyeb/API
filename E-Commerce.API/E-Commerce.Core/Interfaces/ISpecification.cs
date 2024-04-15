using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface ISpecification<T>
    {
        // where Criteria
        public Expression<Func<T,bool>> Criteria { get; }
        // includes
        public List<Expression<Func<T,object>>> IncludeExpressions { get;}
        //orderby
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDesc { get;}
        //skip
        public int Skip {  get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}

// context.set<T>.include().include().where().orderby().skip().take()