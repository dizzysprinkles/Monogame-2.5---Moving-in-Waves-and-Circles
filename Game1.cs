using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_2._5___Moving_in_Waves_and_Circles
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont bouncingFont;
        Texture2D ballTexture;

        Rectangle wavingBallRect;

        // Ball traveling in a wave
        float ballWavestep;
        int ballWaveSpeed;  // Horizontal
        int ballWaveAmplitude;

        // Bouncing Letters
        string bouncingText;
        float bouncingTextY;
        float bouncingTextStep;
        float ballWavingOffset;
        int bouncingTextAmplitude;

        double circleX, circleY, circleStep;
        int circleAmplitude;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Ball
            ballWavestep = 0; //Doesn't change anything visible as it is changed in UPDATE
            ballWaveSpeed = 2; // Changes speed which changes period
            ballWaveAmplitude = 50; // Changes Amplitude of wave
            ballWavingOffset = 0; //Doesn't change anything visible as it is changed in UPDATE
            wavingBallRect = new Rectangle(10, 100, 50, 50);

            // Circle
            circleStep = 0; //Doesn't change anything visible as it is changed in UPDATE
            circleAmplitude = 50; // Changes Amplitude/Diameter of circle path
            circleX = 0; //Doesn't change anything visible as it is changed in UPDATE
            circleY = 0; //Doesn't change anything visible as it is changed in UPDATE


            // Text Values
            bouncingText = "Hello there!"; // Text that is displayed
            bouncingTextAmplitude = 25; //Changes Height/Amplitude
            bouncingTextStep = 0; //Doesn't change anything visible as it is changed in UPDATE
            bouncingTextY = 75; // Doesn't change anything visible as it is changed in UPDATE
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ballTexture = Content.Load<Texture2D>("Images/beachBall");
            bouncingFont = Content.Load<SpriteFont>("Fonts/TextFont");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Ball Waving Logic
            ballWavingOffset = (int)Math.Round(-1 * Math.Sin(ballWavestep) * ballWaveAmplitude) + 200;
            ballWavestep += 0.1f;

            //Ball Horizontal Movement
            wavingBallRect.X += ballWaveSpeed;
            if (wavingBallRect.Right > _graphics.PreferredBackBufferWidth || wavingBallRect.X < 0)
                ballWaveSpeed *= -1;

            // Text Logic - Hello There
            bouncingTextY = (int)Math.Round(-1 * Math.Sin(bouncingTextStep) * bouncingTextAmplitude);
            bouncingTextStep += 0.2f;

            // Circle Ball
            circleStep += 0.1;
            circleX = Math.Cos(circleStep) * circleAmplitude;
            circleY = -1 * Math.Sin(circleStep) * circleAmplitude;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // Wave Ball
            _spriteBatch.Draw(ballTexture, new Rectangle(wavingBallRect.X, wavingBallRect.Y + (int)Math.Round(ballWavingOffset), wavingBallRect.Width, wavingBallRect.Height), Color.White);

            // Letters Bouncing
            for (int i = 0; i < bouncingText.Length; i++) // Iterate through each letter
                _spriteBatch.DrawString(bouncingFont, bouncingText[i].ToString(), new Vector2(10 + 12 * i, 100 + (int)Math.Round(-1 * Math.Sin(bouncingTextStep + 0.2 * i) * bouncingTextAmplitude)), Color.Black);

            // Ball in circle
            _spriteBatch.Draw(ballTexture, new Rectangle(400 + (int)Math.Round(circleX), 200 + (int)Math.Round(circleY), 50, 50), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
