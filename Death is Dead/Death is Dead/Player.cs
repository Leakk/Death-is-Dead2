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
        private Collision HitboxB;
        private Collision HitboxH;
        private Collision HitboxG;
        private Collision HitboxD;
        private Projectile[] tirs;

        //Constructors

        public Player(Vector2 position, Texture2D texture)
        {
            this.HitboxH = new Collision( new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), texture.Width, 3));
            this.HitboxB = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + texture.Height - 3, texture.Width, 3));
            this.HitboxG = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), 3, texture.Height));
            this.HitboxD = new Collision(new Rectangle(Convert.ToInt32(position.X) + texture.Width - 3, Convert.ToInt32(position.Y), 3, texture.Height));
            this.position = position;
            physics = new Physics_Engine(0.20f, 50);
            this.texture = texture;
            this.hasJumped = false;
            tirs = new Projectile[10];
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

        //Methods



        //Update & Draw
        int iiiiiiiiiiii = 0;

        public void Update(KeyboardState keyboard, Rectangle[] rect)
        {
            Rectangle plate = new Rectangle(500, 500, texture.Width, texture.Height);

            this.position += this.velocity;
            if (!physics.touched_floor(this.position))
            {

                HitboxH = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), texture.Width, 3));
                HitboxB = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) + texture.Height - 3, texture.Width, 3));
                HitboxG = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), 3, texture.Height));
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width - 3, Convert.ToInt32(this.position.Y), 3, texture.Height));
                this.velocity = physics.apply_gravity(this.velocity);
                //hasjump = false
                if (position.Y + texture.Height >= 590)
                    velocity.Y += -0.3f;
            }
            else
            {
                HitboxH = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), texture.Width, 3));
                HitboxB = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) + texture.Height - 3, texture.Width, 3));
                HitboxG = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), 3, texture.Height));
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width - 3, Convert.ToInt32(this.position.Y), 3, texture.Height));
                velocity.Y = 0;
                position.Y = 550;
                hasJumped = true;
                if (position.Y + texture.Height >= 590)
                    velocity.Y += -0.3f;

            }

            if (keyboard.IsKeyDown(Keys.Space) && hasJumped)
            {
                hasJumped = false;
                this.velocity.Y = -12f;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                if (HitboxD.is_coll(rect) == false)
                    velocity.X = 8;
                else
                    position.X =  HitboxD.rect_coll(rect).X-Ressources.Player.Width;

            }
            else if (keyboard.IsKeyDown(Keys.Q))
            {
                if (HitboxG.is_coll(rect)==false)
                    velocity.X = -8;
                else
                    position.X = HitboxG.rect_coll(rect).X+Ressources.plateforme.Width;
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
                        tirs[i] = new Projectile(texture.Bounds.X, 20, this);
                        break;
                    }
                }
            }
            if (iiiiiiiiiiii > 0)
                iiiiiiiiiiii--;


        }



        public void Draw(SpriteBatch sb)
        {
            
            sb.Draw(texture, position, Color.White);
            sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }

    }
}
