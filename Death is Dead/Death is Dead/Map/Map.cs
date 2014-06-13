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
        Texture2D fond;
        private Vector2 moving_background = new Vector2(0f, 0f);
        public end_flag flag = new end_flag(10000, 400);
        

        public Map(Obstacle[] obs, Mob[] mob, Texture2D fo)
        {
            this.obs = obs;
            this.mobs = mob;
            fond = fo;
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
            sb.Draw(Ressources.fond, moving_background, Color.White);
            flag.Draw(sb);
          //  sb.Draw(Ressources.fond, new Rectangle((int)moving_background, 0, 800, 600), Color.White);
            if (moving_background.X <= -1600f)
            {
                moving_background.X = 0f;
            }
            moving_background.X-=0.25f;
            foreach (Obstacle item in obs)
            {
                if (item.rectangle.X + item.texture.Width > 0 || item.rectangle.X < screenWidth || item.rectangle.Y + item.texture.Height > 0 || item.rectangle.Y < screenHeight)
                    sb.Draw(item.texture, new Vector2(item.rectangle.X, item.rectangle.Y), Color.White);
            }

            foreach (Mob item in mobs)
            {
                if (!item.dead && (item.position.X + item.texture.Width > 0 || item.position.X < screenWidth || item.position.Y + item.texture.Height > 0 || item.position.Y < screenHeight))
                    item.Draw(sb);
            }
        }
    }
}
