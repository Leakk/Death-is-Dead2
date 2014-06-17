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
        static public LinkedList<Obstacle> liste;
        public Obstacle[] map;
        public LinkedList<Mob> mob;
        public Mob[] ennemi;
        Vector2 flag;
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
        cButton2 E1;
        cButton2 E2;
        cButton2 E3, flagg, E4;
        cButton gauche, droite;
        Boolean change;
        uint[] tab;
        int a;
        Boolean plate;
        Boolean ene;
        Boolean drap;
        int choix = 0;
        public int lang;

        public editeur(int x)
        {
            lang = x;
            change = false;
            i = 0;
            j = 0;
            curfond = Ressources.fond;
            curplate = Ressources.plateforme;
            liste = new LinkedList<Obstacle>();
            a = 0;
            map = new Obstacle[0];
            plate = true;
            ennemi = new Mob[0];
            mob = new LinkedList<Mob>();
            flag = new Vector2(0, -500);
            drap = false;
            ene = false;
        }

        public void load(GraphicsDeviceManager graphics)
        {
            fond1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 1", Ressources.font);
            fond2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 2", Ressources.font);
            fond3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 3", Ressources.font);
            plate1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 1", Ressources.font);
            plate2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 2", Ressources.font);
            plate3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 3", Ressources.font);
            sol1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 1", Ressources.font);
            sol2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 2", Ressources.font);
            sol3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 3", Ressources.font);
            E1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Ennemi 1", Ressources.font);
            E2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  1", Ressources.font);
            E3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  2", Ressources.font);
            E4 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Ennemi 2", Ressources.font);
            flagg = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Drapeau", Ressources.font);

            if (lang == 1)
            {
                fond1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 1", Ressources.font);
                fond2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 2", Ressources.font);
                fond3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fond niveau 3", Ressources.font);
                plate1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 1", Ressources.font);
                plate2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 2", Ressources.font);
                plate3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateforme niveau 3", Ressources.font);
                sol1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 1", Ressources.font);
                sol2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 2", Ressources.font);
                sol3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "sol niveau 3", Ressources.font);
                E1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Ennemi 1", Ressources.font);
                E2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  1", Ressources.font);
                E3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  2", Ressources.font);
                E4 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Ennemi 2", Ressources.font);
                flagg = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Drapeau", Ressources.font);
            }
            if (lang == 2)
            {
                fond1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Background 1", Ressources.font);
                fond2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Background 2", Ressources.font);
                fond3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Background 3", Ressources.font);
                plate1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateform level 1", Ressources.font);
                plate2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateform level 2", Ressources.font);
                plate3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plateform level 3", Ressources.font);
                sol1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "ground level 1", Ressources.font);
                sol2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "ground level 2", Ressources.font);
                sol3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "ground level 3", Ressources.font);
                E1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Enemy 1", Ressources.font);
                E2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  1", Ressources.font);
                E3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Bonus  2", Ressources.font);
                E4 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Enemy 2", Ressources.font);
                flagg = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "Flag", Ressources.font);
            }
            if (lang == 3)
            {
                fond1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fondo nivel 1", Ressources.font);
                fond2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fondo nivel 2", Ressources.font);
                fond3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "fondo nivel 3", Ressources.font);
                plate1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plataforma nivel 1", Ressources.font);
                plate2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plataforma nivel 2", Ressources.font);
                plate3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "plataforma nivel 3", Ressources.font);
                sol1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "suelo nivel 1", Ressources.font);
                sol2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "suelo nivel 2", Ressources.font);
                sol3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "suelo nivel 3", Ressources.font);
                E1 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "enemigo 1", Ressources.font);
                E2 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "prima  1", Ressources.font);
                E3 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "prima  2", Ressources.font);
                E4 = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "enemigo 2", Ressources.font);
                flagg = new cButton2(Ressources.boutton, graphics.GraphicsDevice, "bandera", Ressources.font);
            }

                gauche = new cButton(Ressources.boutton, Color.White, graphics.GraphicsDevice);
                droite = new cButton(Ressources.boutton, Color.White, graphics.GraphicsDevice);
        }

        public void seri()
        {
            map = new Obstacle[liste.Count];
            int x = 0;
            foreach (Obstacle item in liste)
            {
                map[x] = new Obstacle(new Rectangle(item.rectangle.X, item.rectangle.Y, item.rectangle.Width, item.rectangle.Height), item.texture);
                x++;
            }
            ennemi = new Mob[mob.Count];
            x = 0;
            foreach (Mob item in mob)
            {
                    ennemi[x] = new Mob(item.position, item.texture, item.life,item.type);
                x++;
            }
            tab = new uint[curfond.Height * curfond.Width];
            curfond.GetData<uint>(tab);
            IFormatter format = new BinaryFormatter();
            Stream liste1 = new FileStream("Editeur/map.edi", FileMode.Create, FileAccess.Write);
            Stream liste2 = new FileStream("Editeur/fond.edi", FileMode.Create, FileAccess.Write);
            Stream liste3 = new FileStream("Editeur/mob.edi", FileMode.Create, FileAccess.Write);
            Stream liste4 = new FileStream("Editeur/flag.edi", FileMode.Create, FileAccess.Write);
            format.Serialize(liste1, map);
            format.Serialize(liste2, tab);
            format.Serialize(liste3, ennemi);
            format.Serialize(liste4, flag);
            liste1.Close();
            liste2.Close();
            liste3.Close();
            liste4.Close();
        }


        public void charge(ContentManager cont)
        {
            IFormatter format = new BinaryFormatter();

            try
            {
                Stream liste1 = new FileStream("Editeur/map.edi", FileMode.Open, FileAccess.Read);


                Stream liste2 = new FileStream("Editeur/fond.edi", FileMode.Open, FileAccess.Read);
                Stream liste3 = new FileStream("Editeur/mob.edi", FileMode.Open, FileAccess.Read);
                Stream liste4 = new FileStream("Editeur/flag.edi", FileMode.Open, FileAccess.Read);

                ennemi = (Mob[])format.Deserialize(liste3);
                map = (Obstacle[])format.Deserialize(liste1);
                tab = (uint[])format.Deserialize(liste2);
                flag = (Vector2)format.Deserialize(liste4);
                curfond.SetData<uint>(tab);
                liste1.Close();
                liste2.Close();
                liste3.Close();
                liste = new LinkedList<Obstacle>();
                for (int i = 0; i < map.Length; i++)
                {
                    map[i].maj(cont);
                    liste.AddLast(map[i]);
                }
                mob = new LinkedList<Mob>();
                for (int i = 0; i < ennemi.Length; i++)
                {
                    ennemi[i].maj(cont);
                    ennemi[i] = new Mob(ennemi[i].position, ennemi[i].texture, ennemi[i].life, ennemi[i].type);
                    mob.AddLast(ennemi[i]);
                }

            }
            catch
            {

            }
        }

        public void update()
        {
            int x;
            ennemi = new Mob[mob.Count];
            x = 0;
            foreach (Mob item in mob)
            {
                ennemi[x] = new Mob(new Vector2(item.position.X - a, item.position.Y), item.texture, item.life,item.type);
                x++;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.C) && i == 0)
            {
                change = !change;
                i = 20;
            }
            #region /*fond*/
            if (fond1.isClicked)
            {
                curfond = Ressources.fond;
                tab = new uint[curfond.Height * curfond.Width];
            }
            if (fond2.isClicked)
            {
                curfond = Ressources.fond2;
                tab = new uint[curfond.Height * curfond.Width];
            }
            if (fond3.isClicked)
            {
                curfond = Ressources.fond3;
                tab = new uint[curfond.Height * curfond.Width];
            }
            #endregion
            #region/*plateformes*/
            if (plate1.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.plateforme;
            }
            if (sol1.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.sol;
            }
            if (plate2.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.plateforme2;
            }
            if (sol2.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.sol2;
            }
            if (plate3.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.plateforme3;
            }
            if (sol3.isClicked)
            {
                plate = true;
                ene = false;
                drap = false;
                curplate = Ressources.sol3;
            }
            #endregion
            #region/*ennemis*/
            if (E1.isClicked)
            {
                plate = false;
                ene = true;
                drap = false;
                curplate = Ressources.E2;
                choix = 0;
            }
            if (E2.isClicked)
            {
                plate = false;
                ene = true;
                drap = false;
                curplate = Ressources.Bonus_coeur;
                choix = 1;

            }
            if (E3.isClicked)
            {
                plate = false;
                ene = true;
                drap = false;
                curplate = Ressources.Faux;
                choix = 2;
            }
            if (E4.isClicked)
            {
                plate = false;
                ene = true;
                drap = false;
                curplate = Ressources.E2;
                choix = 3;
            }
            #endregion
            if (flagg.isClicked)
            {
                plate = false;
                ene = false;
                drap = true;
                curplate = Ressources.Flag_final;
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
                E1.setPosition(new Vector2(560, 0));
                E2.setPosition(new Vector2(560, 50));
                E3.setPosition(new Vector2(560, 100));
                E4.setPosition(new Vector2(680, 0));
                flagg.setPosition(new Vector2(680, 50));
                fond1.Udapte(Mouse.GetState());
                fond2.Udapte(Mouse.GetState());
                fond3.Udapte(Mouse.GetState());
                plate1.Udapte(Mouse.GetState());
                plate2.Udapte(Mouse.GetState());
                plate3.Udapte(Mouse.GetState());
                sol1.Udapte(Mouse.GetState());
                sol2.Udapte(Mouse.GetState());
                sol3.Udapte(Mouse.GetState());
                E1.Udapte(Mouse.GetState());
                E2.Udapte(Mouse.GetState());
                E3.Udapte(Mouse.GetState());
                E4.Udapte(Mouse.GetState());
                flagg.Udapte(Mouse.GetState());
            }
            else
            {
                gauche.setPosition(new Vector2(0, 300));
                droite.setPosition(new Vector2(600, 300));
                gauche.Udapte(Mouse.GetState());
                droite.Udapte(Mouse.GetState());
            }
            if (droite.isClicked)
            {
                a += 10;
                ennemi = new Mob[mob.Count];
                x = 0;
                foreach (Mob item in mob)
                {
                            ennemi[x] = new Mob(new Vector2(item.position.X - a, item.position.Y), item.texture, item.life,item.type);
                    x++;
                }
                map = new Obstacle[liste.Count];
                x = 0;
                foreach (Obstacle item in liste)
                {
                    map[x] = new Obstacle(new Rectangle(item.rectangle.X - a, item.rectangle.Y, item.rectangle.Width, item.rectangle.Height), item.texture);
                    x++;
                }
            }
            if (gauche.isClicked && a > 0)
            {
                a -= 10;
                ennemi = new Mob[mob.Count];
                x = 0;
                foreach (Mob item in mob)
                {
                        ennemi[x] = new Mob(new Vector2(item.position.X - a, item.position.Y), item.texture, item.life,item.type);
                    x++;
                }
                map = new Obstacle[liste.Count];
                x = 0;
                foreach (Obstacle item in liste)
                {
                    map[x] = new Obstacle(new Rectangle(item.rectangle.X - a, item.rectangle.Y, item.rectangle.Width, item.rectangle.Height), item.texture);
                    x++;
                }
            }
            if (i != 0)
                i--;

            if (j != 0)
                j--;

            if (!change && Mouse.GetState().LeftButton == ButtonState.Pressed && j == 0 && !droite.isClicked && !gauche.isClicked)
            {



                j = 20;
                if (plate)
                {
                    liste.AddLast(new Obstacle(new Rectangle((int)Mouse.GetState().X + a, (int)Mouse.GetState().Y, curplate.Width, curplate.Height), curplate));
                    map = new Obstacle[liste.Count];
                    x = 0;
                    foreach (Obstacle item in liste)
                    {
                        map[x] = new Obstacle(new Rectangle(item.rectangle.X - a, item.rectangle.Y, item.rectangle.Width, item.rectangle.Height), item.texture);
                        x++;
                    }
                }
                if (ene)
                {
                    mob.AddLast(new Mob(new Vector2((int)Mouse.GetState().X + a, (int)Mouse.GetState().Y), curplate, 100, choix));
                    ennemi = new Mob[mob.Count];
                    x = 0;
                    foreach (Mob item in mob)
                    {
                            ennemi[x] = new Mob(new Vector2(item.position.X - a, item.position.Y), item.texture, item.life,item.type);
                        x++;
                    }
                }
                if (drap)
                {
                    flag = new Vector2((int)Mouse.GetState().X + a, (int)Mouse.GetState().Y);
                }

            }
            if (!change && Mouse.GetState().RightButton == ButtonState.Pressed && j == 0)
            {
                try
                {
                    if (plate)
                    {
                        liste.RemoveLast();
                        map = new Obstacle[liste.Count];
                        x = 0;
                        foreach (Obstacle item in liste)
                        {
                            map[x] = new Obstacle(new Rectangle(item.rectangle.X - a, item.rectangle.Y, item.rectangle.Width, item.rectangle.Height), item.texture);
                            x++;
                        }
                    }
                    if (ene)
                    {
                        mob.RemoveLast();
                        ennemi = new Mob[mob.Count];
                        x = 0;
                        foreach (Mob item in mob)
                        {
                                ennemi[x] = new Mob(new Vector2(item.position.X - a, item.position.Y), item.texture, item.life,item.type);
                            x++;
                        }
                    }
                    j = 20;
                }
                catch { }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(curfond, new Rectangle((int)-0.25 * a, 0, 2400, 600), Color.White);
            if (drap)
            {
                spriteBatch.Draw(curplate, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 70, 160), Color.White);
            }
            else if (curplate == Ressources.Bonus_coeur || curplate == Ressources.Faux)
                spriteBatch.Draw(curplate, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 50, 50), Color.White);
            else
                spriteBatch.Draw(curplate, new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, curplate.Width, curplate.Height), Color.White);
            foreach (Obstacle item in map)
            {
                spriteBatch.Draw(item.texture, item.rectangle, Color.White);

            }
            spriteBatch.Draw(Ressources.Flag_final, new Rectangle((int)flag.X - a, (int)flag.Y, 70, 160), Color.White);
            foreach (Mob item in ennemi)
            {

                        item.Draw(spriteBatch);

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
                E1.Draw(spriteBatch);
                E2.Draw(spriteBatch);
                E3.Draw(spriteBatch);
                E4.Draw(spriteBatch);
                flagg.Draw(spriteBatch);
                if (lang == 1)
                {
                    spriteBatch.DrawString(Ressources.font, "pause", new Vector2(300, 250), Color.Red, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C pour reprendre", new Vector2(300, 350), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (lang == 2)
                {
                    spriteBatch.DrawString(Ressources.font, "pause", new Vector2(300, 250), Color.Red, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C to resume", new Vector2(300, 350), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (lang == 3)
                {
                    spriteBatch.DrawString(Ressources.font, "pausa", new Vector2(300, 250), Color.Red, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C para continuar", new Vector2(300, 350), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
            else
            {
                gauche.Draw(spriteBatch);
                droite.Draw(spriteBatch);
                if (lang == 1)
                {
                    spriteBatch.DrawString(Ressources.font, "Clic gauche ajouter", new Vector2(0, 0), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "Clic droit retour", new Vector2(0, 50), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C pour le menu", new Vector2(0, 100), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (lang == 2)
                {
                    spriteBatch.DrawString(Ressources.font, "Left clic Add", new Vector2(0, 0), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "Right clic remove last add", new Vector2(0, 50), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C back to menu", new Vector2(0, 100), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
                if (lang == 3)
                {
                    spriteBatch.DrawString(Ressources.font, "Clic izquierdo anadir", new Vector2(0, 0), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "Clic derecho retorno", new Vector2(0, 50), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.DrawString(Ressources.font, "C Volver al menu", new Vector2(0, 100), Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }
    }
}
