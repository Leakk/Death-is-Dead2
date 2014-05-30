using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Death_is_Dead
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Microsoft.Xna.Framework.Game
    {
        enum GameState
        {
            MainMenu,
            Option,
            Playing,
            Playing_2P,
            Paused,
            Paused_editor,
            GameOver,
            Editor
        }

        enum language
        {
            english,
            french,
            spanish,
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect button_click;
        Song Menu_song;
        Song Game_song_lvl1;
        private Color colour1 = new Color(255, 0, 0, 255);
        // private Color btnColorPause = new Color(255, 0, 0, 255);
        public KeyboardState keyboardState;
        public string lang = "";
        public Texture2D curFond;


        #region  /*Pour les boutons*/
        bool playOnce = false;
        int count = 0;
        int count_save = 0;
        #endregion

        #region/*boutton editeur*/
        editeur editeur1 = new editeur();
        #endregion


        language Currentlanguage;
        GameState CurrentGameState = GameState.MainMenu;

        // Screen Adjustments 
        int screenWidth = 800, screenHeight = 600;

        private Player player1;
        private Map map1;
        private Map map = new Map(new Obstacle[0], new Mob[0],Ressources.fond);
        private Player2 player2;
        private bool songisplayed = false;

        /* attention si on change la résolution in faudra peut etre modifier dans la classe cButton le public cButton  :
         * dans le size  le "graphics.Viewport.Width / 8, graphics.Viewport.Height / 30" la valeur des 2 divisions est a changer */

        #region /*Boutons*/
        /*main_menu*/
        cButton btnPlay;
        cButton btnOption;
        cButton btnMultiplayer;
        cButton btnExit;
        cButton btnLoad;
        cButton btnEditor;
        /*Option*/
        cButton btnRes;
        cButton btnBack;
        cButton btnApply_change;
        cButton btnSound_volume;
        cButton btnLanguage;
        /*pause*/
        cButton btnBackToMenu;
        cButton btnResume;
        cButton btnSave;
        /*pause_editeur*/
        cButton btnBackToMenu_editor;
        cButton btnResume_editor;
        cButton btnSave_editor;
        cButton btnLoad_editor;
        /*gameOver*/
        cButton btnBackFromGameOver;
        #endregion

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Physics Engine";
        }

        protected override void Initialize()
        {
            Currentlanguage = language.french;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // screen stuff
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            // graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            #region /*Pour le menu*/


            if (Currentlanguage == language.english) lang = "eng";
            if (Currentlanguage == language.french) lang = "fre";
            if (Currentlanguage == language.spanish) lang = "spa";

            /* MainMenu*/
            btnPlay = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_play"), colour1, graphics.GraphicsDevice);
            btnMultiplayer = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_play_mult"), colour1, graphics.GraphicsDevice);
            btnOption = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_option"), colour1, graphics.GraphicsDevice);
            btnExit = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Exit"), colour1, graphics.GraphicsDevice);
            btnLoad = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Load"), colour1, graphics.GraphicsDevice);
            btnEditor = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/editor"), colour1, graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(500, 100));
            btnLoad.setPosition(new Vector2(500,180));
            btnMultiplayer.setPosition(new Vector2(500, 260));
            btnEditor.setPosition(new Vector2(500, 340));
            btnOption.setPosition(new Vector2(500, 420));
            btnExit.setPosition(new Vector2(500, 500));

            /*Option*/
            btnBack = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnRes = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/fullscreen"), colour1, graphics.GraphicsDevice);
            btnLanguage = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/language"), colour1, graphics.GraphicsDevice);
            btnBack.setPosition(new Vector2(40, 500));
            btnRes.setPosition(new Vector2(40, 200));
            btnLanguage.setPosition(new Vector2(40, 100));

            button_click = (Content.Load<SoundEffect>("Sound_effects/Menu/button_sound_click"));
            Menu_song = (Content.Load<Song>("Sound_effects/Menu/Menu_music")); //Ressources.Menu_song;
            Game_song_lvl1 = (Content.Load<Song>("Sound_effects/Game/Game_music")); //Ressources.Game_song_lvl1;
            #endregion

            #region/*Pour le menu pause*/
            btnBackToMenu = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnResume = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/resume"), colour1, graphics.GraphicsDevice);
            btnSave = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/save"), colour1, graphics.GraphicsDevice);
            btnBackToMenu.setPosition(new Vector2(300, 400));
            btnSave.setPosition(new Vector2(300,300));
            btnResume.setPosition(new Vector2(300, 200));
            #endregion
            #region /* Pour le menu pause de l'editeur */
            btnBackToMenu_editor = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnResume_editor = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/resume"), colour1, graphics.GraphicsDevice);
            btnSave_editor = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/save"), colour1, graphics.GraphicsDevice);
            btnLoad_editor = new cButton(Content.Load<Texture2D>("sprite/paused_editor/" + lang + "/load"), colour1, graphics.GraphicsDevice);

            btnBackToMenu_editor.setPosition(new Vector2(300, 500));
            btnSave_editor.setPosition(new Vector2(300, 300));
            btnLoad_editor.setPosition(new Vector2(300, 400));
            btnResume_editor.setPosition(new Vector2(300, 200));
            #endregion
            #region/*Menu Game over*/
            btnBackFromGameOver = new cButton(Content.Load<Texture2D>("sprite/Game_over/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnBackFromGameOver.setPosition(new Vector2(10, 300));
            #endregion
            #region/*editeur*/
            editeur1.load(graphics);
            #endregion
            Ressources.Load(Content);

        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

   
        protected override void Update(GameTime gameTime)
        {
            
            map1 = new Map(new Obstacle[21] { new Obstacle(new Rectangle(0, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(300, 360, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(1040, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(1800, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2100, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2540, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2840, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(3450, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme/*carre*/), new Obstacle(new Rectangle(3850, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4450, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4850, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(5350, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6600, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6850, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7250, 300, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7650, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(7700, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8150, 150, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8550, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(9550, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(9200, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol) }, new Mob[5] { new Mob(new Vector2(1250, 400), Ressources.Player, 100), new Mob(new Vector2(3370, 400), Ressources.Player, 100), new Mob(new Vector2(5870, 400), Ressources.Player, 100), new Mob(new Vector2(7450, 100), Ressources.Player, 100), new Mob(new Vector2(8700, 400), Ressources.Player, 100) },Ressources.fond);
            MouseState mouse = Mouse.GetState();

            #region /*Les GameStates*/
            switch (CurrentGameState)
            {
                #region/*menu*/
                case GameState.MainMenu:
                    if (!songisplayed)
                    {
                        MediaPlayer.Play(Menu_song);
                        songisplayed = true;
                    }
                    btnPlay.Udapte(mouse);
                    btnLoad.Udapte(mouse);
                    btnMultiplayer.Udapte(mouse);
                    btnOption.Udapte(mouse);
                    btnExit.Udapte(mouse);
                    btnEditor.Udapte(mouse);
                    if (btnPlay.isClicked)
                    {
                        button_click.Play();
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                        player2 = new Player2(new Vector2(-1000, 0), Ressources.Player2, 100);
                        map = map1;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        songisplayed = false;
                        CurrentGameState = GameState.Playing;
                    }
                    if (btnMultiplayer.isClicked)
                    {
                        button_click.Play();
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                        player2 = new Player2(new Vector2(350, 0), Ressources.Player2, 100);
                        map = map1;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        songisplayed = false;
                        CurrentGameState = GameState.Playing_2P;
                    }
                    if (btnOption.isClicked)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.Option;
                    }
                 
                    if (btnExit.isClicked)
                    {
                        button_click.Play();
                        Exit();
                    }

                    if ((btnLoad.isClicked)&&(count==0))
                    {
                        try
                        {
                            count = 15;
                            button_click.Play();
                            IFormatter format = new BinaryFormatter();
                            Stream liste = new FileStream("p1.save", FileMode.Open, FileAccess.Read);
                            Stream liste2 = new FileStream("p2.save", FileMode.Open, FileAccess.Read);
                            Stream liste3 = new FileStream("map.save", FileMode.Open, FileAccess.Read);
                            Stream liste4 = new FileStream("mobs.save", FileMode.Open, FileAccess.Read);
                            Player P1;
                            Player2 P2;
                            Obstacle[] obs2;
                            Mob[] mobs2;
                            P1 = (Player)format.Deserialize(liste);
                            P2 = (Player2)format.Deserialize(liste2);
                            obs2 = (Obstacle[])format.Deserialize(liste3);
                            mobs2 = (Mob[])format.Deserialize(liste4);
                            player1 = new Player(P1.position, Ressources.Player, P1.life);
                            player2 = new Player2(P2.position, Ressources.Player2, P2.life);
                            foreach (Obstacle item in obs2)
                            {
                                item.maj(Content);
                            }
                            map.obs = obs2;
                            for (int a = 0; a < mobs2.Length; a++)
                            {
                                mobs2[a].maj(Content);
                                mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life);
                            }
                            if (P2.position.X < 0)
                                CurrentGameState = GameState.Playing;
                            else
                                CurrentGameState = GameState.Playing_2P;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Game_song_lvl1);
                        }
                        catch { }
                    
                    }

                    if ((btnEditor.isClicked)&&(count==0))
                    {
                        count = 15;
                        button_click.Play();
                        editeur1 = new editeur();
                        editeur1.load(graphics);
                        editeur1.j = 30;
                        CurrentGameState = GameState.Editor;
                        

                    }
                    if (count != 0)
                        count--;
                    break;
                #endregion
                #region/* case paused*/
                case GameState.Paused:
                    btnResume.Udapte(mouse);
                    btnBackToMenu.Udapte(mouse);
                    btnSave.Udapte(mouse);
                    if ((btnResume.isClicked)&&(count_save==0))
                    {
                        count_save = 15;
                        button_click.Play();
                        CurrentGameState = GameState.Playing;
                    }
                    if (btnBackToMenu.isClicked)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.MainMenu;
                    }
                    if ((btnSave.isClicked) && (count_save == 0))
                    {
                       count_save = 15;
                       button_click.Play();

                       IFormatter format = new BinaryFormatter();
                       Stream liste = new FileStream("p1.save", FileMode.Create, FileAccess.Write);
                       Stream liste2 = new FileStream("p2.save", FileMode.Create, FileAccess.Write);
                       Stream liste3 = new FileStream("map.save", FileMode.Create, FileAccess.Write);
                       Stream liste4 = new FileStream("mobs.save", FileMode.Create, FileAccess.Write);
                       format.Serialize(liste, player1);
                       format.Serialize(liste2, player2);
                       format.Serialize(liste3, map.obs);
                       format.Serialize(liste4, map.mobs);
                       liste.Close();
                       liste2.Close();
                       liste3.Close();
                       liste4.Close();
                    }
                    if (count_save != 0)
                    {
                        count_save--;
                    }
                    break;
                #endregion
                #region/* case paused_editor*/
                case GameState.Paused_editor:
                    btnResume_editor.Udapte(mouse);
                    btnBackToMenu_editor.Udapte(mouse);
                    btnSave_editor.Udapte(mouse);
                    btnLoad_editor.Udapte(mouse);
                    if ((btnResume_editor.isClicked) && (count_save == 0))
                    {
                        count_save = 15;
                        button_click.Play();
                        editeur1.j = 20;
                        CurrentGameState = GameState.Editor;
                    }
                    if (btnBackToMenu_editor.isClicked)
                    {
                        button_click.Play();
                        editeur1.j = 20;
                        CurrentGameState = GameState.MainMenu;
                    }
                    if ((btnSave_editor.isClicked) && (count_save == 0))
                    {
                        count_save = 15;
                        button_click.Play();
                        editeur1.j = 20;
                        editeur1.seri();
                        CurrentGameState = GameState.Editor;

                    }

                    if ((btnLoad_editor.isClicked) && (count_save == 0))
                    {
                        count_save = 15;
                        button_click.Play();
                        editeur1.charge(Content);
                        editeur1.j = 20;
                        CurrentGameState = GameState.Editor;
                    }
                    if (count_save != 0)
                    {
                        count_save--;
                    }
                    break;
                #endregion
                #region/* case Game over*/
                case GameState.GameOver:
                    btnBackFromGameOver.Udapte(mouse);
                    if (btnBackFromGameOver.isClicked)
                    {
                        CurrentGameState = GameState.MainMenu;
                    }
                    break;
                #endregion
                #region/*GameState.Option*/
                case GameState.Option:
                    playOnce = false;
                    btnBack.Udapte(mouse);
                    btnRes.Udapte(mouse);
                    btnLanguage.Udapte(mouse);
                    if (btnBack.isClicked)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.MainMenu;


                    }
                    if ((btnRes.isClicked) && (count == 0))
                    {
                        count = 15;
                        button_click.Play();
                        if (graphics.IsFullScreen == false)
                            graphics.IsFullScreen = true;
                        else graphics.IsFullScreen = false;
                        graphics.ApplyChanges();

                    }
                    if ((btnLanguage.isClicked) && (count == 0))
                    {
                        try
                        {
                            count = 15;
                            switch (Currentlanguage)
                            {
                                case language.english:
                                    Currentlanguage = language.french;
                                    UnloadContent();
                                    LoadContent();
                                    Draw(gameTime);
                                    button_click.Play();
                                    break;
                                case language.french:
                                    Currentlanguage = language.spanish;
                                    UnloadContent();
                                    LoadContent();
                                    Draw(gameTime);
                                    button_click.Play();
                                    break;
                                case language.spanish:
                                    Currentlanguage = language.english;
                                    UnloadContent();
                                    LoadContent();
                                    Draw(gameTime);
                                    button_click.Play();
                                    break;
                            }

                        }
                        catch
                        {
                        }
                    }
                    if (count != 0)
                        count--;
                    break;
                #endregion
                #region/*Editor*/
                case GameState.Editor:

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Paused_editor;
                    }
                    editeur1.update();
                    break;
                # endregion
                #region/*playing*/
                default:
                    #region/*Pause*/
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                CurrentGameState = GameState.Paused;
            }

         
            #endregion
                    if (CurrentGameState == GameState.Playing_2P)
                    {
                        #region /*Update Player2*/
                        player2.Update(Keyboard.GetState(), map.obs);
                        for (int i = 0; i < player2.Tirs.Length; i++)
                        {
                            if (player2.Tirs[i] != null)
                            {
                                if (player2.Tirs[i].position.X > 800 || player2.Tirs[i].position.X < -Ressources.Tir.Width)
                                    player2.Tirs[i] = null;
                                else
                                    player2.Tirs[i].Update();
                                foreach (Mob mob in map.mobs)
                                {
                                    if (mob.isTouched(player2.Tirs[i]))
                                    {
                                        mob.life -= 10;
                                        player2.Tirs[i] = null;
                                    }
                                }
                                if (player2.Tirs[i] != null)
                                    if (new Collision(new Rectangle((int)player2.Tirs[i].position.X, (int)player2.Tirs[i].position.Y, player2.Tirs[i].texture.Width, player2.Tirs[i].texture.Height)).is_coll(map.obs))
                                        player2.Tirs[i] = null;
                            }
                        }
                        #region/*Smoke2*/
                        for (int j = 0; j < player2.Smoke.Length; j++)
                        {
                            if (player2.Smoke[j] != null)
                            {
                                if (player2.Smoke[j].Alpha < 0)
                                {
                                    player2.Smoke[j] = null;

                                }
                                else
                                    player2.Smoke[j].Update();
                            }

                        }
                        #endregion
                        #endregion
                        if (player2.dead)
                        {
                            CurrentGameState = GameState.GameOver;
                            player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                        }
                    }

                    #region /*Update Player*/
                    player1.Update(Keyboard.GetState(), map.obs);
                    for (int i = 0; i < player1.Tirs.Length; i++)
                    {
                        if (player1.Tirs[i] != null)
                        {
                            if (player1.Tirs[i].position.X > 800 || player1.Tirs[i].position.X < -Ressources.Tir.Width)
                                player1.Tirs[i] = null;
                            else
                                player1.Tirs[i].Update();
                            foreach (Mob mob in map.mobs)
                            {
                                if (mob.isTouched(player1.Tirs[i]))
                                {
                                    mob.life -= 10;
                                    player1.Tirs[i] = null;
                                }
                            }
                            if (player1.Tirs[i] != null)
                                if (new Collision(new Rectangle((int)player1.Tirs[i].position.X, (int)player1.Tirs[i].position.Y, player1.Tirs[i].texture.Width, player1.Tirs[i].texture.Height)).is_coll(map.obs))
                                    player1.Tirs[i] = null;

                        }
                    }
                    #endregion

                    #region /*Update Mobs*/
                    foreach (Mob item in map.mobs)
                    {
                        item.Update(map.obs, player1,player2);
                        #region /* faux */
                        if (player1.CurrentWeaponIsFaux)
                        {
                            if (player1.Faux_damageBox_ground_isActivate)
                            {           
                                   if ((item.HitboxB.Rectangle.Intersects(player1.Faux_damageBox_ground))
                                    ||(item.HitboxH.Rectangle.Intersects(player1.Faux_damageBox_ground)
                                    ||(item.HitboxG.Rectangle.Intersects(player1.Faux_damageBox_ground)
                                    ||(item.HitboxD.Rectangle.Intersects(player1.Faux_damageBox_ground))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -=15;
                                }
                            }

                            if (player1.Faux_damageBox_air_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player1.Faux_damageBox_air))
                                 || (item.HitboxH.Rectangle.Intersects(player1.Faux_damageBox_air)
                                 || (item.HitboxG.Rectangle.Intersects(player1.Faux_damageBox_air)
                                 || (item.HitboxD.Rectangle.Intersects(player1.Faux_damageBox_air))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 15;
                                }
                            }
                        }

                        if (player2.CurrentWeaponIsFaux)
                        {
                            if (player2.Faux_damageBox_ground_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player2.Faux_damageBox_ground))
                                 || (item.HitboxH.Rectangle.Intersects(player2.Faux_damageBox_ground)
                                 || (item.HitboxG.Rectangle.Intersects(player2.Faux_damageBox_ground)
                                 || (item.HitboxD.Rectangle.Intersects(player2.Faux_damageBox_ground))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 20;
                                }
                            }

                            if (player2.Faux_damageBox_air_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player2.Faux_damageBox_air))
                                 || (item.HitboxH.Rectangle.Intersects(player2.Faux_damageBox_air)
                                 || (item.HitboxG.Rectangle.Intersects(player2.Faux_damageBox_air)
                                 || (item.HitboxD.Rectangle.Intersects(player2.Faux_damageBox_air))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 20;
                                }
                            }
                        }
                        #endregion

                        for (int i = 0; i < item.Tirs.Length; i++)
                        {
                            if (item.Tirs[i] != null)
                            {
                                if (item.Tirs[i].position.X > 800 || item.Tirs[i].position.X < -Ressources.Tir.Width)
                                    item.Tirs[i] = null;
                                else
                                    item.Tirs[i].Update();

                                if (player1.isTouched(item.Tirs[i]))
                                {
                                    player1.life -= 5;
                                    item.Tirs[i] = null;
                                }

                                if (player2.isTouched(item.Tirs[i]))
                                {
                                    player2.life -= 5;
                                    item.Tirs[i] = null;
                                }

                            }
                        }
                    }
                    #endregion

                    map.Update(gameTime);

                    #region/*Smoke*/
                    for (int j = 0; j < player1.Smoke.Length; j++)
                    {
                        if (player1.Smoke[j] != null)
                        {
                            if (player1.Smoke[j].Alpha < 0)
                            {
                                player1.Smoke[j] = null;

                            }
                            else
                                player1.Smoke[j].Update();
                        }

                    }
                    #endregion

                    if (player1.dead)
                    {
                        CurrentGameState = GameState.GameOver;
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                    }

                    

                    break;
                #endregion
            }
            #endregion



        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Menu/Main_menu/MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnLoad.Draw(spriteBatch);
                    btnMultiplayer.Draw(spriteBatch);
                    btnOption.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    btnEditor.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    map.Draw(spriteBatch, screenWidth, screenHeight);
                    #region/*Player*/
                    player1.Draw(spriteBatch);

                    foreach (Projectile tir in player1.Tirs)
                    {
                        if (tir != null)
                            tir.Draw(spriteBatch);
                    }
                    #endregion
                    #region/*Smoke*/
                    foreach (Smoke smoke in player1.Smoke)
                    {
                        if (smoke != null && smoke.Alpha >= 0)
                            smoke.Draw(spriteBatch);
                    }
                    #endregion
                    #region /*Mobs*/
                    foreach (Mob item in map.mobs)
                    {
                            item.Draw(spriteBatch);
                            foreach (Projectile tir in item.Tirs)
                            {
                                if (tir != null)
                                    tir.Draw(spriteBatch);
                            }
                    }
                    #endregion
                    break;
                case GameState.Option:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Menu/Option/Option_background"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBack.Draw(spriteBatch);
                    btnRes.Draw(spriteBatch);
                    btnLanguage.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/paused/foreground_paused"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBackToMenu.Draw(spriteBatch);
                    btnResume.Draw(spriteBatch);
                    btnSave.Draw(spriteBatch);
                    break;
                case GameState.Paused_editor:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/paused/foreground_paused"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBackToMenu_editor.Draw(spriteBatch);
                    btnResume_editor.Draw(spriteBatch);
                    btnSave_editor.Draw(spriteBatch);
                    btnLoad_editor.Draw(spriteBatch);





                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Game_over/gameover"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBackFromGameOver.Draw(spriteBatch);
                    break;
                case GameState.Editor:
                    editeur1.draw(spriteBatch);
                    break;
                default:
                    map.Draw(spriteBatch, screenWidth, screenHeight);
                    #region/*Player2*/
                    player2.Draw(spriteBatch);
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/hud_multiplayer/" + lang + "/player_2"), new Rectangle((int)(player2.position.X) + 10, (int)(player2.position.Y) - 25, 40, 22), Color.White);
                    foreach (Projectile tir in player2.Tirs)
                    {
                        if (tir != null)
                            tir.Draw(spriteBatch);
                    }
                    #endregion
                    #region/*Smoke2*/
                    foreach (Smoke smoke in player2.Smoke)
                    {
                        if (smoke != null && smoke.Alpha >= 0)
                            smoke.Draw(spriteBatch);
                    }
                    #endregion
                    #region/*Player*/
                    player1.Draw(spriteBatch);
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/hud_multiplayer/" + lang + "/player_1"), new Rectangle((int)(player1.position.X) + 10, (int)(player1.position.Y) - 25, 40, 22), Color.White);
                    foreach (Projectile tir in player1.Tirs)
                    {
                        if (tir != null)
                            tir.Draw(spriteBatch);
                    }
                    #endregion
                    #region/*Smoke*/
                    foreach (Smoke smoke in player1.Smoke)
                    {
                        if (smoke != null && smoke.Alpha >= 0)
                            smoke.Draw(spriteBatch);
                    }
                    #endregion
                    #region /*Mobs*/

                    foreach (Mob item in map.mobs)
                    {
                       
                            item.Draw(spriteBatch);
                            foreach (Projectile tir in item.Tirs)
                            {
                                if (tir != null)
                                    tir.Draw(spriteBatch);
                            }
                    }
                    #endregion
                    break;
            }

            spriteBatch.End();
        }

    }
}
