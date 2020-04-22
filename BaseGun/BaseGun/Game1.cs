using BaseGun.Graphics;
using BaseGun.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BaseGun
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        private PixelDrawer _pixelDrawer;
        private PlayerGunShip _gunShip;
        private List<SimpleEnemy> _enemies;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1000,
                PreferredBackBufferHeight = 625,
                SynchronizeWithVerticalRetrace = true,
            };

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixelDrawer = new PixelDrawer(this, ref _spriteBatch);
            _gunShip = new PlayerGunShip(this, ref _pixelDrawer);

            //_simpleEmemy = new SimpleEnemy(this, ref _pixelDrawer);
            //Components.Add(_simpleEmemy);

            _enemies = new List<SimpleEnemy>()
            {
                new SimpleEnemy(this, ref _pixelDrawer),
                new SimpleEnemy(this, ref _pixelDrawer),
                new SimpleEnemy(this, ref _pixelDrawer),
                new SimpleEnemy(this, ref _pixelDrawer),
                new SimpleEnemy(this, ref _pixelDrawer)
            };

            foreach (var enemy in _enemies)
            {
                Components.Add(enemy);
            }

            Components.Add(_gunShip);
            Components.Add(_pixelDrawer);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // a SpriteBatch can be used to draw textures.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            var keyboardState = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (keyboardState.IsKeyDown(Keys.F11))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.PreferredBackBufferWidth = _graphics.IsFullScreen ? 1366 : 1000;
                _graphics.PreferredBackBufferHeight = _graphics.IsFullScreen ? 768 : 625;
                _graphics.ApplyChanges();
            }

            // End method with call to base.Update
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // End method with call to base.Draw
            base.Draw(gameTime);
        }
    }
}