using System;
using BaseGun2.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGun2.Model
{
    public sealed class Droid
    {
        private const int DIM = 20;

        private readonly Game _game;
        private readonly SpriteBatch _spriteBatch;

        private Texture2D _texture;
        private uint[] _pixels;

        public Vector2 Origin = new Vector2(DIM / 2.0f);
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 Orientation { get; set; }
        public float Rotation { get; set; }

        public Droid(Game game, SpriteBatch spriteBatch)
        {
            _game = game;
            _spriteBatch = spriteBatch;

            while(Speed == Vector2.Zero)
            {
                Speed = new Vector2(
                Randomizer.GetRandomNumber(-100, 100) / 10,
                Randomizer.GetRandomNumber(-100, 100) / 10);
            }
            
            Orientation = Vector2.UnitY;
        }

        public Droid(Game game, SpriteBatch spriteBatch, Vector2 position = new Vector2())
            : this(game, spriteBatch)
        {
            Position = (position == Vector2.Zero) ?
                new Vector2(
                _game.GraphicsDevice.PresentationParameters.Bounds.Width / 2.0f,
                _game.GraphicsDevice.PresentationParameters.Bounds.Height / 2.0f)
                :
                new Vector2(position.X - DIM / 2, position.Y - DIM / 2);
        }

        public static Droid CreateDroid(Game game, SpriteBatch spriteBatch, Vector2 position)
        {
            var droid = new Droid(game, spriteBatch, position);
            droid.LoadContent();

            return droid;
        }

        public void LoadContent()
        {
            _pixels = new uint[DIM * DIM];
            _texture = new Texture2D(_game.GraphicsDevice, DIM, DIM);

            DrawDroid();
        }

        public void Update(GameTime gameTime)
        {
            Rotation += 0.1f;
            Position += Speed;

            CheckBounds();
        }

        private void CheckBounds()
        {
            if (Position.X <= -DIM / 2)
            {
                Position = new Vector2(_game.GraphicsDevice.PresentationParameters.Bounds.Width + DIM / 2, Position.Y);
            }
            else if (Position.X > _game.GraphicsDevice.PresentationParameters.Bounds.Width + DIM / 2)
            {
                Position = new Vector2(0 - DIM / 2, Position.Y);
            }

            if (Position.Y <= 0 - DIM / 2)
            {
                Position = new Vector2(Position.X, _game.GraphicsDevice.PresentationParameters.Bounds.Height + DIM / 2);
            }
            else if (Position.Y > _game.GraphicsDevice.PresentationParameters.Bounds.Height + DIM / 2)
            {
                Position = new Vector2(Position.X, 0 - DIM / 2);
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1.0f, SpriteEffects.None,0);
            _spriteBatch.End();
        }

        private void DrawDroid()
        {
            var offset = (int)DIM / 2;

            for (int x = -10; x < 10; x++)
            {
                for (int y = -4; y < 4; y++)
                {
                    PutPixel(x + offset, y + offset);
                }
            }

            for (int x = -4; x < 4; x++)
            {
                for (int y = -10; y < 10; y++)
                {
                    PutPixel(x + offset, y + offset);
                }
            }

            _texture.SetData(_pixels, 0, DIM * DIM);
        }

        private void PutPixel(int x, int y)
        {
            _pixels[x + y * DIM] = Color.Red.PackedValue;
        }
    }
}