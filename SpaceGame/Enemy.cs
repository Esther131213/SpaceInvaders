using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;


namespace SpaceGame
{
    public class Enemy
    {
        Texture2D tex;
        Vector2 pos;
        int enemySpeedDown;

        public Enemy(Texture2D tex, Vector2 pos, int enemySpeedDown)
        {
            this.tex = tex;
            this.pos = pos;
            this.enemySpeedDown = enemySpeedDown;
            System.Diagnostics.Debug.WriteLine(pos);
        }

        public void Update(GameTime gameTime)
        {
            if (pos.Y == 890)
            {
                enemySpeedDown = 0;
            }
            pos.Y += enemySpeedDown;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);

        }
    }
}
