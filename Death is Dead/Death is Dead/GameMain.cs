using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class GameMain
    {
        //Fields

        private Player player;
        static Rectangle[] rect = new Rectangle[5] { new Rectangle(300, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(0, 550,10000, Ressources.plateforme.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height) };

        Map map = new Map(rect);

        //Constructors

        public GameMain()
        {
            player = new Player(new Vector2(350, 0), Ressources.Player);
        }

        // Get & Set

        //Methods

        //Update & Draw
        public void Update()
        {
            player.Update(Keyboard.GetState(), rect);
            for (int i = 0; i < player.Tirs.Length; i++)
            {
                if (player.Tirs[i] != null)
                {
                    if (player.Tirs[i].position.X > 700)
                        player.Tirs[i] = null;
                    else
                        player.Tirs[i].Update();
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {

            map.Draw(sb);
            sb.Draw(Ressources.plateforme, rect[0], Color.Red);
            player.Draw(sb);
            foreach (Projectile tir in player.Tirs)
            {
                if (tir != null && tir.position.X < 700)
                    tir.Draw(sb);
            }
        }
    }
}
