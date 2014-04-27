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
                item.rectangle.X--;
            }
        }

        public void Draw(SpriteBatch sb, int screenWidth, int screenHeight)
        {
            sb.Draw(Ressources.fond, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            foreach (Obstacle item in obs)
            {
                if (item.rectangle.X + item.texture.Width > 0 || item.rectangle.X < screenWidth || item.rectangle.Y + item.texture.Height > 0 || item.rectangle.Y < screenHeight)
                    sb.Draw(item.texture, new Vector2(item.rectangle.X, item.rectangle.Y), Color.White);
            }

            foreach (Mob item in mobs)
            {
                if (item.position.X + item.texture.Width > 0 || item.position.X < screenWidth || item.position.Y + item.texture.Height > 0 || item.position.Y < screenHeight)
                sb.Draw(item.texture, new Vector2(item.position.X, item.position.Y), Color.White);
            }
        }
    }
}
