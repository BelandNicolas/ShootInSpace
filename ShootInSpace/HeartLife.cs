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
    public class HeartLife
    {
        //Champs
        public static int Width = 28;
        public static int Height = 28;
        Texture2D spriteActuel;
        Texture2D spriteHeartFull;
        Texture2D spriteHeartEmpty;
        Rectangle BoxCollider;
        public bool IsFull;

        //Methods

        public HeartLife(Game1 game, int posX)
        {
            spriteHeartFull = game.Content.Load<Texture2D>("HeartFull.png");
            spriteHeartEmpty = game.Content.Load<Texture2D>("HeartEmpty.png");
            BoxCollider = new Rectangle(posX, 0, Width, Height);
            IsFull = true;
            spriteActuel = spriteHeartFull;
        }

        //Function

        public void Update()
        {
            if (IsFull)
            {
                spriteActuel = spriteHeartFull;
            }
            else
            {
                spriteActuel = spriteHeartEmpty;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteActuel,BoxCollider, Color.White);
        }
    }
}
