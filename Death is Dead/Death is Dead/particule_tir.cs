using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Death_is_Dead
{
    class particule_tir
    {
         
        private int size1;
        private Vector2 velocity1;
        public Vector2 position1;
        private Texture2D particle;
        public int Alpha = 90;
        public Color colour = new Color(0,60, 255,90);
       

        public particule_tir(int size, float speed, Vector2 pos)
        {
            this.size1 = size;
            this.velocity1 = new Vector2(speed, Ressources.random_number.Next(-2,2));
            this.position1 = pos;
            position1.Y = pos.Y+25;
            this.particle = Ressources.particule;
        }
        public void Update()
        {
           
            this.position1.X -= 1;
            this.position1 += this.velocity1;
            if (Alpha>10) Alpha = Alpha - 10;
            colour.A = (byte)Alpha;
        }

        public void Draw(SpriteBatch sb)
        {
            position1.Y = position1.Y - 0.06f;
            sb.Draw(particle, position1, colour);
            
        }
      
    }
}
