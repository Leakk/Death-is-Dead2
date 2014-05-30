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
    [Serializable]
    class Players : Entity
    {
        [NonSerialized]
        public float position_Y_texture;

        [NonSerialized]
        int k = 5;



        public Players(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {
            Tirs = new Projectile[10];
            position_Y_texture = position.Y;
            this.life = life;
            Life = new Life();
        }


        public void Update2( Obstacle[] rect)
        {
            #region /*Flottement*/
            if (k < 80)
            {
                if (k < 40)
                {
                    position_Y_texture = position.Y - k * 0.25f;
                    k++;
                }
                else
                {
                    position_Y_texture = position.Y + (k * 0.25f) - 20;
                    k++;
                }
            }
            else
                k = 0;
            #endregion      
                      

            if (position.X < 0)
                position.X = 0;
            if (position.X > 800 - texture.Width)
                position.X = 800 - texture.Width;
            base.Update(rect);
            Life.Udapte(life);

            if (position.X <= 4 && HitboxD.is_coll(rect) || life <= 0 || position.Y > 600)
                dead = true;
            else
                dead = false;


        }


    }
}
