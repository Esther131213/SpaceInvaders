using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceGame
{
    internal class Bullet
    {
        Texture2D tex;
        public Vector2 pos;
        int bulletSpeed;
        public bool isAlive = true;
        public Rectangle bHitBox;

        public Bullet(Texture2D tex, Vector2 pos, int bulletSpeed)
        {
            this.tex = tex;
            this.pos = pos;
            this.bulletSpeed = bulletSpeed;
        }

        public void Update(GameTime gameTime)
        {
            //Makes the bullet constantly move upward, unless it has reached Y = 0.
            pos.Y -= bulletSpeed;
            if (pos.Y < 0)
            {
                isAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bHitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteBatch.Draw(tex, pos, null, Color.White, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);
        }

        public Rectangle GetHitBox()
        {
            return bHitBox;
        }
    }
}
