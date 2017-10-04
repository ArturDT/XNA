/* Artur Dobrzanski */ 
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace Krasnal
{

    class Plecak : SpritePodstawowy
    {
        public static plecakNaEkranie[] plecakEkran = new plecakNaEkranie[10];
        public Plecak()
        {
            spriteRectangle.Width = Silnik.rozmiarKaflka;
            spriteRectangle.Height = Silnik.rozmiarKaflka;
            for (int i = 0; i < 10; i++)
            {
                plecakEkran[i].IDSlotu = i;
                plecakEkran[i].IDItemu = -1;
                plecakEkran[i].iloscItemkow = 0;
            }
        }
        public void rysujPlecak(SpriteBatch spriteBatch)
        {
            Rectangle teksturaRec = new Rectangle(0, 0, 100, 100);
            int ilePxMa1KafWSpr = Silnik.iloscPxTexturaPlecak;
            for (int i = 0; i < 10; i++)//rysujemy 10 kontenerow (plecak na ekranie)
            {
                teksturaRec = new Rectangle(0, 0, ilePxMa1KafWSpr, ilePxMa1KafWSpr);
                spriteRectangle.X = ((Silnik.displayWidth - (10 * Silnik.rozmiarKaflka)) / 2) + i * Silnik.rozmiarKaflka;
                spriteRectangle.Y = Silnik.displayHeight - Silnik.rozmiarKaflka - 10;
                Draw(spriteBatch, teksturaRec, 0.5f);
                plecakEkran[i].rectangle = spriteRectangle;
                //nakladamy na odpowiednie kontenery tekstury po ID przedmiotow i wypisujemy ich ilosc:
                int z = plecakEkran[i].iloscItemkow - 1;
                if (z != -1)
                {
                    switch (plecakEkran[i].IDItemu)
                    {
                        case 1://trawa
                            teksturaRec = new Rectangle(ilePxMa1KafWSpr, 0, ilePxMa1KafWSpr, ilePxMa1KafWSpr);
                            Draw(spriteBatch, teksturaRec);
                            wyswietlIloscPrzedmiotow(spriteBatch, i);
                            break;
                    }
                }
                else
                {
                    plecakEkran[i].IDItemu = -1;
                }
            }

        }

        private void wyswietlIloscPrzedmiotow(SpriteBatch spriteBatch, int i)
        {
            Vector2 textVector = new Vector2(spriteRectangle.X, spriteRectangle.Y);
            spriteBatch.DrawString(Silnik.font, plecakEkran[i].iloscItemkow.ToString(), textVector, Color.Black);
        }

        public void update()
        {
            if (Silnik.zebranoInd != -1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (plecakEkran[i].IDItemu == Silnik.zebranoInd)
                    {
                        plecakEkran[i].iloscItemkow++;
                        Silnik.zebranoInd = -1;
                        break;
                    }
                }
                if (Silnik.zebranoInd != -1)
                    for (int i = 0; i < 10; i++)
                    {
                        if (plecakEkran[i].IDItemu == -1)
                        {
                            plecakEkran[i].IDItemu = Silnik.zebranoInd;
                            plecakEkran[i].iloscItemkow++;
                            break;
                        }
                    }
                Silnik.zebranoInd = -1;

            }

        }



    }
}
