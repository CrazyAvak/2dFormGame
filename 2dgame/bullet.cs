using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2dgame
{    
    class bullet
    {
        private int x;
        private int y;
        private int id;
                
        public bullet( int x, int y, int id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Id { get => id; set => id = value; }
    }
}
