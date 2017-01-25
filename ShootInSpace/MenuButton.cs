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
    public class MenuButton
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public string texte;
        public SpriteFont font;
        public Color color = Color.White;
        public byte nbPosition; //Sa position en aparition, 1 = tout en haut

        

        public MenuButton(string txt, Game1 game, byte pos, utility use)
        {
            
            nbPosition = pos;
            texture = game.Content.Load<Texture2D>("MenuGame.png");
            texte = txt;
            rectangle = new Rectangle(100, 100, MenuBase.WIDTHBUTTON, MenuBase.HEIGHTBUTTON);
            font = game.Content.Load<SpriteFont>("TexteMenu");
            rectangle.X = CenterScreenWidth(rectangle);
            rectangle.Y = PlaceVert(nbPosition);
            asignButton = use;
        }

        public enum utility
        {
            Play,
            Option,
            Quit,
            Back,
            Resume,
            Menu
        } // Les types d'utilité du bouton
        public utility asignButton;

        public void Update(Game game, ButtonState souris)
        {
            if (DetectPointInRect(rectangle, Mouse.GetState().Position)) //Si le curseur est sur le bouton
            {
                color = Color.Blue;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && souris == ButtonState.Released) //Si il clique
                {
                    Console.WriteLine(texte);
                    switch (asignButton) //Effectue la commande selon son utility
                    {
                        case utility.Play:
                            MenuBase.MenuState = MenuBase.etats.InGame;
                            break;
                        case utility.Option:
                            MenuBase.MenuState = MenuBase.etats.MenuOption;
                            break;
                        case utility.Quit:
                            game.Exit();
                            break;
                        case utility.Back:
                            MenuBase.MenuState = MenuBase.etats.MenuPrincipal;
                            break;
                        case utility.Resume:
                            MenuBase.MenuState = MenuBase.etats.InGame;
                            break;
                        case utility.Menu:
                            MenuBase.MenuState = MenuBase.etats.MenuPrincipal;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    color = Color.White;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, texture.Bounds,  color, 0.0f, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, texte, new Vector2(CenterTexteHoriz(rectangle, texte), CenterTexteVert(rectangle, texte)), color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1);
        }



        public bool DetectPointInRect(Rectangle source, Point target)
        {
            bool isDetect = false;

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    if (target.X == i + source.X && target.Y == j + source.Y)
                    {
                        isDetect = true;
                    }
                }
            }
            return isDetect;
        }
        public int CenterTexteHoriz(Rectangle rect, string txt)
        {
            return rect.X + (rect.Width / 2) - ((int)font.MeasureString(txt).X / 2);
        }
        public int CenterTexteVert(Rectangle rect, string txt)
        {
            return rect.Y + (rect.Height / 2) - ((int)font.MeasureString(txt).Y / 2);
        }
        public int CenterScreenWidth(Rectangle source)
        {
            return Game1.fenetre.Width / 2 - (source.Width / 2);
        }
        public int PlaceVert(byte pos)
        {
            return ((Game1.fenetre.Width / (MenuBase.NBBUTTON + 1)) * pos) - MenuBase.HEIGHTBUTTON / 2;
        }
    }
}
