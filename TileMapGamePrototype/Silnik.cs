/* Artur Dobrzanski */ 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using Microsoft.Xna.Framework.Media;

namespace Krasnal
{
    public struct Obiekty
    {
        public Rectangle kwadrat;
        public int numer, ID;
        public int IDitem;
    }

    public struct plecakNaEkranie
    {
        public int IDSlotu,
                   IDItemu,
                   iloscItemkow;
        public Rectangle rectangle;
    }

    public class Silnik
    {
        //zmienne opisujące wyświetlanie
        public static int displayWidth = 1200,
                          displayHeight = 600,
                          rozmiarKaflka = 50,
                          iloscPxTeksturaObiektMapa = 100,
                          iloscPxTexturaPlecak = 100;
        public static SpriteFont font;

        //zmienne pokazujące aktualny stan swiata:
        public static int zebranoInd = -1;
    }
}
