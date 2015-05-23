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
    class Ballon
    {
        #region Fields
        Texture2D sprite;
        Rectangle drawRectangle;
        double velocity;
        bool active = true;
        #endregion

        #region Constructors
        public Ballon(int X, double v, Texture2D theSprite)
        {
            velocity = v;
            sprite = theSprite;
            drawRectangle = new Rectangle(X, 600-sprite.Height, sprite.Width, sprite.Height);
        }
        #endregion

        #region Properties
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        #endregion

        #region methon
        public void Update(GameTime gametime)
        {
            drawRectangle.Y -= Convert.ToInt32(gametime.ElapsedGameTime.Milliseconds * velocity);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, new Color(86,154,232));
        }
        #endregion
    }
}
