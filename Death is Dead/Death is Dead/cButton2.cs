using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class cButton2
    {
        
        SpriteFont font;
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        string text;


        public Vector2 size;

        public cButton2(Texture2D newTexture, GraphicsDevice graphics, string tex, SpriteFont fo)
        {
            
            font =  fo;
            text = tex;
            texture = newTexture;
            rectangle = new Rectangle(10, 10, 100, 100);
        }

        public bool isClicked;

        public void Udapte(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, text.Length*9 + 40,40 );
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
               
                if (mouse.LeftButton == ButtonState.Pressed)
                    isClicked = true;
                else
                    isClicked = false;
            }
            else
            {
                isClicked = false;

            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(font,text,new Vector2(position.X+20,position.Y+5),Color.Red,0,Vector2.Zero,1,SpriteEffects.None,0);
        }
    }
}
