using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dgame
{
    class weapon
    {
        private string name;
        private string type;
        private int damage;

        public weapon(string name, string type, int damage)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;

        }
    }
}
