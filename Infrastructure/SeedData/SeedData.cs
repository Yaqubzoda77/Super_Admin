using Dapper;
using Domain.Contain;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.SeedData;

public static  class SeedData
{
    public static void Seed(DataContext context)
    {
  
        
        if(context.Roles.Any()) return;
        var roles = new List<IdentityRole>()
        {

            new IdentityRole(Roles.SuperAdmin){NormalizedName = Roles.SuperAdmin.ToUpper()},
            new IdentityRole(Roles.Student){NormalizedName = Roles.Student.ToUpper()},

        };
        context.Roles.AddRange(roles);
        context.SaveChanges();

    }
}