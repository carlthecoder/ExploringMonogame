using BaseGun.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseGun.Model
{
    public sealed class PlayerGunShip : DrawableGameComponent
    {
        private const float MOVE_SPEED = 5.0f;
        private const float SLOW_DOWN_RATE = 0.5f;
        private const float SHIP_WIDTH = 60;
        private const float SHIP_HEIGHT = 40;

        private readonly PixelDrawer _pixelDrawer;

        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public double Health { get; set; }
        public double Shield { get; set; }

        public PlayerGunShip(Game game, ref PixelDrawer pixelDrawer)
            :
            base(game)
        {
            _pixelDrawer = pixelDrawer;

            Position = new Vector2(
                GraphicsDevice.PresentationParameters.Bounds.Width / 2,
                GraphicsDevice.PresentationParameters.Bounds.Height * 0.9f
                );

            Health = 100;
            Shield = 100;
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D))
            {
                Speed = new Vector2(MOVE_SPEED, Speed.Y);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Speed = new Vector2(-MOVE_SPEED, Speed.Y);
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Speed = new Vector2(Speed.X, -MOVE_SPEED);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Speed = new Vector2(Speed.X, MOVE_SPEED);
            }

            Position = new Vector2(Position.X + Speed.X, Position.Y + Speed.Y);

            SlowDownAferMove();

            CheckBounds();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _pixelDrawer.DrawSquare(new Vector2(Position.X, Position.Y), new Vector2(5, SHIP_HEIGHT), Color.Red);
            _pixelDrawer.DrawSquare(new Vector2(Position.X, Position.Y + 15), new Vector2(SHIP_WIDTH, 10), Color.Blue);
            _pixelDrawer.DrawTriangle(new Vector2(Position.X, Position.Y - 5), 25, Color.Blue);

            base.Draw(gameTime);
        }

        private void SlowDownAferMove()
        {
            if (Speed.X > 0.0f)
            {
                Speed = new Vector2(Speed.X - SLOW_DOWN_RATE, Speed.Y);
            }
            else if (Speed.X < 0.0f)
            {
                Speed = new Vector2(Speed.X + SLOW_DOWN_RATE, Speed.Y);
            }

            if (Speed.Y > 0.0f)
            {
                Speed = new Vector2(Speed.X, Speed.Y - SLOW_DOWN_RATE);
            }
            else if (Speed.Y < 0.0f)
            {
                Speed = new Vector2(Speed.X, Speed.Y + SLOW_DOWN_RATE);
            }
        }

        private void CheckBounds()
        {
            if (Position.X - (SHIP_WIDTH / 2) < 0.0)
            {
                Position = new Vector2((SHIP_WIDTH / 2), Position.Y);
            }

            if (Position.Y - (SHIP_HEIGHT / 2) < 0.0)
            {
                Position = new Vector2(Position.X, (SHIP_HEIGHT / 2));
            }

            if (Position.X + (SHIP_WIDTH / 2) > Game.Window.ClientBounds.Width)
            {
                Position = new Vector2(Game.Window.ClientBounds.Width - (SHIP_WIDTH / 2), Position.Y);
            }

            if (Position.Y + (SHIP_HEIGHT / 2) > Game.Window.ClientBounds.Height)
            {
                Position = new Vector2(Position.X, Game.Window.ClientBounds.Height - (SHIP_HEIGHT / 2));
            }
        }
    }
}