using Application.Contracts.BaseInterface;
using Application.CoreInformation.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ProjectDbContext ProjectDbContext { get; set; }
        public BaseRepository(ProjectDbContext projectDbContext)
        {
            ProjectDbContext = projectDbContext;
        }
        public void Create(T entity)
        {
            ProjectDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ProjectDbContext?.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            var result = ProjectDbContext.Set<T>();
            return result;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            var result = ProjectDbContext.Set<T>().Where(expression);
            return result;
        }

        public void Update(T entity)
        {
            ProjectDbContext.Set<T>().Update(entity);
        }
    }
}
