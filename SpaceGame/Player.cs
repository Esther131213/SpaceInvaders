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
    internal class Player
    {
        Texture2D texture;
        Vector2 position;
        int playerSpeed = 10;

        public player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;

            this.position = new Vector2(100, 100);
        }

        public void Load()
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
            {
                position.X += playerSpeed;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                position.X -= playerSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Microsoft.Xna.Framework.Color.White);
        }
    }
}
