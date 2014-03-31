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
    class Player
    {

        //Fields
        public Vector2 position;
        private Vector2 velocity;
        private Physics_Engine physics;
        private Texture2D texture;
        private bool hasJumped;
        private bool hasFliped;
        private Collision HitboxB;
        private Collision HitboxH;
        private Collision HitboxG;
        private Collision HitboxD;
        private Projectile[] tirs;
        private smoke[] smoke1;
        float position_Y_texture;
        Random rnd = new Random();
        float badrandom;
        private Life Life1;
        public int life;

        //Constructors

        public Player(Vector2 position, Texture2D texture, int life)
        {
            this.HitboxH = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), texture.Width, 3));
            this.HitboxB = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + texture.Height - 3, texture.Width, 3));
            this.HitboxG = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), 3, texture.Height));
            this.HitboxD = new Collision(new Rectangle(Convert.ToInt32(position.X) + texture.Width - 3, Convert.ToInt32(position.Y), 3, texture.Height));
            this.position = position;
            physics = new Physics_Engine(0.20f, 50);
            this.texture = texture;
            this.hasJumped = false;
            this.hasFliped = false;
            tirs = new Projectile[10];
            smoke1 = new smoke[20];
            position_Y_texture = Convert.ToInt32(position.Y);
            this.life = life;
            Life1 = new Life();
        }

        //Get & Set

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Projectile[] Tirs
        {
            get { return tirs; }
            set { tirs = value; }
        }
        public smoke[] Smoke
        {
            get { return smoke1; }
            set { smoke1 = value; }
        }

        public bool HasFliped
        {
            get { return hasFliped; }
            set { hasFliped = value; }
        }

        //Methods



        //Update & Draw
        int iiiiiiiiiiii = 0;
        int aaaaaaaaaa = 0;
        int k = 5;
        public void Update(KeyboardState keyboard, Rectangle[] rect)
        {
            this.position += this.velocity;
            Collision next_h = new Collision(new Rectangle(HitboxH.Rectangle.X + 7, HitboxH.Rectangle.Y + Convert.ToInt32(velocity.Y) - 2, HitboxH.Rectangle.Width - 14, HitboxH.Rectangle.Height));
            if (next_h.is_coll(rect))
                velocity.Y = 2;
            Rectangle recta = new Rectangle(0, 800, 100000, 10);
            Collision next = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + 7, Convert.ToInt32(this.position.Y + 2) + texture.Height, texture.Width - 14, 3));
            if (next.is_coll(rect) == false)
            {

                HitboxH = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) - 2, texture.Width, 3));
                HitboxB = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) + texture.Height - 3, texture.Width, 3));
                HitboxG = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), 3, texture.Height));
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width, Convert.ToInt32(this.position.Y), 3, texture.Height));
                this.velocity = physics.apply_gravity(this.velocity);
                hasJumped = false;
                // if ((position.Y + texture.Height >= recta.Y-20) && (position.X >= recta.X) && (position.X <= recta.X + recta.Width))
                //     velocity.Y += -3f;

            }
            else
            {
                recta = next.rect_coll(rect);
                HitboxH = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), texture.Width, 3));
                HitboxB = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) + texture.Height, texture.Width, 3));
                HitboxG = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), 3, texture.Height));
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width, Convert.ToInt32(this.position.Y), 3, texture.Height));
                position.Y = recta.Y - texture.Height;
                hasJumped = true;
                velocity.Y = 0f;
                //if ((position.Y + texture.Height >= recta.Y - 20) && (position.X >= recta.X) && (position.X <= recta.X + recta.Width))
                //    velocity.Y += -0.3f;


            }

            if (k < 80)
            {
                if (k < 40)
                {
                    position_Y_texture = Convert.ToInt32(position.Y) - k * 0.25f;
                    k++;
                }
                else
                {
                    position_Y_texture = Convert.ToInt32(position.Y) + (k * 0.25f) - 20;
                    k++;
                }

            }
            else
                k = 0;

            if (keyboard.IsKeyDown(Keys.Space) && hasJumped)
            {
                hasJumped = false;
                this.velocity.Y = -12f;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                Collision next_b = (new Collision(new Rectangle(HitboxD.Rectangle.X + 8, HitboxD.Rectangle.Y, HitboxD.Rectangle.Width, HitboxD.Rectangle.Height)));
                if (next_b.is_coll(rect) == false)
                    velocity.X = 8;
                else
                {
                    velocity.X = 0;
                }

                hasFliped = false;

            }
            else if (keyboard.IsKeyDown(Keys.Q))
            {
                if (HitboxG.is_coll(rect) == false)
                    velocity.X = -8;
                else
                {
                    velocity.X = 0;
                }

                hasFliped = true;
            }
            else
            {

                velocity.X = velocity.X / 1.15f;
            }

            if (position.X < 0)
                position.X = 0;

            if (keyboard.IsKeyDown(Keys.P) && iiiiiiiiiiii == 0)
            {
                iiiiiiiiiiii = 20;
                for (int i = 0; i < tirs.Length; i++)
                {
                    if (tirs[i] == null)
                    {
                        tirs[i] = new Projectile(texture.Bounds.X, 10, this, hasFliped);
                        Ressources.tir_son.Play();
                        break;
                    }
                }
            }
            if (iiiiiiiiiiii > 0)
                iiiiiiiiiiii--;


            #region/*smoke*/
            if ((HitboxB.is_coll(rect)) && aaaaaaaaaa == 0)
            {
                aaaaaaaaaa = 5;
                for (int i = 0; i < smoke1.Length; i++)
                {
                    if (smoke1[i] == null)
                    {
                        if (((HitboxD.is_coll(rect)) | HitboxG.is_coll(rect)))
                        {
                            smoke1[i] = new smoke(texture.Bounds.X, badrandom, this);
                        }
                        else
                        {
                            smoke1[i] = new smoke(texture.Bounds.X, (velocity.X / 6) + badrandom, this);
                        }
                        if (badrandom > 0)
                        {
                            badrandom = -0.05f * rnd.Next(4, 8);
                        }
                        else
                        {
                            badrandom = 0.05f * rnd.Next(4,8);
                        }

                        break;
                    }
                }
            }
            if (aaaaaaaaaa > 0)
                aaaaaaaaaa--;
            #endregion

        }



        public void Draw(SpriteBatch sb)
        {
            Life1.Udapte(life);
            Life1.Draw(sb,10,10);
            if (hasFliped)
                sb.Draw(Ressources.PlayerFlip, new Vector2(position.X, position_Y_texture), Color.White);
            else
                sb.Draw(Ressources.Player, new Vector2(position.X, position_Y_texture), Color.White);
            //sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }

    }
}
