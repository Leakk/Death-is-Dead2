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
    class Mob : Entity
    {
        int latenceTir = 0;
        Vector2 mob_pos = new Vector2(0, 0);
        int pos_X_tmp = 0;
        int pos_Y_tmp = 0;
        Bonus.Coeur coeur = new Bonus.Coeur();
        bool activer_bonus_coeur;



        public Mob(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {

            activer_bonus_coeur = true;
            mob_pos = position;
            this.life = life;
            Life = new Life();
        }

        public void Update(Obstacle[] rect, Player player)
        {

            base.Update(rect);
            //position.X += 1;
            Life.Udapte(life);



            if (life <= 0)
            {
                if (activer_bonus_coeur) coeur.exist = true;         /* pour evité que sa mettre coeur.exist = true à chaque frame*/
                activer_bonus_coeur = false;                          /* vu qu'apres quand le joueur le prends sa se met à false ( c'est gérer dans la classe coeur )*/


                coeur.Udapte_coeur((int)position.X, (int)position.Y, ref player);


                HitboxB = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxD = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxG = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxH = new Collision(new Rectangle(0, 0, 0, 0));
                dead = true;
            }
            else
                if (position.X - player.position.X < 800 && position.X - player.position.X > -800)
                {
                    pos_X_tmp = (int)position.X;          /* ça c'est parce que il me semble que maxime faisait teleporté les enemies hors de la map lors de leur mort */
                    pos_Y_tmp = (int)position.Y;         /* donc je retiens leur derniere pos quand ils étaient encore dans l'image, donc quand ils étaient vivants */

                    if (HitboxD.is_coll(rect) || HitboxG.is_coll(rect))
                        velocity.Y = -6f;

                    if (position.X < player.position.X)
                    {
                        velocity.X = 2;
                        hasFliped = false;
                    }
                    if (position.X > player.position.X)
                    {
                        velocity.X = -2;
                        hasFliped = true;
                    }

                    if (position.Y == player.position.Y && latenceTir == 0 && !dead)
                    {
                        latenceTir = 40;
                        for (int i = 0; i < Tirs.Length; i++)
                        {
                            if (Tirs[i] == null)
                            {
                                Tirs[i] = new Projectile(texture.Bounds.X, 10, this, hasFliped);
                                Ressources.tir_son.Play();
                                break;
                            }
                        }
                    }
                    if (latenceTir > 0)
                        latenceTir--;

                }
        }

        public void Draw(SpriteBatch sb)
        {

            // coeur.Draw(sb,(int)mob_pos.X,(int)mob_pos.Y);
            if (coeur.exist)
                coeur.Draw(sb, (int)position.X, (int)position.Y);

            Life.Draw(sb, (int)position.X, (int)position.Y - 20, 0.5f, 5);

            if (!dead)
            {
                if (hasFliped)
                    sb.Draw(Ressources.EFlip2, new Vector2(position.X, position.Y), Color.White);
                else
                    sb.Draw(Ressources.E2, new Vector2(position.X, position.Y), Color.White);
            }

            //sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }
    }
}
