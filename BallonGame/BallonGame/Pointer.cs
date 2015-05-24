using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallonGame
{
    class bead
    {
        #region fields
        Texture2D sprite;
        Rectangle drawRectangle;        
        #endregion

        #region Constructors
        public bead(Texture2D thesptire){
            sprite = thesptire;
            drawRectangle = new Rectangle(0,0,30, 30);
        }
        #endregion

        #region properties
        private int X
        {
            get { return drawRectangle.Center.X; }
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                // clamp to keep in range
                if (drawRectangle.X < 0)
                {
                    drawRectangle.X = 0;
                }
                else if (drawRectangle.X > 800 - drawRectangle.Width)
                {
                    drawRectangle.X = 800 - drawRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Gets and sets the y location of the center of the burger
        /// </summary>
        private int Y
        {
            get { return drawRectangle.Center.Y; }
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                // clamp to keep in range
                if (drawRectangle.Y < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if (drawRectangle.Y > 600 - drawRectangle.Height)
                {
                    drawRectangle.Y = 600 - drawRectangle.Height;
                }
            }
        }

        #endregion


        #region method
        public void Update(MouseState mouse)
        {
            drawRectangle.X = mouse.X;
            drawRectangle.Y = mouse.Y;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, new Color(86,145,225));
        }
        #endregion
    }
}
