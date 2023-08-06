using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private DongHoShopDbContext dbContext;

        public DongHoShopDbContext Init()
        {
            return dbContext ?? (dbContext = new DongHoShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
