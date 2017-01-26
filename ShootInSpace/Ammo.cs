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
    public class Ammo
    {
        Texture2D Sprite;
        public Rectangle BoxCollider;
        Rectangle BoxTexture;
        int Speed;
        public bool NeedToDelete;

        public Ammo(Texture2D textu,Vector2 pos)
        {
            Sprite = textu;
            BoxTexture = new Rectangle(0, 0, Sprite.Bounds.Width, Sprite.Bounds.Height);
            BoxCollider = new Rectangle((int)pos.X, (int)pos.Y, 10, 34);
            BoxCollider.Y = BoxCollider.Y - BoxCollider.Height - 30;
            Speed = 6;
            NeedToDelete = false;
        }

        public void Update(GameTime gameTime, Player player)
        {
            BoxCollider.Y -= Speed;
            if (BoxCollider.Y < -100)
            {
                NeedToDelete = true;
            }
            foreach (Meteor meteor in Game1.meteors) //Collision avec un meteor
            {
                if (BoxCollider.Intersects(meteor.BoxCollider))
                {
                    NeedToDelete = true;
                    meteor.NeedToDelete = true;
                    SoundsBank.PlaySoundsEffect("Explosion");
                    player.Score++;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, BoxCollider, BoxTexture, Color.White);
        }
    }
}
