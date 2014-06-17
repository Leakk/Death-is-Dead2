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
    class Mob : Entity
    {
        [NonSerialized]
        int latenceTir = 0;
        Vector2 mob_pos;
        [NonSerialized]
        int pos_X_tmp = 0;
        [NonSerialized]
        int pos_Y_tmp = 0;
        [NonSerialized]
        public Bonus.Coeur coeur = new Bonus.Coeur();
        [NonSerialized]
        public bool activer_bonus;
        [NonSerialized]
        public Bonus.Faux Bonusfaux = new Bonus.Faux();
        public int type;
        uint[] tab;




        public Mob(Vector2 position, Texture2D texture, int life, int type)
            : base(position, texture, life, true)
        {
            this.type = type;
            activer_bonus = true;
            mob_pos = position;
            this.life = life;
            Life = new Life();
            tab = new uint[texture.Height * texture.Width];
            texture.GetData<uint>(tab);
            if (type == 1 || type == 2)
            {
                texture = Ressources.E2;
                if (type == 1)
                {
                    coeur.exist = true;
                    coeur.x = (int)position.X;
                    coeur.y = (int)position.Y;
                }
                else
                {
                    Bonusfaux.exist = true;
                    Bonusfaux.x = (int)position.X;
                    Bonusfaux.y = (int)position.Y;
                }
                activer_bonus = false;
                dead = true;
                this.life = 0;
            }
        }

        public void Update(Obstacle[] rect, Players player, Players p2)
        {

            base.Update(rect);
            //position.X += 1;
            Life.Udapte(life);



            if (life <= 0)
            {
                if (activer_bonus)                                  /* pour evité que sa mettre coeur.exist = true à chaque frame*/
                /* vu qu'apres quand le joueur le prends sa se met à false ( c'est gérer dans la classe coeur )*/
                {
                    if ((Ressources.random_number.Next(0, 20) == 1)
                        || (Ressources.random_number.Next(0, 20) == 2)
                            || (Ressources.random_number.Next(0, 20) == 3))
                    {
                        coeur.exist = true;
                    }
                    else
                        if ((Ressources.random_number.Next(0, 20) == 10)
                            || (Ressources.random_number.Next(0, 20) == 11))
                        {
                            Bonusfaux.exist = true;
                        }
                }
                activer_bonus = false;

                velocity.X = 0;
                velocity.Y = 0;
                if (coeur.exist)
                {
                    coeur.Udapte_coeur((int)position.X, (int)position.Y, ref player, ref p2);
                }
                if (Bonusfaux.exist)
                {
                    Bonusfaux.Udapte((int)position.X, (int)position.Y, ref player, ref p2);
                }

                HitboxB = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxD = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxG = new Collision(new Rectangle(0, 0, 0, 0));
                HitboxH = new Collision(new Rectangle(0, 0, 0, 0));
                dead = true;
            }
            else
            {
                switch (type)
                {
                    #region/*type1*/
                    case 0:
                        {
                            float nearby_p1 = IA.isPlayerNearby(player, this);
                            float nearby_p2 = IA.isPlayerNearby(p2, this);
                            if (nearby_p1 < nearby_p2)
                            {
                                if (nearby_p1 < 800)
                                {
                                    pos_X_tmp = (int)position.X;          /* ça c'est parce que il me semble que maxime faisait teleporté les enemies hors de la map lors de leur mort */
                                    pos_Y_tmp = (int)position.Y;         /* donc je retiens leur derniere pos quand ils étaient encore dans l'image, donc quand ils étaient vivants */

                                    //if (HitboxD.is_coll(rect) || HitboxG.is_coll(rect))
                                    //    velocity.Y = -6f;

                                    if (nearby_p1 > 80 && IA.isGroundNearby(rect, this, hasFliped))
                                    {
                                        Tuple<int, bool> mob_bef = IA.isMobBefore(player, this);
                                        velocity.X = 2 * mob_bef.Item1;
                                        hasFliped = mob_bef.Item2;
                                    }
                                    else if (nearby_p1 < 50 && (Math.Abs(HitboxB.rect_coll(rect).Center.X - position.X) < Ressources.sol.Width / 2 - 10 || IA.isGroundNearby(rect, this, hasFliped)))
                                    {
                                        Tuple<int, bool> mob_bef = IA.isMobBefore(player, this);
                                        velocity.X = -2 * mob_bef.Item1;
                                        hasFliped = mob_bef.Item2;
                                    }
                                    else
                                        velocity.X = 0;

                                    if (IA.isPlateformNearby(rect, this) && velocity.Y == 0)
                                        velocity.Y = -6f;

                                    if (position.Y >= player.position.Y && position.Y <= player.position.Y + player.texture.Height && latenceTir == 0 && !dead)
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
                            else
                            {
                                if (nearby_p2 < 800)
                                {
                                    pos_X_tmp = (int)position.X;          /* ça c'est parce que il me semble que maxime faisait teleporté les enemies hors de la map lors de leur mort */
                                    pos_Y_tmp = (int)position.Y;         /* donc je retiens leur derniere pos quand ils étaient encore dans l'image, donc quand ils étaient vivants */

                                    if (HitboxD.is_coll(rect) || HitboxG.is_coll(rect))
                                        velocity.Y = -6f;

                                    if (nearby_p2 > 80)
                                    {
                                        Tuple<int, bool> mob_bef = IA.isMobBefore(p2, this);
                                        velocity.X = 2 * mob_bef.Item1;
                                        hasFliped = mob_bef.Item2;
                                    }
                                    else if (nearby_p2 < 50)
                                    {
                                        Tuple<int, bool> mob_bef = IA.isMobBefore(p2, this);
                                        velocity.X = -2 * mob_bef.Item1;
                                        hasFliped = mob_bef.Item2;
                                    }
                                    else
                                        velocity.X = 0;

                                    if (IA.isPlateformNearby(rect, this))
                                        velocity.Y = -6f;

                                    if (position.Y >= p2.position.Y && position.Y <= p2.position.Y + p2.texture.Height && latenceTir == 0 && !dead)
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
                        }
                        break;
                    #endregion
                    case 3:
                        /*ecrit la pour le mob*/
                        break;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            switch (type)
            {
                case 3:
                    /*le draw*/
                    break;

                default:

                    if (coeur.exist)
                        coeur.Draw(sb);
                    if (Bonusfaux.exist)
                    {
                        Bonusfaux.Draw(sb);
                    }

                    Life.Draw(sb, (int)position.X, (int)position.Y - 20, 0.5f, 5);

                    if (!dead)
                    {
                        if (hasFliped)
                            sb.Draw(Ressources.EFlip2, new Vector2(position.X, position.Y), Color.White);
                        else
                            sb.Draw(Ressources.E2, new Vector2(position.X, position.Y), Color.White);
                    }
                    break;
            }

            //sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }


        public void maj(ContentManager co)
        {
            Texture2D x = co.Load<Texture2D>("sprite/E2");
            texture = new Texture2D(x.GraphicsDevice, 50, tab.Length / 50);
            texture.SetData<uint>(tab);
        }
    }
}
