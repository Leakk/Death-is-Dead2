using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Death_is_Dead
{
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle rectangle;

        Color colour1;

        public Vector2 size;

        public cButton(Texture2D newTexture,Color colour, GraphicsDevice graphics)
        {
            this.colour1 = colour;
            texture = newTexture;

            /* ScreenW =800, ScreenH=600
               ImgW=100,ImgH=20 */

            /* la divison par 8 en Width et par 30 en height est a régler en fonction de la taille de la fenetre */

            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height /12);

        }

        bool down;
        public bool isClicked;
        bool for_sound = false;
        bool for_sound2 = true;

        public void Udapte(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                for_sound = true;

                if ((for_sound == true) & (for_sound2 == true))
                    Ressources.button_sound.Play();

                for_sound2 = false;

                colour1.G = 255;
                colour1.B = 255;

                if (colour1.A == 255) down = false;
                if (colour1.A == 0) down = true;
                if (down) colour1.A += 3; else colour1.A -= 3;

                if (mouse.LeftButton == ButtonState.Pressed)
                    isClicked = true;
                else
                    isClicked = false;
            }
            else
            {
                colour1.G = 0;
                colour1.B = 0;
                for_sound = false;
                for_sound2 = true;
                isClicked = false;

                if (colour1.A < 255)
                    colour1.A += 3; 
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour1);
        }
    }
}
