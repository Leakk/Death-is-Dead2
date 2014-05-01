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
    class Player : Entity
    {
        public Projectile[] Tirs;
        float position_Y_texture;

        Random rnd = new Random();
        public Smoke[] Smoke;
        float badrandom;

        int latenceTir = 0;
        int latenceSmoke = 0;
        int k = 5;

        public Player(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {
            Tirs = new Projectile[10];
            Smoke = new Smoke[20];
            position_Y_texture = position.Y;
            this.life = life;
            Life = new Life();
        }


        public void Update(KeyboardState keyboard, Obstacle[] rect)
        {
            #region /*Flottement*/
            if (k < 80)
            {
                if (k < 40)
                {
                    position_Y_texture = position.Y - k * 0.25f;
                    k++;
                }
                else
                {
                    position_Y_texture = position.Y + (k * 0.25f) - 20;
                    k++;
                }
            }
            else
                k = 0;
            #endregion

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

            if (position.X < 0)
                position.X = 0;
            if (position.X > 800-texture.Width)
                position.X = 800-texture.Width;

            #region /*Tirs*/
            if (keyboard.IsKeyDown(Keys.P) && latenceTir == 0)
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

            base.Update(rect);
            Life.Udapte(life);

            if (position.X <= 4 && HitboxD.is_coll(rect) || life <= 0 || position.Y>600)
                dead = true;
            else
                dead = false;


        }

        public void Draw(SpriteBatch sb)
        {
            Life.Draw(sb, 10, 10,1,15);

            if (hasFliped)
                sb.Draw(Ressources.PlayerFlip, new Vector2(position.X, position_Y_texture), Color.White);
            else
                sb.Draw(Ressources.Player, new Vector2(position.X, position_Y_texture), Color.White);

            sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }

    }
}
