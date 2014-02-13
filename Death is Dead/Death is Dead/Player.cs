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
        private Vector2 position;
        private Vector2 velocity;
        private Physics_Engine physics;
        private Texture2D texture;
        private bool hasJumped;

        //Constructors

        public Player(Vector2 position, Texture2D texture)
        {
            this.position = position;
            physics = new Physics_Engine(0.20f, 50);
            this.texture = texture;
            this.hasJumped = false;
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

        //Methods

        //Update & Draw

        public void Update(KeyboardState keyboard)
        {
            this.position += this.velocity;
            if (!physics.touched_floor(this.position))
            {
                this.velocity = physics.apply_gravity(this.velocity);
                hasJumped = false;
            }
            else
            {
                velocity.Y = 0;
                position.Y = 550;
                hasJumped = true;
            }

            if (keyboard.IsKeyDown(Keys.Space) && hasJumped)
            {
                hasJumped = false;
                this.velocity.Y = -12f;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X = 8;
            }
            else if (keyboard.IsKeyDown(Keys.Q))
            {
                velocity.X = -8;
            }
            else
            {
                velocity.X = velocity.X/1.15f;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

    }
}
