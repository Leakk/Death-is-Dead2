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
            GameOver
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

        #region  /*Pour les boutons*/
        bool playOnce = false;
        int count = 0;
        int count_save = 0;
        #endregion

        private bool paused = false;

        language Currentlanguage;
        GameState CurrentGameState = GameState.MainMenu;

        // Screen Adjustments 
        int screenWidth = 800, screenHeight = 600;

        private Player player1;
        private static Obstacle[] rect;
        private static Mob[] mobs;
        private Map map;
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
            btnPlay.setPosition(new Vector2(500, 100));
            btnLoad.setPosition(new Vector2(500,200));
            btnMultiplayer.setPosition(new Vector2(500, 300));
            btnOption.setPosition(new Vector2(500, 400));
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
            #region/*Menu Game over*/
            btnBackFromGameOver = new cButton(Content.Load<Texture2D>("sprite/Game_over/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnBackFromGameOver.setPosition(new Vector2(10, 300));
            #endregion
            Ressources.Load(Content);

        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            #region/*Pause*/
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
                CurrentGameState = GameState.Paused;
            }

            if ((paused) & (!Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                paused = false;
                if (paused)
                    CurrentGameState = GameState.Playing;
                else
                    CurrentGameState = GameState.Paused;
            }
            #endregion

            MouseState mouse = Mouse.GetState();

            #region /*Les GameStates*/
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    //  MediaPlayer.Stop();
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
                    if (btnPlay.isClicked)
                    {
                        button_click.Play();
                        rect = new Obstacle[21] { new Obstacle(new Rectangle(0, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(300, 360, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(1040, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(1800, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2100, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2540, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2840, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(3450, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme/*carre*/), new Obstacle(new Rectangle(3850, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4450, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4850, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(5350, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6600, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6850, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7250, 300, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7650, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(7700, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8150, 150, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8550, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(9550, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(9200, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol) };
                        mobs = new Mob[5] { new Mob(new Vector2(1250, 400), Ressources.Player, 100), new Mob(new Vector2(3370, 400), Ressources.Player, 100), new Mob(new Vector2(5870, 400), Ressources.Player, 100), new Mob(new Vector2(7450, 100), Ressources.Player, 100), new Mob(new Vector2(8700, 400), Ressources.Player, 100) };
                        map = new Map(rect, mobs);
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        songisplayed = false;
                        CurrentGameState = GameState.Playing;
                    }
                    if (btnMultiplayer.isClicked)
                    {
                        button_click.Play();
                        rect = new Obstacle[21] { new Obstacle(new Rectangle(0, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(300, 360, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(1040, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(1800, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2100, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2540, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2840, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(3450, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme/*carre*/), new Obstacle(new Rectangle(3850, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4450, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4850, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(5350, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6600, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6850, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7250, 300, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7650, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(7700, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8150, 150, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8550, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(9550, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(9200, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol) };
                        mobs = new Mob[5] { new Mob(new Vector2(1250, 400), Ressources.Player, 100), new Mob(new Vector2(3370, 400), Ressources.Player, 100), new Mob(new Vector2(5870, 400), Ressources.Player, 100), new Mob(new Vector2(7450, 100), Ressources.Player, 100), new Mob(new Vector2(8700, 400), Ressources.Player, 100) };
                        map = new Map(rect, mobs);
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100);
                        player2 = new Player2(new Vector2(350, 0), Ressources.Player, 100);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        songisplayed = false;
                        CurrentGameState = GameState.Playing_2P;
                    }
                    if (btnOption.isClicked)
                        button_click.Play();

                    if (btnOption.isClicked)
                        CurrentGameState = GameState.Option;
                    if (btnExit.isClicked)
                    {
                        button_click.Play();
                        Exit();
                    }

                    if ((btnLoad.isClicked)&&(count==0))
                    {
                        count = 15;
                        button_click.Play();
                        /* action chargement */
                    
                    }
                    if (count != 0)
                        count--;


                    break;
                #region/* case paused*/
                case GameState.Paused:
                    btnResume.Udapte(mouse);
                    btnBackToMenu.Udapte(mouse);
                    btnSave.Udapte(mouse);
                    if (btnResume.isClicked)
                    {
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
                    /* action sauvegarder*/
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
                default:
                    if (CurrentGameState == GameState.Playing_2P)
                    {
                        #region /*Update Player2*/
                        player2.Update(Keyboard.GetState(), rect);
                        for (int i = 0; i < player2.Tirs.Length; i++)
                        {
                            if (player2.Tirs[i] != null)
                            {
                                if (player2.Tirs[i].position.X > 800 || player2.Tirs[i].position.X < -Ressources.Tir.Width)
                                    player2.Tirs[i] = null;
                                else
                                    player2.Tirs[i].Update();
                                foreach (Mob mob in mobs)
                                {
                                    if (mob.isTouched(player2.Tirs[i]))
                                    {
                                        mob.life -= 10;
                                        player2.Tirs[i] = null;
                                    }
                                }
                                if (player2.Tirs[i] != null)
                                    if (new Collision(new Rectangle((int)player2.Tirs[i].position.X, (int)player2.Tirs[i].position.Y, player2.Tirs[i].texture.Width, player2.Tirs[i].texture.Height)).is_coll(rect))
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
                    player1.Update(Keyboard.GetState(), rect);
                    for (int i = 0; i < player1.Tirs.Length; i++)
                    {
                        if (player1.Tirs[i] != null)
                        {
                            if (player1.Tirs[i].position.X > 800 || player1.Tirs[i].position.X < -Ressources.Tir.Width)
                                player1.Tirs[i] = null;
                            else
                                player1.Tirs[i].Update();
                            foreach (Mob mob in mobs)
                            {
                                if (mob.isTouched(player1.Tirs[i]))
                                {
                                    mob.life -= 10;
                                    player1.Tirs[i] = null;
                                }
                            }
                            if (player1.Tirs[i] != null)
                                if (new Collision(new Rectangle((int)player1.Tirs[i].position.X, (int)player1.Tirs[i].position.Y, player1.Tirs[i].texture.Width, player1.Tirs[i].texture.Height)).is_coll(rect))
                                    player1.Tirs[i] = null;

                        }
                    }
                    #endregion

                    #region /*Update Mobs*/
                    foreach (Mob item in mobs)
                    {
                        item.Update(rect, player1);
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
                    foreach (Mob item in mobs)
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
                case GameState.GameOver:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Game_over/gameover"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBackFromGameOver.Draw(spriteBatch);
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

                    foreach (Mob item in mobs)
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
