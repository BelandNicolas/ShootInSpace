using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootInSpace
{
    public class MenuBase
    {
        public const byte NBBUTTON = 3;
        public const int HEIGHTBUTTON =75 ;
        public const int WIDTHBUTTON = 263;

        public static MenuButton playButton;
        public static MenuButton optionButton;
        public static MenuButton quitButton;
        public static MenuButton backButton;
        public static MenuButton resumeButton;
        public static MenuButton menuButton;

        public enum etats
        {
            MenuPrincipal,
            MenuInGame,
            MenuPlay,
            MenuOption,
            InGame
        }; //Les états du menu
        public static etats MenuState;

        public static void Initialize()
        {
            MenuState = etats.MenuPrincipal;
        }

        public static void LoadContent(Game1 game)
        {
            playButton = new MenuButton("Play", game, 1, MenuButton.utility.Play);
            optionButton = new MenuButton("Option", game, 2, MenuButton.utility.Option);
            quitButton = new MenuButton("Quit", game, 3, MenuButton.utility.Quit);
            backButton = new MenuButton("Back", game, 2, MenuButton.utility.Back);
            resumeButton = new MenuButton("Resume", game, 1, MenuButton.utility.Resume);
            menuButton = new MenuButton("Menu", game, 2, MenuButton.utility.Menu);
        }

        public static void Update(Game1 game, ButtonState SourisLastState)
        {
            switch (MenuState) //Update selon l'état
            {
                case etats.MenuPrincipal:
                    playButton.Update(game, SourisLastState);
                    optionButton.Update(game, SourisLastState);
                    quitButton.Update(game, SourisLastState);
                    break;
                case etats.MenuInGame:
                    resumeButton.Update(game, SourisLastState);
                    menuButton.Update(game, SourisLastState);
                    break;
                case etats.MenuPlay:
                    break;
                case etats.MenuOption:
                    backButton.Update(game, SourisLastState);
                    break;
                case etats.InGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        MenuState = etats.MenuInGame;
                    }
                    break;
                default:
                    break;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (MenuState) //Desine selon l'état
            {
                case etats.MenuPrincipal:
                    playButton.Draw(spriteBatch);
                    optionButton.Draw(spriteBatch);
                    quitButton.Draw(spriteBatch);
                    break;
                case etats.MenuInGame:
                    resumeButton.Draw(spriteBatch);
                    menuButton.Draw(spriteBatch);
                    break;
                case etats.MenuPlay:
                    break;
                case etats.MenuOption:
                    backButton.Draw(spriteBatch);
                    break;
                case etats.InGame:
                    
                    break;
                default:
                    break;
            }
        }

        
    }
}
