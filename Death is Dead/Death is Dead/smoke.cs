using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class Smoke
    {
        private int size1;
        private Vector2 velocity1;
        public Vector2 position1;
        private Texture2D smoke;
        private Entity player;
        public int Alpha = 150;
        public Color colour = new Color(200, 200, 200, 100);
       

        public Smoke(int size, float speed, Entity player)
        {
            this.size1 = size;
            this.velocity1 = new Vector2(speed, 0);
            this.player = player;
            this.position1 = player.position;
            position1.Y = player.position.Y+25;
            this.smoke = Ressources.smoke;
        }
        public void Update()
        {
            this.position1.X -= 1;
            this.position1 += this.velocity1;
            Alpha = Alpha - 2;
            colour.A = (byte)Alpha;
        }

        public void Draw(SpriteBatch sb)
        {
            position1.Y = position1.Y - 0.06f;
            sb.Draw(smoke, position1, colour);
        }
    }
}
