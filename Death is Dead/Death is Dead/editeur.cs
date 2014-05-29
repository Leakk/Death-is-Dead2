using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Death_is_Dead
{
    class editeur
    {
        public LinkedList<Obstacle> liste;
        public int i, j;
        public Texture2D curfond, curplate;
        public cButton2 fond1;
        cButton2 fond2;
        cButton2 fond3;
        cButton2 plate1;
        cButton2 plate2;
        cButton2 plate3;
        cButton2 sol1;
        cButton2 sol2;
        cButton2 sol3;
        cButton2 enregist;
        Boolean change;
        uint[] tab;

        public editeur()
        {
            change = false;
            i = 0;
            j = 0;
            curfond = Ressources.fond;
            curplate = Ressources.plateforme;
            liste = new LinkedList<Obstacle>();
        }

        public void load(GraphicsDeviceManager graphics )
        {
            fond1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond level 1", Ressources.font);
            fond2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond level 2", Ressources.font);
            fond3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond level 3", Ressources.font);
            plate1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme level 1", Ressources.font);
            plate2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme level 2", Ressources.font);
            plate3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme level 3", Ressources.font);
            sol1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol level 1", Ressources.font);
            sol2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol level 2", Ressources.font);
            sol3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol level 3", Ressources.font);
            enregist = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Enregistrer", Ressources.font);
        }

        public void seri()
        {
            tab = new uint[curfond.Height * curfond.Width];
            curfond.GetData<uint>(tab);
            Obstacle[] listef = new Obstacle[liste.Count];
            int i = 0;
            foreach (Obstacle item in liste)
            {
                listef[i] = item;
                i++;
            }
            IFormatter format = new BinaryFormatter();
            Stream liste1 = new FileStream("map.edi", FileMode.Create, FileAccess.Write);
            Stream liste2 = new FileStream("fond.edi", FileMode.Create, FileAccess.Write);
            format.Serialize(liste1, listef);
            format.Serialize(liste2, tab);
            liste1.Close();
            liste2.Close();
        }


        public void charge(ContentManager cont)
        {
            IFormatter format = new BinaryFormatter();
            Stream liste1 = new FileStream("map.edi", FileMode.Open, FileAccess.Read);
            Stream liste2 = new FileStream("fond.edi", FileMode.Open, FileAccess.Read);
            
            Obstacle[] listef= (Obstacle[])format.Deserialize(liste1);
            tab = (uint[])format.Deserialize(liste2);
            curfond.SetData<uint>(tab);
            liste1.Close();
            liste2.Close();
            LinkedList<Obstacle> lis = new LinkedList<Obstacle>();
            for (int i = 0; i < listef.Length; i++)
            {
                listef[i].maj(cont);
                lis.AddLast(listef[i]);
            }
            liste = lis;
            
            
        }

        public void update()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.C) && i == 0)
            {
                change = !change;
                i = 20;
            }
            if (fond1.isClicked)
            {
                curfond = Ressources.fond;
                tab = new uint[curfond.Height * curfond.Width];
            }
            if (fond2.isClicked)
            { /*fond 2*/
                tab = new uint[curfond.Height * curfond.Width];
            }
            if (plate1.isClicked)
                curplate = Ressources.plateforme;
            if (sol1.isClicked)
                curplate = Ressources.sol;
            if (fond3.isClicked)
            { /*fond 3*/
                tab = new uint[curfond.Height * curfond.Width];
            }



            if (change)
            {
                fond1.setPosition(new Vector2(0, 0));
                fond2.setPosition(new Vector2(0, 50));
                fond3.setPosition(new Vector2(0, 100));
                plate1.setPosition(new Vector2(180, 0));
                plate2.setPosition(new Vector2(180, 50));
                plate3.setPosition(new Vector2(180, 100));
                sol1.setPosition(new Vector2(400, 0));
                sol2.setPosition(new Vector2(400, 50));
                sol3.setPosition(new Vector2(400, 100));
                enregist.setPosition(new Vector2(550, 50));
                fond1.Udapte(Mouse.GetState());
                fond2.Udapte(Mouse.GetState());
                fond3.Udapte(Mouse.GetState());
                plate1.Udapte(Mouse.GetState());
                plate2.Udapte(Mouse.GetState());
                plate3.Udapte(Mouse.GetState());
                sol1.Udapte(Mouse.GetState());
                sol2.Udapte(Mouse.GetState());
                sol3.Udapte(Mouse.GetState());
                enregist.Udapte(Mouse.GetState());
            }
            if (i != 0)
                i--;

            if (j != 0)
                j--;

            if (!change && Mouse.GetState().LeftButton == ButtonState.Pressed && j == 0)
            {
                j = 20;
                liste.AddLast(new Obstacle(new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, curplate.Width, curplate.Height), curplate));
            }
            if (!change && Mouse.GetState().RightButton == ButtonState.Pressed && j == 0)
            {
                try
                {
                    j = 20;
                    liste.RemoveLast();
                }
                catch { }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(curfond, new Rectangle(0, 0, 800, 600), Color.White);
            spriteBatch.Draw(curplate, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, curplate.Width, curplate.Height), Color.White);
            foreach (Obstacle item in liste)
            {
                spriteBatch.Draw(item.texture, item.rectangle, Color.White);
            }
            if (change)
            {
                fond1.Draw(spriteBatch);
                fond2.Draw(spriteBatch);
                fond3.Draw(spriteBatch);
                plate1.Draw(spriteBatch);
                plate2.Draw(spriteBatch);
                plate3.Draw(spriteBatch);
                sol1.Draw(spriteBatch);
                sol2.Draw(spriteBatch);
                sol3.Draw(spriteBatch);
                enregist.Draw(spriteBatch);
                spriteBatch.DrawString(Ressources.font, "pause", new Vector2(300, 250), Color.Red, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                spriteBatch.DrawString(Ressources.font, "C pour reprendre", new Vector2(300, 350), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(Ressources.font, "Clic gauche ajouter", new Vector2(0, 0), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(Ressources.font, "Clic droit retour", new Vector2(0, 50), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(Ressources.font, "C pour le menu", new Vector2(0, 100), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }
    }
}
