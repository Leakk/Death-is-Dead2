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


        static obstacle[] rect = new obstacle[21] { new obstacle(new Rectangle(0, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(300, 360, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(1040, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(1800, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(2100, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(2540, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(2840, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(3450, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme/*carre*/), new obstacle(new Rectangle(3850, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(4450, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(4850, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(5350, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(6600, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(6850, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(7250, 300, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(7650, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(7700, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(8150, 150, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(8550, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new obstacle(new Rectangle(9550, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new obstacle(new Rectangle(9200, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol) };

        Map map = new Map(rect);

        //Constructors

        public GameMain()
        {
            player = new Player(new Vector2(350, 0), Ressources.Player,100);
           
            
            
        }

        // Get & Set

        //Methods

        //Update & Draw
        public void Update(GameTime gameTime)
        {
            player.update1(rect);
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
            map.Update(gameTime);

        
            
            

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


            
        }
    }
}
