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
    public class StanGracza : SpritePodstawowy
    {
        public static int predkoscGracza = 4,
                          predkoscDomyslnaGracza = 4,
                          atakGracza = 1;

        private static int hpAktualneGracza = 100;

        public void update()
        {

        }

        public void rysujStany(SpriteBatch spriteBatch)
        {
            rysujHP(spriteBatch);
        }
        private void rysujHP(SpriteBatch spriteBatch)
        {
            spriteRectangle = new Rectangle(0, 0, Silnik.rozmiarKaflka, Silnik.rozmiarKaflka * 2);
            Rectangle teksturaRec = new Rectangle(1000, 0, 100, 200);
            switch ((int)(hpAktualneGracza / 10))
            {
                case 0:
                    teksturaRec = new Rectangle(1000, 0, 100, 200);
                    break;
                case 1:
                    teksturaRec = new Rectangle(900, 0, 100, 200);
                    break;
                case 2:
                    teksturaRec = new Rectangle(800, 0, 100, 200);
                    break;
                case 3:
                    teksturaRec = new Rectangle(700, 0, 100, 200);
                    break;
                case 4:
                    teksturaRec = new Rectangle(600, 0, 100, 200);
                    break;
                case 5:
                    teksturaRec = new Rectangle(500, 0, 100, 200);
                    break;
                case 6:
                    teksturaRec = new Rectangle(400, 0, 100, 200);
                    break;
                case 7:
                    teksturaRec = new Rectangle(300, 0, 100, 200);
                    break;
                case 8:
                    teksturaRec = new Rectangle(200, 0, 100, 200);
                    break;
                case 9:
                    teksturaRec = new Rectangle(100, 0, 100, 200);
                    break;
                case 10:
                    teksturaRec = new Rectangle(0, 0, 100, 200);
                    break;
            }
            Draw(spriteBatch, teksturaRec, 0.5f);
        }

    }
}
