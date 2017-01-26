using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace ShootInSpace
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const string CurrentVersion = "1.0.1";

        Player player;

        public static Viewport fenetre;

        public static bool DebugMode = false;
        public static SpriteFont debugSpriteFont;
        SpriteFont score;

        public static List<Meteor> meteors = new List<Meteor>();
        Random random = new Random();

        BackGround backGround1;
        BackGround backGround2;

        ButtonState SourisLastState;
        KeyboardState LastKeyState = new KeyboardState();

        Song musicGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Settings.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Settings.SCREEN_HEIGHT;
            graphics.IsFullScreen = Settings.IS_FULL_SCREEN;
            IsMouseVisible = Settings.IS_MOUSE_ACTIVE;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fenetre = graphics.GraphicsDevice.Viewport;
            MenuBase.Initialize();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuBase.LoadContent(this);
            // TODO: use this.Content to load your game content here
            debugSpriteFont = Content.Load<SpriteFont>("DebugSpriteFont");
            score = Content.Load<SpriteFont>("Score");

            player = new Player(Content.Load<Texture2D>("goodFighter.png"), new Vector2(fenetre.Bounds.Width / 2, fenetre.Bounds.Bottom), this);

            backGround1 = new BackGround();
            backGround2 = new BackGround();

            backGround1.Sprite = Content.Load<Texture2D>("bg_verti.png");
            backGround1.BoxCollider = new Rectangle(0, 0, fenetre.Width, fenetre.Height + backGround1.Speed);
            backGround2.Sprite = Content.Load<Texture2D>("bg_verti.png");
            backGround2.BoxCollider = new Rectangle(0, -backGround1.BoxCollider.Height, fenetre.Width, fenetre.Height + backGround2.Speed);

            musicGame = Content.Load<Song>("TechnoMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(musicGame);

            SoundsBank.LoadSounds(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.PageDown))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.T) && !LastKeyState.IsKeyDown(Keys.T))
            {
                DebugMode = !DebugMode;
            }

            // TODO: Add your update logic here

            #region BackGround
            if (backGround1.BoxCollider.Y < fenetre.Height)
            {
                backGround1.BoxCollider.Y += backGround1.Speed;
            }
            else
            {
                backGround1.BoxCollider.Y = -fenetre.Height;
            }

            if (backGround2.BoxCollider.Y < fenetre.Height)
            {
                backGround2.BoxCollider.Y += backGround2.Speed;
            }
            else
            {
                backGround2.BoxCollider.Y = -fenetre.Height;
            }
            #endregion


            switch (MenuBase.MenuState)
            {
                case MenuBase.etats.MenuPrincipal:
                    break;
                case MenuBase.etats.MenuInGame:
                    break;
                case MenuBase.etats.MenuPlay:
                    break;
                case MenuBase.etats.MenuOption:
                    break;
                case MenuBase.etats.InGame:
                    if (random.Next(0, 100) == 30)
                    {
                        SpawnMeteor();
                    }

                    player.Update(gameTime, this);
                    #region Controle Meteor
                    foreach (Meteor meteor in meteors)
                    {
                        meteor.Update(gameTime);
                    }
                    for (int i = 0; i < meteors.Count; i++)
                    {
                        if (meteors[i].NeedToDelete)
                        {
                            meteors.RemoveAt(i);
                            i--;
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }

            MenuBase.Update(this, SourisLastState);
            SourisLastState = Mouse.GetState().LeftButton;
            LastKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(backGround1.Sprite, backGround1.BoxCollider, Color.White);
            spriteBatch.Draw(backGround2.Sprite, backGround2.BoxCollider, Color.White);

            switch (MenuBase.MenuState) //États du jeu en cours
            {
                case MenuBase.etats.MenuPrincipal:
                    spriteBatch.DrawString(debugSpriteFont, "Appuyer sur T pour activer le mode debug", new Vector2(10,10), Color.Gray);
                    spriteBatch.DrawString(debugSpriteFont, "Appuyer sur Escape pour ouvrir le menu", new Vector2(10, 30), Color.Gray);
                    break;
                case MenuBase.etats.MenuInGame:
                    foreach (Meteor meteor in meteors)
                    {
                        meteor.Draw(spriteBatch);
                    }
                    player.Draw(spriteBatch);
                    spriteBatch.DrawString(score, player.Score.ToString(), new Vector2(10, 10), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1);
                    if (DebugMode) spriteBatch.DrawString(debugSpriteFont, "x : " + player.BoxCollider.X.ToString(), new Vector2(10, 300), Color.White);
                    break;
                case MenuBase.etats.MenuPlay:
                    break;
                case MenuBase.etats.MenuOption:
                    break;
                case MenuBase.etats.InGame:
                    foreach (Meteor meteor in meteors)
                    {
                        meteor.Draw(spriteBatch);
                    }
                    player.Draw(spriteBatch);
                    spriteBatch.DrawString(score, player.Score.ToString(), new Vector2(10, 10), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1);
                    if (DebugMode) spriteBatch.DrawString(debugSpriteFont, "x : " + player.BoxCollider.X.ToString(), new Vector2(10, 300), Color.White); 
                    break;
                default:
                    break;
            }
            MenuBase.Draw(spriteBatch);
            spriteBatch.DrawString(debugSpriteFont, "DebugMode : " + DebugMode.ToString(), new Vector2(0, fenetre.Height - debugSpriteFont.MeasureString("DebugMode : ").Y), Color.White);
            spriteBatch.DrawString(debugSpriteFont, "Current Version : " + CurrentVersion, new Vector2(fenetre.Width - debugSpriteFont.MeasureString("Current Version : " + CurrentVersion).X, fenetre.Height - debugSpriteFont.MeasureString("Current Version : " + CurrentVersion).Y), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SpawnMeteor()
        {
            meteors.Add(new Meteor(Content.Load<Texture2D>("Meteor.png"), random.Next(1, 5), new Vector2(random.Next(1, fenetre.Width - 40), -10)));
        }
       
    }
}
