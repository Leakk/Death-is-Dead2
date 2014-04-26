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
    class Map
    {

        public scrolling liste;

        public Map(Rectangle[] liste)
        {
            this.liste = new scrolling(liste);
        }

        public void Update(GameTime gameTime)
        {
            liste.Update(gameTime);
        }
        public void Draw(SpriteBatch sb)
        {

            sb.Draw(Ressources.fond, new Rectangle(0,0, 800,600), Color.White);
            foreach (Rectangle item in liste.list)
            {
                sb.Draw(Ressources.plateforme, new Vector2(item.X, item.Y), Color.White);
            }
        }
    }
}
