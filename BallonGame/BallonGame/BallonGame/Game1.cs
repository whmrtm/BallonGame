using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BallonGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Game Constants
        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = 800;
        const double VELOCITY_RANGE = 1.5;
        // Prepare the pictures
        Texture2D pointerSprite;
        Texture2D ballonSprite;

        // Declare the objects
        bead mybead;
        List<Ballon> ballons = new List<Ballon>();

        // Preparion for generating random velocity and X position
        Random random = new Random();

        // Time preparision
        int TOTAL_ECLIPSE_TIME = 1000;
        int eclipseTime = 0;
        int CLICK_RANGE = 20;
        
        //click processing
        //bool leftClickStarted = false;
        bool leftClickReleased = true;
        
        // text display
        SpriteFont font;
        Vector2 scorePosition = new Vector2(WINDOW_WIDTH / 12, WINDOW_HEIGHT / 12);
        Vector2 Missposition = new Vector2(WINDOW_WIDTH / 12 * 9, WINDOW_HEIGHT / 12);
        // Score handling
        int score = 0;
        int miss = 0;
        string scoreString;
        string missString;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set the resolution
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballonSprite = Content.Load<Texture2D>("red");
            pointerSprite = Content.Load<Texture2D>(@"pointer");
            font = Content.Load<SpriteFont>("Arial"); 
            mybead = new bead(pointerSprite);
            score = 0;
            miss = 0;
            scoreString = "Score: " + score;
            missString = "Miss: " + miss;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // Update Mouse Cursor
            MouseState mouse = Mouse.GetState();
            mybead.Update(mouse);

            // Generate a ballon every random 1~3 seconds
            eclipseTime += gameTime.ElapsedGameTime.Milliseconds;
            if (eclipseTime >= TOTAL_ECLIPSE_TIME)
            {
                eclipseTime = 0;
                TOTAL_ECLIPSE_TIME = random.Next(1000, 2000);
                int Xposion = random.Next(0,WINDOW_WIDTH - ballonSprite.Width);
                double v = VELOCITY_RANGE * random.NextDouble() + 0.01;
                ballons.Add(new Ballon(Xposion, v,ballonSprite));
            }
            // update all the members in the ballons
            foreach(Ballon myballon in ballons){
                myballon.Update(gameTime);
            }

            // when the moust leftClicks on the ballon, the ballon becomes in active, Score add one point
            if (mouse.LeftButton == ButtonState.Pressed && leftClickReleased)
            {
                //leftClickStarted = true;
                leftClickReleased = false;
                Vector2 mousePosition = new Vector2(mouse.X, mouse.Y);
                foreach (Ballon myballon in ballons)
                {   
                    Vector2 BallonCenter = new Vector2(myballon.CollisionRectangle.Center.X, myballon.CollisionRectangle.Center.Y);
                    CLICK_RANGE = myballon.CollisionRectangle.Width;
                    if (Vector2.Distance(mousePosition, BallonCenter) <= CLICK_RANGE)
                    {
                        score += 1;
                        scoreString = "Score: " + score;
                        myballon.Active = false;
                    }
                }
            }
                else if(mouse.LeftButton == ButtonState.Released && leftClickReleased == false){
                    leftClickReleased = true;
                    //leftClickStarted = false;
                }
            // make all the ballons that run out of the window inactive, miss add one point
            
            foreach (Ballon myballon in ballons)
            {   
                if (myballon.CollisionRectangle.Y < 0-myballon.CollisionRectangle.Height)
                {
                    myballon.Active = false;
                    miss++;
                    missString = "Miss: " + miss;
                }
            }

            //remove all the inactive ballons
            for (int i = 0; i < ballons.Count(); i++)
            {
                if (ballons[i].Active == false)
                {
                    ballons.RemoveAt(i);
                }
            }

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            mybead.Draw(spriteBatch);
            foreach (Ballon myballon in ballons)
            {
                myballon.Draw(spriteBatch);
            }
            spriteBatch.DrawString(font, scoreString, scorePosition, Color.Black);
            spriteBatch.DrawString(font, missString, Missposition, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
