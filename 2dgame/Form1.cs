using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dgame
{
    public partial class Form1 : Form
    {

        private Timer time;
        private Timer bullet;
        private Timer enemySpawn;
        
        private int x = 0;
        private int y = 0;
        gameController controller;
        private string movement = "";
        
        public Form1()
        {
            InitializeComponent();
            controller = new gameController();
            startGame();
            time = new Timer();
            time.Interval = 16;
            time.Tick += Time_Tick;
            time.Start();

            bullet = new Timer();
            bullet.Interval = 16;
            bullet.Tick += Bullet_Tick;
            bullet.Start();

            enemySpawn = new Timer();
            enemySpawn.Interval = 5000;
            enemySpawn.Tick += Enemy_Tick;
            enemySpawn.Start();
        }

        private void Enemy_Tick(object sender, EventArgs e)
        {
            //adds a new zombie
            PictureBox zombie = new PictureBox();
            zombie.Size = new Size(68, 59);
            zombie.SizeMode = PictureBoxSizeMode.StretchImage;
            zombie.Image = Image.FromFile("../zombie.png");
            Random r = new Random();
            int randY = r.Next(0, 520);            
            zombie.Location = new Point(770, randY);
            controller.createEnemy(770, randY);
            Enemy enemy = controller.ListEnemy[controller.EnemyID];
            zombie.Name = enemy.Id.ToString();
            this.Controls.Add(zombie);
        }

        private void Bullet_Tick(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {             
                if(item is Label)
                {
                    foreach (bullet bullet in controller.ListBullets)
                    {
                        if (item.Name == bullet.Id.ToString())
                        {
                            if(bullet.X > 860)
                            {
                                this.Controls.Remove(item);
                                controller.ListBullets.Remove(bullet);
                                controller.BulletID--;
                                break;
                            }
                            else
                            {
                                bullet.X = bullet.X + 5;
                                item.Location = new Point(bullet.X, bullet.Y);
                                item.Refresh();
                            }                            
                        }
                    }
                }               
            }
        }

        private void startGame()
        {
            labelHealth.Text = "Health: " + controller.getPlayer().Health;
            labelScore.Text = "Score: " + controller.getPlayer().Score;         
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            charMove();
            enemyMove();
            collision();
        }        

        private void enemyMove()
        {
            //make every enemy walk to the left
            foreach (Control item in this.Controls)
            {
                if(item is PictureBox)
                {
                    foreach (Enemy enemy in controller.ListEnemy)
                    {
                        if(item.Name == enemy.Id.ToString())
                        {
                            enemy.X = enemy.X - 1;
                            item.Location = new Point(enemy.X, enemy.Y);
                            if(enemy.X < 0)
                            {
                                /*
                                //lose health
                                if(controller.removeHealth() == true)
                                {
                                    //player is game over
                                    this.Close();
                                }
                                else
                                {
                                    labelHealth.Text = "Health: " + controller.getPlayer().Health;
                                }
                                controller.deleteEnemy(enemy.Id);
                                deleteEnemy(enemy.Id);
                                */
                            }
                            item.Refresh();
                        }
                    }
                }
            }
        }

        private void collision()
        {
            //collision detection            
            var collisonResult = controller.checkCollision();
            if(collisonResult.Item1 == -10 && collisonResult.Item2 == -10)
            {             
            }else if(collisonResult.Item1 == -10 && collisonResult.Item2 > 0)
            {
                //enemy not destroyed but bullet has a hit ans has to be removed
                deleteBullet(collisonResult.Item2);

            }else if(collisonResult.Item1 >= 0 && collisonResult.Item2 >= 0)
            {
                deleteEnemy(collisonResult.Item1);
                deleteBullet(collisonResult.Item2);                
            }

        }

        private void deleteBullet(int id)
        {
            foreach(Control item in this.Controls)
            {
                if(item is Label)
                {
                    if(item.Name == id.ToString())
                    {
                        Console.WriteLine("bullet wordt verwijderd");
                        this.Controls.Remove(item);
                        this.Refresh();
                    }
                }
            }
        }
        private void deleteEnemy(int id)
        {
            foreach (Control item in this.Controls)
            {
                if(item is PictureBox)
                {
                    if(item.Name == id.ToString())
                    {
                        Console.WriteLine("enemy wordt verwijderd");
                        this.Controls.Remove(item);
                        labelScore.Text = "Score: " + controller.getPlayer().Score;
                        this.Refresh();
                    }
                }
            }
        }

        private void charMove()
        {
            x = controller.getPlayer().X;
            y = controller.getPlayer().Y;
            switch (movement)
            {
                case "W":
                    controller.changePlayerPosition(y = y - 10, false);
                    movement = "";
                    break;
                case "S":
                    controller.changePlayerPosition(y = y + 10, false);
                    movement = "";
                    break;
                case "A":
                    controller.changePlayerPosition(x = x - 10, true);
                    movement = "";
                    break;
                case "D":
                    controller.changePlayerPosition(x = x + 10, true);
                    movement = "";
                    break;
                default:
                    break;
            }
            //moves the player to a new location
            pictureBoxChar.Location = new Point(controller.getPlayer().X, controller.getPlayer().Y);
        }

        private void key(object sender, KeyEventArgs e)
        {            
            if(e.KeyCode == Keys.W)
            {
                movement = "W";
            }else if(e.KeyCode == Keys.S)
            {
                movement = "S";
            }
            else if (e.KeyCode == Keys.A)
            {
                movement = "A";
            }
            else if (e.KeyCode == Keys.D)
            {
                movement = "D";
            }else if(e.KeyCode == Keys.Space)
            {
                this.Controls.Add(createPistolBullet());
            }
        }
        private Label createPistolBullet()
        {
            Label bulletL = new Label();
            bulletL.Location = new Point(x,y);            
            controller.createBullet(x,y);
            bullet _bullet = controller.ListBullets[controller.BulletID];

            bulletL.Name = _bullet.Id.ToString();
            bulletL.Text = "o";
            return bulletL;
        }
    }
}
