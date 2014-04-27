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
            Paused
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
        private Color colour1 = new Color(255, 0, 0, 255);
        // private Color btnColorPause = new Color(255, 0, 0, 255);
        public KeyboardState keyboardState;

        #region  /*Pour les boutons*/
        bool playOnce = false;
        int count = 0;
        #endregion

        private bool paused = false;

        language Currentlanguage;
        GameState CurrentGameState = GameState.MainMenu;

        // Screen Adjustments 
        int screenWidth = 800, screenHeight = 600;

        private Player player;
        private static Obstacle[] rect;
        private static Mob[] mobs;
        private Map map;

        /* attention si on change la résolution in faudra peut etre modifier dans la classe cButton le public cButton  :
         * dans le size  le "graphics.Viewport.Width / 8, graphics.Viewport.Height / 30" la valeur des 2 divisions est a changer */

        #region /*Boutons*/
        /*main_menu*/
        cButton btnPlay;
        cButton btnOption;
        cButton btnMultiplayer;
        cButton btnExit;
        /*Option*/
        cButton btnRes;
        cButton btnBack;
        cButton btnApply_change;
        cButton btnSound_volume;
        cButton btnLanguage;
        /*pause*/
        cButton btnBackToMenu;
        cButton btnResume;
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
            string lang = "";

            if (Currentlanguage == language.english) lang = "eng";
            if (Currentlanguage == language.french) lang = "fre";
            if (Currentlanguage == language.spanish) lang = "spa";

            /* MainMenu*/
            btnPlay = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_play"), colour1, graphics.GraphicsDevice);
            btnMultiplayer = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_play_mult"), colour1, graphics.GraphicsDevice);
            btnOption = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Button_option"), colour1, graphics.GraphicsDevice);
            btnExit = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Exit"), colour1, graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(500, 100));
            btnMultiplayer.setPosition(new Vector2(500, 200));
            btnOption.setPosition(new Vector2(500, 300));
            btnExit.setPosition(new Vector2(500, 400));

            /*Option*/
            btnBack = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnRes = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/fullscreen"), colour1, graphics.GraphicsDevice);
            btnLanguage = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/" + lang + "/language"), colour1, graphics.GraphicsDevice);
            btnBack.setPosition(new Vector2(40, 500));
            btnRes.setPosition(new Vector2(400, 500));
            btnLanguage.setPosition(new Vector2(40, 100));

            button_click = (Content.Load<SoundEffect>("Sound_effects/Menu/button_sound_click"));
            #endregion

            #region/*Pour le menu pause*/
            btnBackToMenu = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnResume = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/resume"), colour1, graphics.GraphicsDevice);
            btnBackToMenu.setPosition(new Vector2(300, 300));
            btnResume.setPosition(new Vector2(300, 200));
            #endregion

            Ressources.Load(Content);


            rect = new Obstacle[21] { new Obstacle(new Rectangle(0, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(300, 360, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(1040, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(1800, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2100, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2540, 380, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(2840, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(3450, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme/*carre*/), new Obstacle(new Rectangle(3850, 450, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4450, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(4850, 350, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(5350, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6600, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(6850, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7250, 300, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(7650, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(7700, 250, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8150, 150, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(8550, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol), new Obstacle(new Rectangle(9550, 400, Ressources.plateforme.Width, Ressources.plateforme.Height), Ressources.plateforme), new Obstacle(new Rectangle(9200, 550, Ressources.sol.Width, Ressources.sol.Height), Ressources.sol) };
            mobs = new Mob[1] { new Mob(new Vector2(350, 0), Ressources.Player, 20) };
            map = new Map(rect, mobs);
            player = new Player(new Vector2(350, 0), Ressources.Player, 100);
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
                    btnPlay.Udapte(mouse);
                    btnMultiplayer.Udapte(mouse);
                    btnOption.Udapte(mouse);
                    btnExit.Udapte(mouse);
                    if (btnPlay.isClicked)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.Playing;
                    }
                    if (btnMultiplayer.isClicked)
                        button_click.Play();
                    if (btnOption.isClicked)
                        button_click.Play();
                    if (btnPlay.isClicked == true)
                        CurrentGameState = GameState.Playing;
                    if (btnOption.isClicked)
                        CurrentGameState = GameState.Option;
                    if (btnExit.isClicked)
                    {
                        button_click.Play();
                        Exit();
                    }
                    break;
                case GameState.Playing:
                    break;
                case GameState.Paused:
                    btnResume.Udapte(mouse);
                    btnBackToMenu.Udapte(mouse);
                    if (btnResume.isClicked)
                        CurrentGameState = GameState.Playing;
                    if (btnBackToMenu.isClicked)
                        CurrentGameState = GameState.MainMenu;
                    break;
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
                    if ((btnRes.isClicked) & (count == 0))
                    {
                        count = 15;
                        button_click.Play();
                        if (graphics.IsFullScreen == false)
                            graphics.IsFullScreen = true;
                        else graphics.IsFullScreen = false;
                        graphics.ApplyChanges();

                    }
                    if ((btnLanguage.isClicked) & (count == 0))
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
            }
            #endregion

            #region /*Update Player*/
            player.update1(rect);
            player.Update(Keyboard.GetState(), rect);
            for (int i = 0; i < player.Tirs.Length; i++)
            {
                if (player.Tirs[i] != null)
                {
                    if (player.Tirs[i].position.X > 800 || player.Tirs[i].position.X < -Ressources.Tir.Width)
                        player.Tirs[i] = null;
                    else
                        player.Tirs[i].Update();
                }
            }
            #endregion

            map.Update(gameTime);

            #region/*Smoke*/
            for (int j = 0; j < player.Smoke.Length; j++)
            {
                if (player.Smoke[j] != null)
                {
                    if (player.Smoke[j].Alpha < 0)
                    {
                        player.Smoke[j] = null;

                    }
                    else
                        player.Smoke[j].Update();
                }

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
                    btnMultiplayer.Draw(spriteBatch);
                    btnOption.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    map.Draw(spriteBatch);
                    #region/*Player*/
                    player.Draw(spriteBatch);

                    foreach (Projectile tir in player.Tirs)
                    {
                        if (tir != null)
                            tir.Draw(spriteBatch);
                    }
                    #endregion
                    #region/*Smoke*/
                    foreach (Smoke smoke in player.Smoke)
                    {
                        if (smoke != null && smoke.Alpha >= 0)
                            smoke.Draw(spriteBatch);
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
                    break;
            }

            spriteBatch.End();
        }
    }
}
