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
        Vector2 pos;
        int playerSpeed;

        public Player(Texture2D tex, Vector2 pos, int playerSpeed)
        {
            this.tex = tex;
            this.pos = pos;
            this.playerSpeed = playerSpeed;
        }

        public void Update(GameTime gameTime)
        {
            playerSpeed = 13;
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Right))
            {
                pos.X += playerSpeed;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                pos.X -= playerSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Microsoft.Xna.Framework.Color.White);
        }
    }
}
