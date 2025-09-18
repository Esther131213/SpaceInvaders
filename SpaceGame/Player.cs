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
    public class Player
    {
        Texture2D tex;
        public Vector2 pos;
        int playerSpeed;
        public int playerHealth;
        public bool pIsAlive = true;

        public Player(Texture2D tex, Vector2 pos, int playerSpeed)
        {
            this.tex = tex;
            this.pos = pos;
            this.playerSpeed = playerSpeed;
            playerHealth = 3;
        }

        public void Update(GameTime gameTime)
        {
            /*
            if (playerHealth == 0)
            {
                pIsAlive = false;
            }
            */
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Right) && pos.X < 600)
            {
                pos.X += playerSpeed;
            }
            if (kstate.IsKeyDown(Keys.Left) && pos.X > 40)
            {
                pos.X -= playerSpeed;
            }
        }

        public void takeDamage()
        {
            playerHealth = -1;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(tex.Width /2, tex.Height /2), 1, SpriteEffects.None, 1);
        }
    }
}
