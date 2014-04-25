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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameMain main;
        SoundEffect button_click;
        private Color colour1 = new Color(255, 0, 0, 255);
       // private Color btnColorPause = new Color(255, 0, 0, 255);
        public KeyboardState keyboardState;
        #region  /*pour les buttons*/
        bool playOnce = false;
        int countt = 0;
        #endregion
        private bool paused =false;


        enum GameState
        {
            MainMenu,
            Option,
            Playing,
            Playing_2P,
            paused

        }

        enum language
        {
            english,
            french,
            spanish,
        }
        language Currentlanguage;
        GameState CurrentGameState = GameState.MainMenu;
        // Screen Adjustments 
        int screenWidth = 800, screenHeight = 600;

        public int ScreenHeight1
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }



        /* attention si on change la résolution in faudra peut etre modifier dans la classe cButton le public cButton  :
         * dans le size  le "graphics.Viewport.Width / 8, graphics.Viewport.Height / 30" la valeur des 2 divisions est a changer */

        #region /*buttons*/
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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Physics Engine";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            
            
            #region /*pour le menu*/  
            string lang = "";
            if (Currentlanguage == language.english) lang = "eng";
            if (Currentlanguage == language.french) lang = "fre";
            if (Currentlanguage == language.spanish) lang = "spa"; 
                 /* MainMenu*/
                btnPlay = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_play"),colour1, graphics.GraphicsDevice);
                btnMultiplayer = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_play_mult"),colour1, graphics.GraphicsDevice);
                btnOption = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_option"),colour1, graphics.GraphicsDevice);
                btnExit = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Exit"),colour1, graphics.GraphicsDevice);
                btnPlay.setPosition(new Vector2(500, 100));
                btnMultiplayer.setPosition(new Vector2(500, 200));
                btnOption.setPosition(new Vector2(500, 300));
                btnExit.setPosition(new Vector2(500, 400));
                /*Option*/
                btnBack = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/back"),colour1, graphics.GraphicsDevice);
                btnRes = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/fullscreen"),colour1, graphics.GraphicsDevice);
                btnLanguage = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/language"),colour1, graphics.GraphicsDevice);
                btnBack.setPosition(new Vector2(40, 500));
                btnRes.setPosition(new Vector2(400, 500));
                btnLanguage.setPosition(new Vector2(40, 100));

                button_click = (Content.Load<SoundEffect>("Sound_effects/Menu/button_sound_click"));    
                         
                
            
            #endregion
            #region/*pour le menu pause*/
                btnBackToMenu = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/back"),colour1, graphics.GraphicsDevice);
                btnResume = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/resume"),colour1, graphics.GraphicsDevice);
                btnBackToMenu.setPosition(new Vector2(300, 300));
                btnResume.setPosition(new Vector2(300, 200));
            #endregion
                Ressources.Load(Content);
            main = new GameMain();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            #region/*pause*/
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
            }

                if ((paused) & (!Keyboard.GetState().IsKeyDown(Keys.Escape)))
                {
                    paused = false;
                    if (CurrentGameState == GameState.Playing)
                    {
                        CurrentGameState = GameState.paused;
                    }
                    else
                    {
                        CurrentGameState = GameState.Playing;
                    }
                }
                // pausedprec = false;
            #endregion

                MouseState mouse = Mouse.GetState();

            #region /*les gameState*/
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
                    if (btnMultiplayer.isClicked) button_click.Play();
                    if (btnOption.isClicked) button_click.Play();
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    if (btnOption.isClicked) CurrentGameState = GameState.Option;
                    if (btnExit.isClicked)
                    {
                        button_click.Play();
                        Exit();
                    }
                    
                    break;
                case GameState.Playing:
                                    
              
                    break;

                case GameState.paused:
                    btnResume.Udapte(mouse);
                    btnBackToMenu.Udapte(mouse);
                    if (btnResume.isClicked) CurrentGameState = GameState.Playing;
                    if (btnBackToMenu.isClicked) CurrentGameState = GameState.MainMenu;
                    break;
                #region/*GameState.Option*/
                case GameState.Option:
                    playOnce = false;
                    btnBack.Udapte(mouse);
                    btnRes.Udapte(mouse);
                    btnLanguage.Udapte(mouse);
                    if (btnBack.isClicked) { button_click.Play(); CurrentGameState = GameState.MainMenu; }
                    if ((btnRes.isClicked) & (countt == 0))
                    {
                        countt = 15;
                        button_click.Play();
                        if (graphics.IsFullScreen == false)
                        graphics.IsFullScreen = true;
                        else graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                        
                    }
                    if ((btnLanguage.isClicked) & (countt == 0))
                    {
                        try
                        {

                            countt = 15;
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

                    if (countt != 0)
                        countt--;
                    break;
                #endregion

                   

            }
              #endregion
           // if (CurrentGameState == GameState.Playing) base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Ressources.Player,new Vector2(500,500),new Color(0,100,100));


            switch (CurrentGameState)
            #region /* gameState */
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Menu/Main_menu/MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                   
                    btnPlay.Draw(spriteBatch);
                    btnMultiplayer.Draw(spriteBatch);
                    btnOption.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                  

                    break;
                case GameState.Playing:
                     
                   
                   
                    
                    break;
                case GameState.Option:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Menu/Option/Option_background"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBack.Draw(spriteBatch);
                    btnRes.Draw(spriteBatch);
                    btnLanguage.Draw(spriteBatch);
                    break;

                case GameState.paused:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/paused/foreground_paused"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnBackToMenu.Draw(spriteBatch);
                    btnResume.Draw(spriteBatch);
                    break;

            }
            spriteBatch.End();
            #endregion

           

       
                if (CurrentGameState == GameState.Playing)
                {
                    main.Update(gameTime);
                    spriteBatch.Begin();
                    main.Draw(spriteBatch);
                    spriteBatch.End();
                    base.Draw(gameTime);
                    
                }
              

            
        }
    }
}
