/* Artur Dobrzanski */ 
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using Microsoft.Xna.Framework.Media;
using System;

namespace Krasnal
{

    public class MapaOperacje
    {
        #region zmienne
        public static MapaObiekt mapa = new MapaObiekt();
        private int maxY;
        private int maxX;
        public SpritePodstawowy kafelka;
        public SpritePodstawowy itemek;
        public SpritePodstawowy obiektNaKafelce;
        private Random rnd = new Random();
        private Boolean ruchGraczaX = false, ruchGraczaY = false;
        private int srodekX = mapa.X / 2;
        private int srodekY = mapa.Y / 2;
        public Obiekty[] obiekty;
        public int maxObiekty;
        private int krokiGraczaX = 0, krokiGraczaY = 0;
        #endregion

        public MapaOperacje()
        {
            maxY = Silnik.displayHeight / Silnik.rozmiarKaflka;
            maxX = Silnik.displayWidth / Silnik.rozmiarKaflka;
            kafelka = new SpritePodstawowy();
            itemek = new SpritePodstawowy();
            obiektNaKafelce = new SpritePodstawowy();
            maxObiekty = (maxY + 2) * (maxX + 2);
            obiekty = new Obiekty[maxObiekty];
            for (int i = 0; i < maxObiekty; i++)
                obiekty[i].kwadrat = new Rectangle();

        }

        private void zapiszMape()
        {
            //praca z plikiem - zapis mapy:
            if (!Directory.Exists("F:\\krasnal"))
            {
                Directory.CreateDirectory("F:\\krasnal");
            }
            BinaryFormatter MyFormatter = new BinaryFormatter();
            FileStream MyStream = new FileStream("F:\\krasnal\\maps\\mapa1.dat", FileMode.Create);
            MyFormatter.Serialize(MyStream, mapa);
            MyStream.Close();
        }

        public void wczytajMape(Game1 gra)
        {
            //praca z plikiem - odczyt mapy:
            BinaryFormatter MyFormatter = new BinaryFormatter();
            FileStream MyStream = new FileStream("F:\\krasnal\\maps\\mapa1.dat", FileMode.Open);
            mapa = (MapaObiekt)MyFormatter.Deserialize(MyStream);
            MyStream.Close();
            //koniec pracy z plikiem
        }

        public void rysujMape(SpriteBatch spriteBatch, int ileDodacDoX, int ileDodacDoY)
        {
            int ilePikseliMaJedenKafelekWSprite = Silnik.iloscPxTeksturaObiektMapa;
            Rectangle pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);//(PozycjaX, PozycjaY, wielkośćKlatek, wielkośćKlatek);
            int x = 0, y = 0;
            for (int i = srodekX - 1; i < srodekX + maxX + 1; i++)
            {
                for (int j = srodekY - 1; j < srodekY + maxY + 1; j++)
                {
                    ladujObiekty(i, j, x, y, ileDodacDoX, ileDodacDoY);
                    rysujPodlozeMapy(i, j, spriteBatch, ilePikseliMaJedenKafelekWSprite);
                    rysujObiektyNaMapie(i, j, ilePikseliMaJedenKafelekWSprite, spriteBatch);
                    y++;
                }
                y = 0;
                x++;
            }
        }

        private void rysujObiektyNaMapie(int i, int j, int ilePikseliMaJedenKafelekWSprite, SpriteBatch spriteBatch)
        {
            Rectangle pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
            bool rysuj = true;
            switch (mapa.tablicaKafli[i * mapa.Y + j, 2])
            {
                case -1: //nic
                    pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    rysuj = false;
                    break;
                case 0: //kamyk do przesuwania
                    pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    break;
                default:
                    rysuj = false;
                    pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    break;
            }
            if (rysuj == true) kafelka.Draw(2, spriteBatch, pozycja);
        }

