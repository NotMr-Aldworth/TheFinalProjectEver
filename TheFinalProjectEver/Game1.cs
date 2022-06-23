using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheFinalProjectEver
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        Rectangle backgroundRect;
        Texture2D backgroundTexture;
        Rectangle boomRect;
        Texture2D boomTexture;
        Rectangle badboyRect;
        Texture2D badboyTexture;
        Vector2 badboySpeed;
        Rectangle pattyRect;
        Texture2D pattyTexture;
        Rectangle pattyBorderRect;
        Vector2 pattySpeed;
        Texture2D wildWestTexture;
        Rectangle wildWestRect;
        Rectangle fightRect;
        Texture2D spongeTexture;

        Rectangle spongeRect;


        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        SoundEffect music;
        SoundEffectInstance musicInstance;

        SpriteFont titleFont;
        float seconds;
        float startTime;
        enum Screen
        {
            Intro,
            WildWest,
            Ending
        }
        Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            this.Window.Title = "Old Fashion Standoff";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            screen = Screen.Intro;
            pattySpeed = new Vector2(-1, 0);
            badboySpeed = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("ye old town");
            backgroundRect = new Rectangle(0, 0, 800, 500);
            boomTexture = Content.Load<Texture2D>("booming");
            boomRect = new Rectangle(0, 0, 800, 500);
            badboyTexture = Content.Load<Texture2D>("badboy");
            badboyRect = new Rectangle(200, 300, 100, 125);
            pattyTexture = Content.Load<Texture2D>("patty");
            pattyRect = new Rectangle(800, 300, 100, 125);
            pattyBorderRect = new Rectangle(400, 100, 10, 100);
            fightRect = new Rectangle(500, 300, 300, 10);
            spongeTexture = Content.Load<Texture2D>("ye old town");
            spongeRect = new Rectangle(0, 0, 800, 500);
            wildWestTexture = Content.Load<Texture2D>("wildwest");
            wildWestRect = new Rectangle(0, 0, 800, 500);
            music = Content.Load<SoundEffect>("music");
            musicInstance = music.CreateInstance();
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            titleFont = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds;
            if (screen == Screen.Intro)
            {
                if (musicInstance.State == SoundState.Stopped)
                    musicInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.WildWest;
                    musicInstance.Stop();
                }


            }
            else if (screen == Screen.WildWest)
            {
                pattyRect.X += (int)pattySpeed.X;
                pattyRect.Y += (int)pattySpeed.Y;
                badboyRect.X += (int)badboySpeed.X;
                badboyRect.Y += (int)badboySpeed.Y;

                if (pattyRect.Left <= pattyBorderRect.Right)
                {
                    pattySpeed.X *= 0;
                    badboySpeed.X = 1;
                }
                if (badboyRect.Right >= pattyBorderRect.Right)
                {
                    badboySpeed.X = 0;
                    badboySpeed.Y = 5;
                    pattySpeed.Y = -5;
                    explodeInstance.Play();
                    seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                    seconds = 0;
                    if (seconds >= 5)
                    {

                        screen = Screen.Ending;
                    }
                if (screen == Screen.Ending)
                    {
                        explodeInstance.Stop();
                    }
                }




            }
            


            mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(wildWestTexture, wildWestRect, Color.White);
                _spriteBatch.DrawString(titleFont, "Click to watch the duel of a lifetime!", new Vector2(100, 100), Color.Black);
            }
            if (screen == Screen.WildWest)
            {
                _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
                _spriteBatch.Draw(badboyTexture, badboyRect, Color.White);
                _spriteBatch.Draw(pattyTexture, pattyRect, Color.White);
                if (badboyRect.Right >= pattyBorderRect.Right)
                { 
                    _spriteBatch.Draw(boomTexture, boomRect, Color.White);
                    _spriteBatch.Draw(badboyTexture, badboyRect, Color.White);
                    _spriteBatch.Draw(pattyTexture, pattyRect, Color.White);
                }

            if (screen == Screen.Ending)
                {
                    _spriteBatch.Draw(spongeTexture, spongeRect, Color.White);
                }

            }



            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
