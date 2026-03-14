using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class Permission

    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Permission() { }

        public Permission(int id, string name) 
        {
            Name = name;

        }

    }
}
