using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dgame
{
    
    class gameController
    {
        Character player;
        List<bullet> listBullets = new List<bullet>();
        List<Enemy> listEnemy = new List<Enemy>();
        private int bulletID = -1;
        private int enemyID = -1;

        internal List<bullet> ListBullets { get => listBullets; set => listBullets = value; }
        internal List<Enemy> ListEnemy { get => listEnemy; set => listEnemy = value; }
        public int BulletID { get => bulletID; set => bulletID = value; }
        public int EnemyID { get => enemyID; set => enemyID = value; }

        public gameController()
        {
            createCharater();
        }

        private void createCharater()
        {
            player = new Character();
            weapon pistol = new weapon("Pistol", "gun", 1);
            player.Weapons.Add(pistol);            
        }

        public void changePlayerPosition(int position, bool isX)
        {
            if (isX)
            {
                player.X = position;
            }
            else
            {
                player.Y = position;
            }
        }
        public bool removeHealth()
        {
            player.Health--;
            if(player.Health <= 0)
            {
                return true;
            }
            return false;
        }

        public Tuple<int ,int> checkCollision()//int 1 is the enemy id if not destroyed gives -10 the int 2 is the bullet 
        {
            //number >= 1 && number <= 100
            foreach (bullet bullet in listBullets)
            {
                foreach (Enemy enemy in listEnemy)
                {
                    if(bullet.X >= enemy.X && bullet.X <= enemy.X + 68 && bullet.Y >= enemy.Y && bullet.Y <= enemy.Y + 59)
                    {
                        enemy.Health--;
                        if(enemy.Health <= 0)
                        {
                            //enemy has to be deleted
                            ListEnemy.Remove(enemy);
                            Console.WriteLine("enemy deleted");
                            enemyID--;
                            player.Score = player.Score + 10;
                            return Tuple.Create(enemy.Id, bullet.Id);
                        }
                        ListBullets.Remove(bullet);
                        bulletID--;
                        Console.WriteLine("hit");
                        return Tuple.Create(-10, bullet.Id);                        
                    }                    
                }
            }
            //if nothing else can be deleted returns a -10
            return Tuple.Create(-10, -10);
        }

        public void deleteEnemy(int id)
        {
            foreach (Enemy item in listEnemy)
            {
               if(item.Id == id)
                {
                    ListEnemy.Remove(item);
                    enemyID--;
                }
            }
        }
        public Character getPlayer()
        {
            return player;
        }
        public void createBullet(int x, int y)
        {
            BulletID++;
            bullet bullet = new bullet(x, y, BulletID);
            ListBullets.Add(bullet);
        }
        public void createEnemy(int x, int y)
        {
            EnemyID++;
            Enemy enemy = new Enemy(x, y, EnemyID);
            ListEnemy.Add(enemy);
        }

    }
}
