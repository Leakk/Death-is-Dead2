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
            MapSelection,
            Option,
            Playing,
            Playing_2P,
            Paused,
            Paused_editor,
            GameOver,
            Win,
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
        public bool multiplayer = false;
        public bool level2_enable = false;
        public bool level3_enable = false;
        private bool game_over_song_activate = false;
        private bool Win_song_activate = false;

        #region  /*Pour les boutons*/
        bool playOnce = false;
        int count = 0;
        int count_save = 0;
        #endregion

        #region/*boutton editeur*/
        editeur editeur1 = new editeur(2);
        #endregion


        language Currentlanguage;
        GameState CurrentGameState = GameState.MainMenu;

        // Screen Adjustments 
        int screenWidth = 800, screenHeight = 600;

        private Player player1;
        private Map map = new Map(new Obstacle[0], new Mob[0], Ressources.Fond, new Vector2(0, 0));
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
        // cButton btnApply_change;
        // cButton btnSound_volume;
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
        /* Map_selection */
        cButton btnMap1;
        cButton btnMap2;
        cButton btnMap3;
        cButton btnMyMaps;
        #endregion

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Death is Dead";
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
            btnLoad.setPosition(new Vector2(500, 180));
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
            #region /* Menu selection de map */
            btnMap1 = new cButton(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/Level 1"), colour1, graphics.GraphicsDevice);
            btnMap2 = new cButton(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/Level 2"), colour1, graphics.GraphicsDevice);
            btnMap3 = new cButton(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/Level 3"), colour1, graphics.GraphicsDevice);
            btnMyMaps = new cButton(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/MyMaps"), colour1, graphics.GraphicsDevice);

            btnMap1.setPosition(new Vector2(20, 100));
            btnMap2.setPosition(new Vector2(20, 200));
            btnMap3.setPosition(new Vector2(20, 300));
            btnMyMaps.setPosition(new Vector2(300, 100));
            #endregion
            #region/*Pour le menu pause*/
            btnBackToMenu = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/back"), colour1, graphics.GraphicsDevice);
            btnResume = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/resume"), colour1, graphics.GraphicsDevice);
            btnSave = new cButton(Content.Load<Texture2D>("sprite/paused/" + lang + "/save"), colour1, graphics.GraphicsDevice);
            btnBackToMenu.setPosition(new Vector2(300, 400));
            btnSave.setPosition(new Vector2(300, 300));
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
            level2_enable = true;
            level3_enable = true;
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
                        multiplayer = false;
                        button_click.Play();

                        CurrentGameState = GameState.MapSelection;
                    }
                    if (btnMultiplayer.isClicked)
                    {
                        multiplayer = true;
                        button_click.Play();

                        CurrentGameState = GameState.MapSelection;
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

                    if ((btnLoad.isClicked) && (count == 0))
                    {
                        try
                        {
                            count = 15;
                            button_click.Play();
                            IFormatter format = new BinaryFormatter();
                            Stream liste = new FileStream("Sauvegarde/p1.save", FileMode.Open, FileAccess.Read);
                            Stream liste2 = new FileStream("Sauvegarde/p2.save", FileMode.Open, FileAccess.Read);
                            Stream liste3 = new FileStream("Sauvegarde/map.save", FileMode.Open, FileAccess.Read);
                            Stream liste4 = new FileStream("Sauvegarde/mobs.save", FileMode.Open, FileAccess.Read);
                            Stream liste5 = new FileStream("Sauvegarde/flag.save", FileMode.Open, FileAccess.Read);
                            Player P1;
                            Player2 P2;
                            Obstacle[] obs2;
                            Mob[] mobs2;
                            P1 = (Player)format.Deserialize(liste);
                            P2 = (Player2)format.Deserialize(liste2);
                            obs2 = (Obstacle[])format.Deserialize(liste3);
                            mobs2 = (Mob[])format.Deserialize(liste4);
                            player1 = new Player(P1.position, Ressources.Player, P1.life, P1.CurrentWeaponIsFaux);
                            player2 = new Player2(P2.position, Ressources.Player2, P2.life, P2.CurrentWeaponIsFaux);
                            foreach (Obstacle item in obs2)
                            {
                                item.maj(Content);
                            }
                            map.obs = obs2;
                            for (int a = 0; a < mobs2.Length; a++)
                            {
                                mobs2[a].maj(Content);
                                    mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life, mobs2[a].type);
                            }

                            Vector2 fl = (Vector2)format.Deserialize(liste5);
                            map.flag = new end_flag((int)fl.X, (int)fl.Y);
                            if (P2.position.X < 0)
                                CurrentGameState = GameState.Playing;
                            else
                                CurrentGameState = GameState.Playing_2P;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Game_song_lvl1);
                        }
                        catch { }

                    }

                    if ((btnEditor.isClicked) && (count == 0))
                    {
                        count = 15;
                        button_click.Play();
                        IFormatter format = new BinaryFormatter();
                        Stream liste2 = new FileStream("Content/lvl1/fond", FileMode.Open, FileAccess.Read);
                        uint[] fon = (uint[])format.Deserialize(liste2);
                        Texture2D tmp = Ressources.Fond;
                        tmp.SetData<uint>(fon);

                        if (Currentlanguage == language.french)
                            editeur1 = new editeur(1);

                        if (Currentlanguage == language.english)
                            editeur1 = new editeur(2);

                        if (Currentlanguage == language.spanish)
                            editeur1 = new editeur(3);
                        editeur1.load(graphics);
                        editeur1.j = 30;
                        CurrentGameState = GameState.Editor;


                    }
                    if (count != 0)
                        count--;
                    break;
                #endregion
                #region /* map selection */
                case GameState.MapSelection:
                    btnBack.Udapte(mouse);
                    btnMap1.Udapte(mouse);
                    btnMap2.Udapte(mouse);
                    btnMap3.Udapte(mouse);
                    btnMyMaps.Udapte(mouse);

                    #region /* chargements des truc commun à tout les bouttons */
                    if (count == 0 && (btnMap1.isClicked) || (btnMap2.isClicked)&& level2_enable
                        || (btnMap3.isClicked)&&level3_enable || (btnMyMaps.isClicked)) /* n'importe quel boutton qui charge une map lira ça */
                    {
                        if (multiplayer)
                        {
                            player2 = new Player2(new Vector2(350, 0), Ressources.Player2, 100, false);
                            CurrentGameState = GameState.Playing_2P;
                        }
                        else
                        {
                            player2 = new Player2(new Vector2(-1000, 0), Ressources.Player2, 100, false);
                            CurrentGameState = GameState.Playing;
                        }

                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100, false);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        songisplayed = false;


                        button_click.Play();
                    }
                    #endregion


                    if (btnMap1.isClicked && count == 0)
                    {
                        IFormatter format = new BinaryFormatter();

                        Stream liste2 = new FileStream("Content/lvl1/fond", FileMode.Open, FileAccess.Read);
                        Stream liste3 = new FileStream("Content/lvl1/map", FileMode.Open, FileAccess.Read);
                        Stream liste4 = new FileStream("Content/lvl1/mobs", FileMode.Open, FileAccess.Read);
                        Stream liste5 = new FileStream("Content/lvl1/flag", FileMode.Open, FileAccess.Read);
                        Obstacle[] obs2;
                        Mob[] mobs2;
                        uint[] fon = (uint[])format.Deserialize(liste2);
                        Vector2 fl = (Vector2)format.Deserialize(liste5);
                        obs2 = (Obstacle[])format.Deserialize(liste3);
                        mobs2 = (Mob[])format.Deserialize(liste4);
                        foreach (Obstacle item in obs2)
                        {
                            item.maj(Content);
                        }
                        for (int a = 0; a < mobs2.Length; a++)
                        {
                            mobs2[a].maj(Content);
                                mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life, mobs2[a].type);
                        }

                        Texture2D tmp = Ressources.Fond; 
                        tmp.SetData<uint>(fon);

                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        map =new Map(obs2,mobs2,tmp,fl);
                        count = 10;
                        button_click.Play();
                    }

                    if (btnMap2.isClicked && count == 0 && level2_enable)
                    {
                        IFormatter format = new BinaryFormatter();

                        Stream liste2 = new FileStream("Content/lvl2/fond", FileMode.Open, FileAccess.Read);
                        Stream liste3 = new FileStream("Content/lvl2/map", FileMode.Open, FileAccess.Read);
                        Stream liste4 = new FileStream("Content/lvl2/mobs", FileMode.Open, FileAccess.Read);
                        Stream liste5 = new FileStream("Content/lvl2/flag", FileMode.Open, FileAccess.Read);
                        Obstacle[] obs2;
                        Mob[] mobs2;
                        uint[] fon = (uint[])format.Deserialize(liste2);
                        Vector2 fl = (Vector2)format.Deserialize(liste5);
                        obs2 = (Obstacle[])format.Deserialize(liste3);
                        mobs2 = (Mob[])format.Deserialize(liste4);
                        foreach (Obstacle item in obs2)
                        {
                            item.maj(Content);
                        }
                        for (int a = 0; a < mobs2.Length; a++)
                        {
                            mobs2[a].maj(Content);
                            mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life, mobs2[a].type);
                        }

                        Texture2D tmp = Ressources.Fond;
                        tmp.SetData<uint>(fon);

                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        map = new Map(obs2, mobs2, tmp, fl);
                        count = 10;
                        button_click.Play();
                    }

                    if (btnMap3.isClicked && count == 0 && level3_enable)
                    {
                        IFormatter format = new BinaryFormatter();

                        Stream liste2 = new FileStream("Content/lvl3/fond", FileMode.Open, FileAccess.Read);
                        Stream liste3 = new FileStream("Content/lvl3/map", FileMode.Open, FileAccess.Read);
                        Stream liste4 = new FileStream("Content/lvl3/mobs", FileMode.Open, FileAccess.Read);
                        Stream liste5 = new FileStream("Content/lvl3/flag", FileMode.Open, FileAccess.Read);
                        Obstacle[] obs2;
                        Mob[] mobs2;
                        uint[] fon = (uint[])format.Deserialize(liste2);
                        Vector2 fl = (Vector2)format.Deserialize(liste5);
                        obs2 = (Obstacle[])format.Deserialize(liste3);
                        mobs2 = (Mob[])format.Deserialize(liste4);
                        foreach (Obstacle item in obs2)
                        {
                            item.maj(Content);
                        }
                        for (int a = 0; a < mobs2.Length; a++)
                        {
                            mobs2[a].maj(Content);
                                mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life, mobs2[a].type);
                        }
                        Texture2D tmp = Ressources.Fond;
                        tmp.SetData<uint>(fon);

                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        map = new Map(obs2, mobs2, tmp, fl);
                        count = 10;
                        button_click.Play();
                    }

                    if (btnMyMaps.isClicked && count == 0)
                    {
                        IFormatter format = new BinaryFormatter();

                        Stream liste2 = new FileStream("Content/Editeur/fond", FileMode.Open, FileAccess.Read);
                        Stream liste3 = new FileStream("Content/Editeur/map", FileMode.Open, FileAccess.Read);
                        Stream liste4 = new FileStream("Content/Editeur/mob", FileMode.Open, FileAccess.Read);
                        Stream liste5 = new FileStream("Content/Editeur/flag", FileMode.Open, FileAccess.Read);
                        Obstacle[] obs2;
                        Mob[] mobs2;
                        uint[] fon = (uint[])format.Deserialize(liste2);
                        Vector2 fl = (Vector2)format.Deserialize(liste5);
                        obs2 = (Obstacle[])format.Deserialize(liste3);
                        mobs2 = (Mob[])format.Deserialize(liste4);
                        foreach (Obstacle item in obs2)
                        {
                            item.maj(Content);
                        }
                        for (int a = 0; a < mobs2.Length; a++)
                        {
                            mobs2[a].maj(Content);
                                mobs2[a] = new Mob(mobs2[a].position, mobs2[a].texture, mobs2[a].life, mobs2[a].type);
                        }
                        Texture2D tmp = Ressources.Fond;
                        tmp.SetData<uint>(fon);

                        MediaPlayer.Stop();
                        MediaPlayer.Play(Game_song_lvl1);
                        map = new Map(obs2, mobs2, tmp, fl);
                        count = 10;

                        button_click.Play();
                    }

                    if (btnBack.isClicked && count == 0)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.MainMenu;
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
                    if ((btnResume.isClicked) && (count_save == 0))
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
                        Stream liste = new FileStream("Content/Sauvegarde/p1", FileMode.Create, FileAccess.Write);
                        Stream liste2 = new FileStream("Content/Sauvegarde/p2", FileMode.Create, FileAccess.Write);
                        Stream liste3 = new FileStream("Content/Sauvegarde/map", FileMode.Create, FileAccess.Write);
                        Stream liste4 = new FileStream("Content/Sauvegarde/mobs", FileMode.Create, FileAccess.Write);
                        Stream liste5 = new FileStream("Content/Sauvegarde/flag", FileMode.Create, FileAccess.Write);
                        format.Serialize(liste, player1);
                        format.Serialize(liste2, player2);
                        format.Serialize(liste3, map.obs);
                        format.Serialize(liste4, map.mobs);
                        format.Serialize(liste5, new Vector2(map.flag.x, map.flag.y));
                        liste.Close();
                        liste2.Close();
                        liste3.Close();
                        liste4.Close();
                        liste5.Close();
                        CurrentGameState = GameState.MainMenu;
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
                        CurrentGameState = GameState.MainMenu;

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
                    if (!game_over_song_activate)
                    {
                        MediaPlayer.Play(Ressources.Game_over_song);
                        game_over_song_activate = true;
                    }
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
                #region/*Win*/
                case GameState.Win:
                    btnBackToMenu.Udapte(mouse);
                    if (!Win_song_activate)
                    {
                        MediaPlayer.Play(Ressources.Victory);
                        Win_song_activate = true;
                    }
                    if (btnBackToMenu.isClicked)
                    {
                        button_click.Play();
                        CurrentGameState = GameState.MainMenu;
                        if (level2_enable == true)
                        {
                            level3_enable = true;
                        }
                        else
                        {
                            level2_enable = true;
                        }

                    }
                    break;

                #endregion
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
                                        Ressources.impact_tir_enemi.Play();
                                        mob.life -= 20;
                                        player2.Tirs[i] = null;
                                    }
                                }
                                if (player2.Tirs[i] != null)
                                    if (new Collision(new Rectangle((int)player2.Tirs[i].position.X, (int)player2.Tirs[i].position.Y, player2.Tirs[i].texture.Width, player2.Tirs[i].texture.Height)).is_coll(map.obs))
                                    {
                                        player2.Tirs[i] = null;
                                        Ressources.impact_tir_mur.Play();
                                    }
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
                            player1 = new Player(new Vector2(350, 0), Ressources.Player, 100, false);
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
                                    Ressources.impact_tir_enemi.Play();
                                    mob.life -= 20;
                                    player1.Tirs[i] = null;
                                }
                            }
                            if (player1.Tirs[i] != null)
                                if (new Collision(new Rectangle((int)player1.Tirs[i].position.X, (int)player1.Tirs[i].position.Y, player1.Tirs[i].texture.Width, player1.Tirs[i].texture.Height)).is_coll(map.obs))
                                {
                                    player1.Tirs[i] = null;
                                    Ressources.impact_tir_mur.Play();
                                }

                        }
                    }
                    #endregion

                    #region /*Update Mobs*/
                    foreach (Mob item in map.mobs)
                    {
                        item.Update(map.obs, player1, player2);
                        #region /* faux */
                        if (player1.CurrentWeaponIsFaux)
                        {
                            if (player1.Faux_damageBox_ground_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player1.Faux_damageBox_ground))
                                 || (item.HitboxH.Rectangle.Intersects(player1.Faux_damageBox_ground)
                                 || (item.HitboxG.Rectangle.Intersects(player1.Faux_damageBox_ground)
                                 || (item.HitboxD.Rectangle.Intersects(player1.Faux_damageBox_ground))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 14;
                                    Ressources.Faux_impact_on_ennemy.Play();
                                }
                            }

                            if (player1.Faux_damageBox_air_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player1.Faux_damageBox_air))
                                 || (item.HitboxH.Rectangle.Intersects(player1.Faux_damageBox_air)
                                 || (item.HitboxG.Rectangle.Intersects(player1.Faux_damageBox_air)
                                 || (item.HitboxD.Rectangle.Intersects(player1.Faux_damageBox_air))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 10;
                                    Ressources.Faux_impact_on_ennemy.Play();
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
                                    item.life -= 14;
                                    Ressources.Faux_impact_on_ennemy.Play();
                                }
                            }

                            if (player2.Faux_damageBox_air_isActivate)
                            {
                                if ((item.HitboxB.Rectangle.Intersects(player2.Faux_damageBox_air))
                                 || (item.HitboxH.Rectangle.Intersects(player2.Faux_damageBox_air)
                                 || (item.HitboxG.Rectangle.Intersects(player2.Faux_damageBox_air)
                                 || (item.HitboxD.Rectangle.Intersects(player2.Faux_damageBox_air))))) /* la hit box de la faux touche l'enemi */
                                {
                                    item.life -= 10;
                                    Ressources.Faux_impact_on_ennemy.Play();
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
                                    Ressources.impact_tir_player.Play();
                                    item.Tirs[i] = null;
                                }

                                if (player2.isTouched(item.Tirs[i]))
                                {
                                    player2.life -= 5;
                                    Ressources.impact_tir_player.Play();
                                    item.Tirs[i] = null;
                                }

                                if (item.Tirs[i] != null)
                                    if (new Collision(new Rectangle((int)item.Tirs[i].position.X, (int)item.Tirs[i].position.Y, item.Tirs[i].texture.Width, item.Tirs[i].texture.Height)).is_coll(map.obs))
                                    {
                                        item.Tirs[i] = null;
                                        Ressources.impact_tir_mur.Play();
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
                        player1 = new Player(new Vector2(350, 0), Ressources.Player, 100, false);
                    }

                    map.flag.Udapte(player1, player2);
                    if (map.flag.Win)
                    {
                        CurrentGameState = GameState.Win;

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
                case GameState.MapSelection:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Map_selection/fond_mapSelect"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    if (multiplayer)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/info2"), new Rectangle(200, 0, 250, 80), Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("sprite/Map_selection/" + lang + "/info"), new Rectangle(200, 0, 250, 80), Color.Red);
                    }
                    btnMap1.Draw(spriteBatch);
                    if (level2_enable)
                    {
                        btnMap2.Draw(spriteBatch);
                    }
                    else
                    {
                        btnMap2.Draw2(spriteBatch, Color.Gray);
                    }
                    if (level3_enable)
                    {
                        btnMap3.Draw(spriteBatch);
                    }
                    else
                    {
                        btnMap3.Draw2(spriteBatch, Color.Gray);
                    }
                    

                    btnMyMaps.Draw(spriteBatch);
                    btnBack.Draw(spriteBatch);
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
                case GameState.Win:
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/paused/foreground_paused"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>("sprite/Win_menu/" + lang + "/Win_message"), new Rectangle(200, 200, 400, 100), Color.White);
                    btnBackToMenu.Draw(spriteBatch);
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
