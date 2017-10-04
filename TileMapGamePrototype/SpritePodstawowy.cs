/* Artur Dobrzanski */ 
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

namespace Krasnal
{
    public class SpritePodstawowy
    {
        protected Texture2D spriteTexture, spriteTexture2;
        public Rectangle spriteRectangle;

        public void LoadTexture(Texture2D inSpriteTexture)
        {
            spriteTexture = inSpriteTexture;
        }

        public void LoadTexture2(Texture2D inSpriteTexture)
        {
            spriteTexture2 = inSpriteTexture;
        }

        public void SetRectangle(Rectangle inSpriteRectangle)
        {
            spriteRectangle = inSpriteRectangle;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D tekstura)
        {
            spriteBatch.Draw(tekstura, spriteRectangle, Color.White);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle teksturaRectangle, float przezroczystosc = 1.0f)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, teksturaRectangle, Color.White * przezroczystosc);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle teksturaRectangle, Texture2D tekstura, float przezroczystosc = 1.0f)
        {
            spriteBatch.Draw(tekstura, spriteRectangle, teksturaRectangle, Color.White * przezroczystosc);
        }
        public virtual void Draw(int x, SpriteBatch spriteBatch, Rectangle teksturaRectangle, float przezroczystosc = 1.0f)
        {
            spriteBatch.Draw(spriteTexture2, spriteRectangle, teksturaRectangle, Color.White * przezroczystosc);
        }
    }
}
