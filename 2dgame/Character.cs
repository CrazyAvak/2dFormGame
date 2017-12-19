using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dgame
{
    class Character
    {
        private int health;
        private int exp;
        private int level;
        private int score;
        private List<weapon> weapons = new List<weapon>();
        private int x;
        private int y;

        public int Health { get => health; set => health = value; }
        public int Exp { get => exp; set => exp = value; }
        internal List<weapon> Weapons { get => weapons; set => weapons = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Score { get => score; set => score = value; }
        public int Level { get => level; set => level = value; }

        public Character()
        {
            Level = 1;
            exp = 0;
            score = 0;
            health = 3;            
            y = 100; // sets the start position
        }


        public void addExp(int exp)
        {
            this.exp = this.exp + exp;
            if(this.exp > 1000)
            {
                Level++;
            }
        }
        public void getDamage(int damage)
        {
            health = health - damage;
            if(health <= 0)
            {
                //dood
            }
        }
    } 
}
