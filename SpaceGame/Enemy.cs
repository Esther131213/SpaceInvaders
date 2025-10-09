using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;


namespace SpaceGame
{
    public class Enemy
    {
        Texture2D tex;
        public Vector2 pos;
        float enemySpeed;
        public bool eIsAlive = true;
        public Rectangle eHitBox;
        //bool goingLeft = true;
        //bool goingRight = false;
        bool goingSide = true;
        bool goingDown = false;
        int riktning = 1; 
        bool CurrentDirection; //True = Right, False = Left
        int timer = 0;
        int frame;
        double frameTimer = 0;
        double frameInterval = 2000;
        int movementAmount = 0;
        public int enemyHealth;
        public int eScore;

        public Enemy(Texture2D tex, Vector2 pos, float enemySpeed, int enemyHealth, int eScore)
        {
            this.tex = tex;
            this.pos = pos;
            this.enemySpeed = enemySpeed;
            this.enemyHealth = enemyHealth;
            this.eScore = eScore;
        }

        public void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (frameTimer >= frameInterval)
            {
                frameTimer = 0;
                if (goingSide)
                {
                    riktning *= -1;
                    goingDown = true;
                    goingSide = false;
                }

                Debug.WriteLine("Den kör!" + timer);
            }

            if (goingSide)
            {
                pos.X += riktning * enemySpeed;
            }
            else if (goingDown)
            {
                pos.Y += 1 * enemySpeed;

                frameTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (frameTimer >= frameInterval)
                {
                    frameTimer = 0;
                    if (goingDown)
                    {
                        goingSide = true;
                        goingDown = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            eHitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            if (enemyHealth == 1)
            {
                spriteBatch.Draw(tex, pos, null, Color.White, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);
            }
            else if (enemyHealth == 2)
            {
                spriteBatch.Draw(tex, pos, null, Color.DarkOliveGreen, 0, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);
            }
        }

        public Rectangle GetHitBox()
        {
            return eHitBox;
        }
    }
}
