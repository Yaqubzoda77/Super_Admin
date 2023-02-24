using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Domain.DomainDto
{
    public class TranslateDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }  
        public string Key { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }   
    }
}
