using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGun.Model
{
    public abstract class AbstractEnemy : DrawableGameComponent
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public double Health { get; set; }
        public double Shield { get; set; }

        public AbstractEnemy(Game game)
            :
            base(game)
        {
        }
    }
}
