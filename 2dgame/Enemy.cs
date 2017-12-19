using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dgame
{
    class Enemy
    {
        private int x;
        private int y;
        private int id;
        private int health;

        
        public Enemy(int x, int y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
            Health = 3;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Id { get => id; set => id = value; }
        public int Health { get => health; set => health = value; }
    }
}