        private void rysujPodlozeMapy(int i, int j, SpriteBatch spriteBatch, int ilePikseliMaJedenKafelekWSprite)
        {
            Rectangle pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
            switch (mapa.tablicaKafli[i * mapa.Y + j, 0])
            {
                case 0://nic
                    pozycja = new Rectangle(0, 0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    break;
                case 1://Ogranicznik - głaz , skała
                    pozycja = new Rectangle(0, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    break;
                case 2://ziemia do zebrania
                    pozycja = new Rectangle(0, ilePikseliMaJedenKafelekWSprite * 2, ilePikseliMaJedenKafelekWSprite, ilePikseliMaJedenKafelekWSprite);
                    break;
            }
            kafelka.Draw(spriteBatch, pozycja);
        }

        private void ladujObiekty(int i, int j, int x, int y, int ileDodacDoX, int ileDodacDoY)
        {
            int pozXEkran = ((i - (int)srodekX) * Silnik.rozmiarKaflka) + ileDodacDoX;
            int pozYEkran = ((j - (int)srodekY) * Silnik.rozmiarKaflka) + ileDodacDoY;
            Rectangle pozycjaNaEkranie = new Rectangle(pozXEkran, pozYEkran, Silnik.rozmiarKaflka, Silnik.rozmiarKaflka);
            Rectangle pozycjaNaEkranie2 = new Rectangle(pozXEkran, pozYEkran, Silnik.rozmiarKaflka / 2, Silnik.rozmiarKaflka / 2);
            kafelka.SetRectangle(pozycjaNaEkranie);
            itemek.SetRectangle(pozycjaNaEkranie2);
            obiekty[x * (maxY + 2) + y].kwadrat = pozycjaNaEkranie;
            if (mapa.tablicaKafli[i * mapa.Y + j, 2] == -1)
                obiekty[x * (maxY + 2) + y].numer = mapa.tablicaKafli[i * mapa.Y + j, 0];
            else obiekty[x * (maxY + 2) + y].numer = mapa.tablicaKafli[i * mapa.Y + j, 2] + 3;
            obiekty[x * (maxY + 2) + y].ID = i * mapa.Y + j;
            obiekty[x * (maxY + 2) + y].IDitem = mapa.tablicaKafli[i * mapa.Y + j, 5];

        }

        public void Update(Player gracz, int keyCode)
        {
            wykonajRuch(gracz, keyCode);
        }


        private void wykonajRuch(Player gracz, int keyCode)
        {
            switch (keyCode)
            {
                case 1://lewo
                    if (sprawdzKolizje(1, gracz.spriteRectangle) == false) ruchWLewo(gracz);
                    break;
                case 2://prawo
                    if (sprawdzKolizje(2, gracz.spriteRectangle) == false) ruchWPrawo(gracz);
                    break;
                case 3://gora
                    if (sprawdzKolizje(3, gracz.spriteRectangle) == false) ruchWGore(gracz);
                    break;
                case 4://dol
                    if (sprawdzKolizje(4, gracz.spriteRectangle) == false) ruchWDol(gracz);
                    break;
                case 5://gora lewo
                    if (sprawdzKolizje(5, gracz.spriteRectangle) == false)
                    {
                        ruchWGore(gracz);
                        ruchWLewo(gracz);
                    }
                    break;
                case 6://gora prawo
                    if (sprawdzKolizje(6, gracz.spriteRectangle) == false)
                    {
                        ruchWGore(gracz);
                        ruchWPrawo(gracz);
                    }
                    break;
              
            }
            if (sprawdzKolizje(4, gracz.spriteRectangle) == false) ruchWDol(gracz); //grawitacja
        }

        private Boolean sprawdzKolizje(int idRuchu, Rectangle gracz)
        {
            Rectangle kwad = gracz;
            kwad = new Rectangle(gracz.X, gracz.Y + (Silnik.rozmiarKaflka - (Silnik.rozmiarKaflka / 4)), gracz.Width, gracz.Height / 4);
            switch (idRuchu)
            {
                case 1:
                    kwad.X -= StanGracza.predkoscGracza;
                    break;
                case 2:
                    kwad.X += StanGracza.predkoscGracza;
                    break;
                case 3:
                    kwad.Y -= StanGracza.predkoscGracza;
                    break;
                case 4:
                    kwad.Y += StanGracza.predkoscGracza;
                    break;
                case 5:
                    kwad.Y -= StanGracza.predkoscGracza;
                    kwad.X -= StanGracza.predkoscGracza;
                    break;
                case 6:
                    kwad.Y -= StanGracza.predkoscGracza;
                    kwad.X += StanGracza.predkoscGracza;
                    break;
                case 7:
                    kwad.Y += StanGracza.predkoscGracza;
                    kwad.X -= StanGracza.predkoscGracza;
                    break;
                case 8:
                    kwad.Y += StanGracza.predkoscGracza;
                    kwad.X += StanGracza.predkoscGracza;
                    break;
            }
            StanGracza.predkoscGracza = StanGracza.predkoscDomyslnaGracza;
            for (int i = 0; i < maxObiekty; i++)
                if (kwad.Intersects(obiekty[i].kwadrat) && obiekty[i].numer != 0)
                {
                    return sprawdzRodzajPrzeszkody(i);
                }
            return false;
        }

        private Boolean sprawdzRodzajPrzeszkody(int i)
        {
            switch (obiekty[i].numer)
            {
                case 0://tło - nic
                    return false;
                case 1://kamien blokujacy przejscie, skala
                    return true;
                case 2://ziemia do zjadania
                    return true;
                case 3://kamien do przesuwania
                    return true;
            }
            return false;
        }

        private void ruchWLewo(Player gracz)
        {
            if (ruchGraczaX == true)
            {
                if (this.krokiGraczaX > -Silnik.displayWidth / 2 - 10)//polowa szerokosci ekranu + 1
                {
                    gracz.ruchWLewo();
                    this.krokiGraczaX -= StanGracza.predkoscGracza;
                    if (krokiGraczaX < 3 && krokiGraczaX > -3)
                    {
                        ruchGraczaX = false;
                        krokiGraczaX = 0;
                        if (krokiGraczaY == 0) gracz.wysrodkujGracza();
                    }
                }
            }
            else
            {
                Game1.ileDodacDoX += StanGracza.predkoscGracza;
                if (Game1.ileDodacDoX >= Silnik.rozmiarKaflka)
                {
                    Game1.ileDodacDoX = 0;
                    srodekX--;
                }
            }
        }

        private void ruchWPrawo(Player gracz)
        {
            if (ruchGraczaX == true)
            {
                if (this.krokiGraczaX < Silnik.displayWidth / 2 - Silnik.rozmiarKaflka + 10)
                {
                    gracz.ruchWPrawo();
                    this.krokiGraczaX += StanGracza.predkoscGracza;
                    if (krokiGraczaX < 3 && krokiGraczaX > -3)
                    {
                        ruchGraczaX = false;
                        krokiGraczaX = 0;
                        if (krokiGraczaY == 0) gracz.wysrodkujGracza();
                    }
                }
            }
            else
            {
                Game1.ileDodacDoX -= StanGracza.predkoscGracza;
                if (Game1.ileDodacDoX <= -Silnik.rozmiarKaflka)
                {
                    Game1.ileDodacDoX = 0;
                    srodekX++;
                }
            }
        }

        private void ruchWGore(Player gracz)
        {
            if (ruchGraczaY == true)
            {
                if (this.krokiGraczaY > -Silnik.displayHeight / 2)
                {
                    gracz.ruchWGore();
                    this.krokiGraczaY -= StanGracza.predkoscGracza * 2;
                    if (krokiGraczaY < 3 && krokiGraczaY > -3)
                    {
                        krokiGraczaY = 0;
                        ruchGraczaY = false;
                        if (krokiGraczaX == 0) gracz.wysrodkujGracza();
                    }
                }
            }
            else
            {
                Game1.ileDodacDoY += StanGracza.predkoscGracza * 2;
                if (Game1.ileDodacDoY >= Silnik.rozmiarKaflka)
                {
                    Game1.ileDodacDoY = 0;
                    srodekY--;
                }
            }
        }

        private void ruchWDol(Player gracz)
        {
            if (ruchGraczaY == true)
            {
                if (this.krokiGraczaY < Silnik.displayHeight / 2 - Silnik.rozmiarKaflka)
                {
                    gracz.ruchWDol();
                    this.krokiGraczaY += StanGracza.predkoscGracza;
                    if (krokiGraczaY < 3 && krokiGraczaY > -3)
                    {
                        krokiGraczaY = 0;
                        ruchGraczaY = false;
                        if (krokiGraczaX == 0) gracz.wysrodkujGracza();
                    }
                }
            }
            else
            {
                Game1.ileDodacDoY -= StanGracza.predkoscGracza;
                if (Game1.ileDodacDoY <= -Silnik.rozmiarKaflka)
                {
                    Game1.ileDodacDoY = 0;
                    srodekY++;;
                }
            }
        }


    }//koniec klasy
}
