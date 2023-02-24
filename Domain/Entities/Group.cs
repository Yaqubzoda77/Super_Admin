using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group
    {
        public int Id { get; set; } 
        public string GroupName { get; set; }      
        public string Key { get; set; }
        public string Value { get; set; }             
    }
}
