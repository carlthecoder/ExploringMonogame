using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BaseGun.Graphics
{
    public sealed class PixelDrawer : DrawableGameComponent
    {
        private Texture2D _canvas;
        private uint[] _pixels;
        private Rectangle _textRect;
        private SpriteBatch _spriteBatch;

        public PixelDrawer(Game game, ref SpriteBatch spriteBatch)
            :
            base(game)
        {
            _spriteBatch = spriteBatch;
        }

        protected override void LoadContent()
        {
            _textRect = GraphicsDevice.PresentationParameters.Bounds;
            _canvas = new Texture2D(GraphicsDevice, _textRect.Width, _textRect.Height, false, SurfaceFormat.Color);
            _pixels = new uint[_textRect.Width * _textRect.Height];

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ClearPixels();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _canvas.SetData(_pixels, 0, _textRect.Width * _textRect.Height);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_canvas, new Rectangle(0, 0, _textRect.Width, _textRect.Height), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawPixel(float x, float y, Color color)
        {
            int xPos = (int)x;
            int yPos = (int)y;

            var drawPosition = xPos + yPos * _textRect.Width;
                    _pixels[drawPosition] = color.PackedValue;
        }

        public void DrawSquare(Vector2 position, Vector2 size, Color color)
        {
            var xBound = (int)Math.Round(size.X / 2);
            var yBound = (int)Math.Round(size.Y / 2);

            for (int width = -xBound; width < xBound; width++)
            {
                for (int height = -yBound; height < yBound; height++)
                {
                    DrawPixel(position.X + width, position.Y + height, color);
                }
            }
        }

        public void DrawTriangle(Vector2 position, float boundY, Color color)
        {
            var yBound = (int)Math.Round(boundY / 2.0);
            var xBound = 0;

            for (var drawHeight = -yBound; drawHeight < yBound; drawHeight++)
            {
                for (int width = 0; width < xBound; width++)
                {
                    DrawPixel(position.X + width - drawHeight - yBound, position.Y + drawHeight, color);
                }
                xBound += 2;
            }
        }

        private void ClearPixels()
        {
            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = 0x00000000;
            }
        }
    }
}