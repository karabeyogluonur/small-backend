using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Infrastructre.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Persistence.Seeds
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<SMDbContext>();

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Database.Migrate();
        }

    }
}
