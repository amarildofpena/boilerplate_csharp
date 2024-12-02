using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase
    {
        private readonly IServiceProvider _serviceProvider;
        protected RepositoryBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected TResult ExecuteInScope<TResult, TContext>(Func<TContext, TResult> action) where TContext : DbContext
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                return action(dbContext);
            }
        }
    }
}
