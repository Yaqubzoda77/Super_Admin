using Domain.Contain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.SeedData;

public static  class SeedData
{
    public static void Seed(DataContext context)
    {
  
        
        if(context.Translations.Any()) return; 
        var roles = new List<IdentityRole>()
        {

            new IdentityRole(Roles.SuperAdmin){NormalizedName = Roles.SuperAdmin.ToUpper()},
            new IdentityRole(Roles.Student){NormalizedName = Roles.Student.ToUpper()},

        };

        var translate = new List<Translate>()
        {

            new Translate(){Id = 1, GroupName = "Menu", Key = "home", Value = "home", LanguageId =1},
            new Translate(){Id = 2, GroupName = "Menu", Key = "home", Value = "главный", LanguageId =2},
            new Translate(){Id = 3, GroupName = "Menu", Key = "home", Value = "Асоси",  LanguageId = 3},

            new Translate(){Id = 4, GroupName = "Menu", Key = "About", Value = "About", LanguageId =1},
            new Translate(){Id = 5, GroupName = "Menu", Key = "About", Value = "O ceбе", LanguageId =2},    
            new Translate(){Id = 6, GroupName = "Menu", Key = "About", Value = "дар бораи", LanguageId = 3},
            
            new Translate(){Id = 7, GroupName = "Menu", Key = "Contact", Value = "Contact", LanguageId =1},
            new Translate(){Id = 8, GroupName = "Menu", Key = "Contact", Value = "Контакт", LanguageId =2},
            new Translate(){Id = 9, GroupName = "Menu", Key = "Contact", Value = "Контакт",  LanguageId = 3},
        };

        context.Translations.AddRange(translate);
        context.SaveChanges();

        var language = new List<Language>()
        {
            new Language(){Id = 1, Name = "English"},
            new Language(){Id = 2, Name = "Russian" },
            new Language(){Id = 3, Name = "Tajik"},

        };

        context.Languages.AddRange(language);
        context.SaveChanges();

        var group = new List<Group>()
        {

            new Group(){Id = 1, GroupName = "Menu", Key =  "home" , Value = "home" },
            new Group(){Id = 2, GroupName = "About" , Key =  "About", Value = "About" },
            new Group(){Id = 3, GroupName = "Contact", Key = "Contact", Value = "Contact"},

        };

        context.Groups.AddRange(group);
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
}