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
        private Life life;
        private int player1_life;
        static Rectangle[] rect = new Rectangle[5] { new Rectangle(300, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(0, 550,1230, Ressources.sol.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height), new Rectangle(600, 800, Ressources.plateforme.Width, Ressources.plateforme.Height) };

        Map map = new Map(rect);

        //Constructors

        public GameMain()
        {
            player = new Player(new Vector2(350, 0), Ressources.Player);
            life = new Life();
            player1_life = 100;
            
            
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
                    if (player.Tirs[i].position.X > 800 || player.Tirs[i].position.X < -Ressources.Tir.Width)
                        player.Tirs[i] = null;
                    else
                        player.Tirs[i].Update();
                }
            }

            life.Udapte(player1_life);

            #region/*smoke*/

            for (int j = 0; j < player.Smoke.Length; j++)
            {
                if (player.Smoke[j] != null)
                {
                    if (player.Smoke[j].Alpha < 0)
                    {
                        player.Smoke[j] = null;
                       
                    }
                    else
                        player.Smoke[j].Update();
                }

            }
            #endregion

            
        }

        public void Draw(SpriteBatch sb)
        {

            map.Draw(sb);
            //sb.Draw(Ressources.plateforme, rect[0], Color.Red);
            player.Draw(sb);
            life.Draw(sb);
            foreach (Projectile tir in player.Tirs)
            {
                if (tir != null)
                    tir.Draw(sb);
            }
            #region/*smoke*/
            foreach (smoke smoke in player.Smoke)
            {
                if (smoke != null && smoke.Alpha >=0)
                    smoke.Draw(sb);
            }
            #endregion
            sb.Draw(Ressources.sol, rect[1], Color.White);


            
        }
    }
}
