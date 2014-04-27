using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Death_is_Dead
{
    class Mob : Entity
    {
        private Projectile[] tirs;
        float position_Y_texture;

        private Life Life;
        public int life;

        public Mob(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {
            tirs = new Projectile[10];
            position_Y_texture = Convert.ToInt32(position.Y);
            this.life = life;
            Life = new Life();
        }

        public void Update(Obstacle[] rect)
        {
            base.Update(rect);
        }
    }
}
