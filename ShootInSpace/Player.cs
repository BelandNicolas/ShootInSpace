using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootInSpace
{
    public class Player
    {
        Texture2D Sprite;
        public Rectangle BoxCollider;
        Rectangle BoxTexture;
        int Speed;
        public int Score;
        public byte Life;
        public bool IsAlive;
        string messageMort = "Vous etes mort, appuyer sur R pour recommencer!";

        int bulletDelay = 2;

        KeyboardState lastKey;

        public static List<Ammo> bullets = new List<Ammo>();

        HeartLife[] heart = new HeartLife[5];

        public Player(Texture2D textu, Vector2 pos, Game1 game)
        {
            Sprite = textu;
            BoxTexture = new Rectangle(0, 0, Sprite.Bounds.Width, Sprite.Bounds.Height);
            BoxCollider = new Rectangle((int)pos.X, (int)pos.Y, 60, 60);
            BoxCollider.Y = BoxCollider.Y - BoxCollider.Height - 30;
            Speed = 6;
            Life = 5;
            IsAlive = true;
            for (int i = 0; i < heart.Length; i++)
            {
                heart[i] = new HeartLife(game, (Game1.fenetre.Width - HeartLife.Width) - (i * (HeartLife.Width + 5))); 
            }
        }

        //UPDATE & DRAW

        public void Update(GameTime gameTime, Game1 game)
        {
            if (IsAlive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    BoxCollider.X -= Speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    BoxCollider.X += Speed;
                }
                BoxCollider.X = MathHelper.Clamp(BoxCollider.X, Game1.fenetre.X, Game1.fenetre.Width - BoxCollider.Width);
                #region Shoot
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (lastKey.IsKeyUp(Keys.Space) && bulletDelay > 6) //tirer direct après avoir lacher la barre d'espace
                    {
                        bullets.Add(new Ammo(game.Content.Load<Texture2D>("FireShoot.png"), new Vector2(BoxCollider.X + BoxCollider.Width / 2 - 5, BoxCollider.Y - 10)));
                        SoundsBank.PlaySoundsEffect("LaserGunModify");
                    }
                    else
                    {
                        Shoot(game);
                    }
                }
                #endregion
                
                #region Collision meteor
                foreach (Meteor meteor in Game1.meteors)
                {
                    if (BoxCollider.Intersects(meteor.BoxCollider))
                    {
                        Life--;
                        meteor.NeedToDelete = true;
                        SoundsBank.PlaySoundsEffect("Explosion");
                    }
                }
                #endregion
                if (Life <= 0)
                {
                    IsAlive = false;
                }
                #region Gestion des coeurs
                for (int i = 0; i < heart.Length; i++)
                {
                    if (i >= Life) //Si le nombre de coeur en vie deviens vide si le chiffre du coeur est plus petit que le nombre de vie
                    {
                        heart[i].IsFull = false;
                    }
                    else
                    {
                        heart[i].IsFull = true;
                    }
                }
                foreach (HeartLife coeur in heart)
                {
                    coeur.Update();
                }
                #endregion
            }
            else //Si le joueur est mort
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    IsAlive = true;
                    Life = 5;
                    Score = 0;
                }
            }
            #region Ammo
            foreach (Ammo fire in bullets)
            {
                fire.Update(gameTime, this);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].NeedToDelete)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            } 
            #endregion           
            

            lastKey = Keyboard.GetState(); 
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsAlive) spriteBatch.Draw(Sprite, BoxCollider, BoxTexture, Color.White); //Dessine le héro

            foreach (Ammo fire in bullets)
            {
                fire.Draw(spriteBatch); //Dessine les balles
            }

            for (int i = 0; i < heart.Length; i++)
            {
                heart[i].Draw(spriteBatch); //Dessine les coeurs
            }

            if (Game1.DebugMode) spriteBatch.DrawString(Game1.debugSpriteFont, "Fire Rate : " + bulletDelay.ToString(), new Vector2(10, 150), Color.White);
            if (Game1.DebugMode) spriteBatch.DrawString(Game1.debugSpriteFont, "Vie du joueur : " + Life.ToString(), new Vector2(10, 200), Color.White);
            if (!IsAlive) spriteBatch.DrawString(Game1.debugSpriteFont, messageMort, new Vector2(Game1.fenetre.Width / 2 - Game1.debugSpriteFont.MeasureString(messageMort).X / 2, Game1.fenetre.Height / 2 - Game1.debugSpriteFont.MeasureString(messageMort).Y), Color.White, 0.0f, Vector2.Zero, 1.2f, SpriteEffects.None, 1);
        }

        public void Shoot(Game1 game)
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            if (bulletDelay <= 0)
            {
                bullets.Add(new Ammo(game.Content.Load<Texture2D>("FireShoot.png"), new Vector2(BoxCollider.X + BoxCollider.Width / 2 - 5, BoxCollider.Y - 10)));
                SoundsBank.PlaySoundsEffect("LaserGunModify");
            }
            //Reset bullet delay
            if (bulletDelay == 0)
            {
                bulletDelay = 20;
            }
        }
    }
}
