using BaseGun.Graphics;
using BaseGun.Helpers;
using Microsoft.Xna.Framework;

namespace BaseGun.Model
{
    public sealed class SimpleEnemy : AbstractEnemy
    {
        private const int SHIP_WIDTH = 5;
        private const int SHIP_HEIGHT = 5;
        private const int MOVE_SPEED = 20;

        private readonly PixelDrawer _pixelDrawer;

        public SimpleEnemy(Game game, ref PixelDrawer pixelDrawer)
            :
            base(game)
        {
            _pixelDrawer = pixelDrawer;

            Health = 100;
            Shield = 20;
            Position = new Vector2(
                Randomizer.GetRandomNumber(0, GraphicsDevice.PresentationParameters.Bounds.Width),
                Randomizer.GetRandomNumber(0, GraphicsDevice.PresentationParameters.Bounds.Height)
                );

            //Speed = new Vector2(
            //    (Randomizer.GetRandomNumber(0, 1) == 1) ? MOVE_SPEED : -MOVE_SPEED,
            //    (Randomizer.GetRandomNumber(0, 1) == 1) ? MOVE_SPEED : -MOVE_SPEED
            //    );

            while (Speed == Vector2.Zero)
            {
                Speed = new Vector2(
                Randomizer.GetRandomNumber(-30, 30) / 10,
                Randomizer.GetRandomNumber(-30, 30) / 10
                );
            }
            
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;

            CheckBounds();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _pixelDrawer.DrawSquare(Position, new Vector2(10, 10), Color.Red);

            base.Draw(gameTime);
        }

        private void CheckBounds()
        {
            if (Position.X - SHIP_WIDTH < 0.0f)
            {
                Speed = new Vector2(-Speed.X, Speed.Y);
                Position = new Vector2(SHIP_WIDTH + 1, Position.Y);
            }

            if (Position.X + SHIP_WIDTH > Game.Window.ClientBounds.Width)
            {
                Speed = new Vector2(-Speed.X, Speed.Y);
                Position = new Vector2(Game.Window.ClientBounds.Width - SHIP_WIDTH - 1, Position.Y);
            }

            if (Position.Y - SHIP_HEIGHT <= 0.0f)
            {
                Speed = new Vector2(Speed.X, -Speed.Y);
                Position = new Vector2(Position.X, SHIP_HEIGHT + 1);
            }

            if (Position.Y + SHIP_HEIGHT >= Game.Window.ClientBounds.Height)
            {
                Speed = new Vector2(Speed.X, -Speed.Y);
                Position = new Vector2(Position.X, Game.Window.ClientBounds.Height - SHIP_HEIGHT - 1);
            }
        }
    }
}