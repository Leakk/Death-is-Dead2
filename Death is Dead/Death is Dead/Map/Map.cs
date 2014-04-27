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
        public Obstacle[] obs;
        public Mob[] mobs;

        public Map(Obstacle[] obs, Mob[] mob)
        {
            this.obs = obs;
            this.mobs = mob;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Obstacle item in obs)
            {
                item.rectangle.X = item.rectangle.X - 1;
            }

            foreach (Mob item in mobs)
            {
                item.position.X = item.position.X - 1;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Ressources.fond, new Rectangle(0, 0, 800, 600), Color.White);

            foreach (Obstacle item in obs)
            {
                sb.Draw(item.texture, new Vector2(item.rectangle.X, item.rectangle.Y), Color.White);
            }

            foreach (Mob item in mobs)
            {
                sb.Draw(item.texture, new Vector2(item.position.X, item.position.Y), Color.White);
            }
        }
    }
}
