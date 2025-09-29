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
    public class Enemy
    {
        Texture2D tex;
        public Vector2 pos;
        float enemySpeed;
        public bool eIsAlive = true;
        public Rectangle eHitBox;
        bool goingLeft = false;
        bool goingRight = false;
        bool goingDown = true;

        public Enemy(Texture2D tex, Vector2 pos, float enemySpeed)
        {
            this.tex = tex;
            this.pos = pos;
            this.enemySpeed = enemySpeed;
        }

        public void Update(GameTime gameTime)
        {
            //Makes the enemy constantly move down towards the player
            pos.Y += enemySpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            eHitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteBatch.Draw(tex, pos, null, Color.White, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);
        }

        public Rectangle GetHitBox()
        {
            return eHitBox;
        }
    }
}
