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
    class Entity
    {
        public Vector2 position;
        public Vector2 velocity;
        public Physics_Engine physics;
        public Texture2D texture;
        public bool hasJumped;
        public bool hasFliped;
        public Collision HitboxB;
        public Collision HitboxH;
        public Collision HitboxG;
        public Collision HitboxD;
        public int life;
        bool joueur;


        public Entity(Vector2 position, Texture2D texture, int life, bool play)
        {
            this.position = position;
            this.velocity = Vector2.Zero;
            physics = new Physics_Engine(0.20f, 50);
            this.texture = texture;
            this.hasJumped = false;
            this.hasFliped = false;
            this.HitboxG = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), 3, texture.Height));
            this.HitboxD = new Collision(new Rectangle(Convert.ToInt32(position.X) + texture.Width - 3, Convert.ToInt32(position.Y), 3, texture.Height - 3));
            this.HitboxH = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), texture.Width, 3));
            this.HitboxB = new Collision(new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y) + texture.Height - 3, texture.Width, 10));
            joueur = play;
        }

        public void Update(Obstacle[] rect)
        {
            position.X--;
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
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width, Convert.ToInt32(this.position.Y), 3, texture.Height - 20));
                this.velocity = physics.apply_gravity(this.velocity);
                hasJumped = false;

            }
            else
            {
                recta = next.rect_coll(rect);
                HitboxH = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), texture.Width, 3));
                HitboxB = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y) + texture.Height, texture.Width, 3));
                HitboxG = new Collision(new Rectangle(Convert.ToInt32(this.position.X), Convert.ToInt32(this.position.Y), 3, texture.Height));
                HitboxD = new Collision(new Rectangle(Convert.ToInt32(this.position.X) + texture.Width, Convert.ToInt32(this.position.Y), 3, texture.Height - 20));
                position.Y = recta.Y - texture.Height;
                hasJumped = true;
                velocity.Y = 0f;
            }

            if (HitboxD.is_coll(rect))
                position.X = HitboxD.rect_coll(rect).X - texture.Width;
        }
    }
}
