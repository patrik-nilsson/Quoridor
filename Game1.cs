using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Quoridor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum GameState
        {
            player1,
            player2,
            gameover
        }
        GameState currenState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Random random;
        Player[] players;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            texture = Content.Load<Texture2D>("Pixel");
            Map.Initialize(9, 9, texture);
            players = new Player[2];
            random = new Random();
            //int starter = random.Next(0, 2);
            int starter = 0;
            if (starter == 0)
            {
                players[0] = new Human(new Point(4, 0), Color.Blue);
                players[1] = new Bot(new Point(4, 8), Color.Red);
            }
            else
            {
                players[0] = new Bot(new Point(4, 8), Color.Red);
                players[1] = new Human(new Point(4, 0), Color.Blue);
            }
            currenState = GameState.player1;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Map.Update();
            players[0].Update();
            players[1].Update();
            switch (currenState)
            {
                case GameState.player1:

                    if (players[0].actionTaken)
                    {
                        if (players[0].ReachedGoal())
                        {
                            currenState = GameState.gameover;
                        }
                        else
                        {
                            currenState = GameState.player2;
                            players[0].actionTaken = false;
                        }
                    }
                    break;
                case GameState.player2:
                    players[1].ActionUpdate();
                    if (players[1].actionTaken)
                    {
                        if (players[1].ReachedGoal())
                        {
                            currenState = GameState.gameover;
                        }
                        else
                        {
                            currenState = GameState.player1;
                            players[1].actionTaken = false;
                        }
                    }
                    break;
                case GameState.gameover:

                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            Map.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
