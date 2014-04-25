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
        public KeyboardState keyboardState;
        #region  /*pour les buttons*/
        bool playOnce = false;
        int countt = 0;
        #endregion
        private bool paused =false;
        private bool pausedForGuide = false;
       

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

        #region    /* pour mettre à pause */
        private void BeginPause(bool UserInitiated) 
        {
            paused = true;
            pausedForGuide = !UserInitiated;
            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }
        private void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            pausedForGuide = false;
            paused = false;
        }
#endregion


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
                btnPlay = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_play"), graphics.GraphicsDevice);
                btnMultiplayer = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_play_mult"), graphics.GraphicsDevice);
                btnOption = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/"+lang+"/Button_option"), graphics.GraphicsDevice);
                btnExit = new cButton(Content.Load<Texture2D>("sprite/Menu/Main_menu/" + lang + "/Exit"), graphics.GraphicsDevice);
                btnPlay.setPosition(new Vector2(500, 100));
                btnMultiplayer.setPosition(new Vector2(500, 200));
                btnOption.setPosition(new Vector2(500, 300));
                btnExit.setPosition(new Vector2(500, 400));
                /*Option*/
                btnBack = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/back"), graphics.GraphicsDevice);
                btnRes = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/fullscreen"), graphics.GraphicsDevice);
                btnLanguage = new cButton(Content.Load<Texture2D>("sprite/Menu/Option/"+lang+"/language"), graphics.GraphicsDevice);
                btnBack.setPosition(new Vector2(40, 500));
                btnRes.setPosition(new Vector2(400, 500));
                btnLanguage.setPosition(new Vector2(40, 100));

                button_click = (Content.Load<SoundEffect>("Sound_effects/Menu/button_sound_click"));    
                         
                
            
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
            MouseState mouse = Mouse.GetState();

            #region /*les gameState*/
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    btnPlay.Udapte(mouse);
                    btnMultiplayer.Udapte(mouse);
                    btnOption.Udapte(mouse);
                    btnExit.Udapte(mouse);
                    if (btnPlay.isClicked) button_click.Play();
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
                  

                    if (keyboardState.IsKeyDown(Keys.Escape))
                    {
                        if (CurrentGameState == GameState.paused)
                        {
                            CurrentGameState = GameState.paused;
                        }
                        else
                        {
                            CurrentGameState = GameState.Playing;
                        }
                    }
              
                    break;

                case GameState.paused:
                    BeginPause(true);
                    
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

            #endregion

            }
           
            base.Update(gameTime);
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
                            
                  
                    break;

            }
            spriteBatch.End();
            #endregion

           

            if (btnPlay.isClicked)
            {
               
                CurrentGameState = GameState.Playing;
                main.Update(gameTime);
                spriteBatch.Begin();
                main.Draw(spriteBatch);
                spriteBatch.End();
                base.Draw(gameTime);
              

            }
        }
    }
}
