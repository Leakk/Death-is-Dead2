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
{[Serializable]
    class Player : Players
    {
        [NonSerialized]
        public Smoke[] Smoke;
        [NonSerialized]
        Random rnd = new Random();
        [NonSerialized]
        float badrandom;
        [NonSerialized]
        int latenceSmoke = 0;
        [NonSerialized]
        int latenceTir = 0;



        public Player(Vector2 position, Texture2D texture, int life,Boolean faux)
            : base(position, texture, life,faux)
        {

            Smoke = new Smoke[20];
            Life = new Life();
        }


        public void Update(KeyboardState keyboard, Obstacle[] rect)
        {

            #region /*Saut*/
            if (keyboard.IsKeyDown(Keys.Space) && hasJumped)
            {
                hasJumped = false;
                this.velocity.Y = -12f;
            }
            #endregion

            #region /*Deplacement*/
            if (keyboard.IsKeyDown(Keys.D))
            {
                Collision next_b = (new Collision(new Rectangle(HitboxD.Rectangle.X + 8, HitboxD.Rectangle.Y, HitboxD.Rectangle.Width, HitboxD.Rectangle.Height)));
                if (next_b.is_coll(rect) == false)
                    velocity.X = 8;
                else
                    velocity.X = 0;

                hasFliped = false;
            }
            else if (keyboard.IsKeyDown(Keys.Q))
            {
                if (HitboxG.is_coll(rect) == false)
                    velocity.X = -8;
                else
                    velocity.X = 0;

                hasFliped = true;
            }
            else
                velocity.X = velocity.X / 1.15f;
            #endregion


            #region/*Faux*/
            if (!hasFliped)
            {
                Faux_damageBox_ground.X = (int)position.X + 57;
                Faux_damageBox_ground.Y = (int)position.Y - 17;
                Faux_damageBox_ground.Width = 58;
                Faux_damageBox_ground.Height = 68;

                Faux_damageBox_air.X = (int)position.X + 15;
                Faux_damageBox_air.Y = (int)position.Y + 75;
                Faux_damageBox_air.Width = 80;
                Faux_damageBox_air.Height = 38;

            }
            else
            {
                Faux_damageBox_ground.X = (int)position.X - 81;
                Faux_damageBox_ground.Y = (int)position.Y - 17;
                Faux_damageBox_ground.Width = 58;
                Faux_damageBox_ground.Height = 68;

                Faux_damageBox_air.X = (int)position.X - 42;
                Faux_damageBox_air.Y = (int)position.Y + 75;
                Faux_damageBox_air.Width = 80;
                Faux_damageBox_air.Height = 38;
            }

            if (keyboard.IsKeyDown(Keys.P) && (CurrentWeaponIsFaux) && (latence_Faux == 0) && (!attackFaux_animation_ground) && (!attackFaux_animation_air))
            {
                Ressources.Faux_sound.Play();
                if (HitboxB.is_coll(rect)) attackFaux_animation_ground = true;
                else attackFaux_animation_air = true;
            }
            #region /* attaque sur le sol */
            if (attackFaux_animation_ground)
            {
                latence_Faux = 30;

                if (attackFaux_animation1 < 4) /* la faux descend */
                {
                    Faux_damageBox_ground_isActivate = true;
                    Faux_rotation += 0.3f;
                    attackFaux_animation1 += 1;
                }
                else
                {
                    if (attackFaux_animation2 < 10) /* la faux remonte */
                    {
                        Faux_damageBox_ground_isActivate = false;
                        Faux_rotation -= 0.12f;
                        attackFaux_animation2 += 1;
                    }
                    else                          /* on remet les variables comme il faut pour une prochaine relecture */
                    {
                        attackFaux_animation1 = 0;
                        attackFaux_animation2 = 0;
                        attackFaux_animation_ground = false; /* animation terminé */
                    }
                }

            }
            #endregion
            #region /* attaque en l'air */
            if (attackFaux_animation_air)
            {
                latence_Faux = 25;

                if (attackFaux_animation1 < 6) /* la faux descend */
                {
                    Faux_damageBox_ground_isActivate = true;
                    Faux_damageBox_air_isActivate = true;
                    Faux_rotation += 0.4f;
                    attackFaux_animation1 += 1;
                }
                else
                {
                    if (attackFaux_animation2 < 16) /* la faux remonte */
                    {
                        Faux_damageBox_ground_isActivate = false;
                        Faux_damageBox_air_isActivate = false;
                        Faux_rotation -= 0.15f;
                        attackFaux_animation2 += 1;
                    }
                    else                          /* on remet les variables comme il faut pour une prochaine relecture */
                    {
                        attackFaux_animation1 = 0;
                        attackFaux_animation2 = 0;
                        attackFaux_animation_air = false; /* animation terminé */
                    }
                }

            }
            #endregion
            #endregion     
            if (position.X < 0)
                position.X = 0;
            if (position.X > 800-texture.Width)
                position.X = 800-texture.Width;
            #region/*Smoke*/
            if ((HitboxB.is_coll(rect)) && latenceSmoke == 0)
            {
                latenceSmoke = 5;
                for (int i = 0; i < Smoke.Length; i++)
                {
                    if (Smoke[i] == null)
                    {
                        if (((HitboxD.is_coll(rect)) | HitboxG.is_coll(rect)))
                        {
                            Smoke[i] = new Smoke(texture.Bounds.X, badrandom, this);
                        }
                        else
                        {
                            Smoke[i] = new Smoke(texture.Bounds.X, (velocity.X / 6) + badrandom, this);
                        }
                        if (badrandom > 0)
                        {
                            badrandom = -0.05f * rnd.Next(4, 8);
                        }
                        else
                        {
                            badrandom = 0.05f * rnd.Next(4, 8);
                        }

                        break;
                    }
                }
            }
            if (latenceSmoke > 0)
                latenceSmoke--;
            #endregion
            #region /*Tirs*/
            if (keyboard.IsKeyDown(Keys.P) && latenceTir == 0&&(!CurrentWeaponIsFaux))
            {
                latenceTir = 20;
                for (int i = 0; i < Tirs.Length; i++)
                {
                    if (Tirs[i] == null)
                    {
                        Tirs[i] = new Projectile(10, 10, this, hasFliped);
                        Ressources.tir_son.Play();
                        break;
                    }
                }
            }
            if (latenceTir > 0)
                latenceTir--;
            #endregion
            

            base.Update2(rect,Keyboard.GetState());
            if (latence_Faux > 0)
            {
                latence_Faux--;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            Life.Draw(sb, 10, 10,1,15);

            #region/*truc pour la faux */

            //sb.Draw(Ressources.lifebar, Faux_damageBox_ground, Color.White); /* afficher les hitBox de la faux */
            //sb.Draw(Ressources.lifebar, Faux_damageBox_air, Color.Green);

            if (this.hasFliped)
            {
                tmp = new Vector2(250, 300);
                faux_pos.X = (this.position.X + 25);
                faux_pos.Y = (this.position_Y_texture + 30);
                Faux = Ressources.Faux_flipped;
            }
            else
            {
                tmp = new Vector2(0, 300);
                faux_pos.X = (this.position.X + 25);
                faux_pos.Y = (this.position_Y_texture + 30);
                Faux = Ressources.Faux;
            }

            #endregion

            if (this.hasFliped)
            {
                 if (CurrentWeaponIsFaux) sb.Draw(Faux, faux_pos, null, Color.White, -Faux_rotation, tmp, 0.27f, SpriteEffects.None, 1);

                 sb.Draw(Ressources.PlayerFlip, new Vector2(this.position.X, this.position_Y_texture), Color.White);
            }
            else
            {
                if (CurrentWeaponIsFaux) sb.Draw(Faux, faux_pos, null, Color.White, Faux_rotation, tmp,0.27f, SpriteEffects.None, 1);

                sb.Draw(this.texture, new Vector2(this.position.X, this.position_Y_texture), Color.White);

            }
            sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
         
        }

    }
}
