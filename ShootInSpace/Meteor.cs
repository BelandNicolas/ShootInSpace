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
    public class Meteor
    {
        Texture2D Sprite;
        public Rectangle BoxCollider;
        Rectangle BoxTexture;
        int Speed;
        public bool NeedToDelete;

        public Meteor(Texture2D textu, int spd, Vector2 pos)
        {
            Sprite = textu;
            BoxTexture = textu.Bounds;
            BoxCollider.X = (int)pos.X;
            BoxCollider.Y = (int)pos.Y;
            BoxCollider.Width = 40;
            BoxCollider.Height = 40;
            Speed = spd;
        }

        public void Update(GameTime gameTime)
        {
            BoxCollider.Y += Speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, BoxCollider, BoxTexture, Color.White);
        }
    }
}
