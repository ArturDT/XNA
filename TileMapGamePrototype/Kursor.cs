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
    class Kursor : SpritePodstawowy
    {
        public Vector2 pozycjaKursora;
        Rectangle teksturaRec;
        String[] tekst = new String[16];
        private int podniesionoID = -1;

        public Kursor()
        {
            spriteRectangle.Width = 25;
            spriteRectangle.Height = 25;
            BinaryFormatter MyFormatter = new BinaryFormatter();
            FileStream MyStream = new FileStream("F:\\krasnal\\tekst\\pl\\tekst1.dat", FileMode.Open);
            tekst = (String[])MyFormatter.Deserialize(MyStream);
            MyStream.Close();
        }

        public void rysujKursor(SpriteBatch spriteBatch)
        {
            spriteRectangle.X = (int)pozycjaKursora.X;
            spriteRectangle.Y = (int)pozycjaKursora.Y;
            switch (podniesionoID)
            {
                case 1://trawa
                    teksturaRec = new Rectangle(200, 0, 100, 100);
                    break;
                default:
                    teksturaRec = new Rectangle(0, 0, 100, 100);
                    break;
            }
            Draw(spriteBatch, teksturaRec);
        }

        public void update(Game1 gra, MouseState mouseState)
        {
            wyswietlInfKafelek(gra, mouseState);
        }


        private bool wyswietlInfKafelek(Game1 gra, MouseState mouseState)
        {
            int rodzajPola = 0;
            for (int i = 0; i < 10; i++)
            {
                if (spriteRectangle.Intersects(Plecak.plecakEkran[i].rectangle) && Plecak.plecakEkran[i].iloscItemkow != 0)
                {
                    Game1.wiadomosc = tekst[Plecak.plecakEkran[i].IDItemu + 99];
                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        zabierzObiektZPlecaka(i);
                    }
                    return true;
                }
            }
            for (int i = 0; i < gra.mapa.maxObiekty; i++)
                if (spriteRectangle.Intersects(gra.mapa.obiekty[i].kwadrat))
                {
                    rodzajPola = gra.mapa.obiekty[i].numer;
                    if (rodzajPola != 0)
                    Game1.wiadomosc = tekst[rodzajPola];
                    return true;
                }

            ///*  //edycja pliku tekstowego
            BinaryFormatter MyFormatter = new BinaryFormatter();
            FileStream MyStream = new FileStream("F:\\krasnal\\tekst\\pl\\tekst1.dat", FileMode.Create);
            String[] tekst2 = { "Pusto", "skała", "Gleba", "kamień", "5", "6", "7", "8", "9", "10", 
                                "11", "12", "13", "14", "15", "16","17", "18", "19", "20",
                              "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", //30
                              "31", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                              "41", "2", "3", "4", "5", "6", "7", "8", "9", "10", //50
                              "51", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                              "61", "2", "3", "4", "5", "6", "7", "8", "9", "10", //70
                              "71", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                              "81", "2", "3", "4", "5", "6", "7", "8", "9", "10", //90
                              "91", "92", "93", "94", "95", "96", "97", "98", "99", "100",
                              "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", //110
                              "111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
                              "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", //130
                              "131", "132", "133", "4", "5", "6", "7", "8", "9", "10",
                              "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",}; //150
            MyFormatter.Serialize(MyStream, tekst2);
            MyStream.Close();
            // * */
            return false;
        }

        private void zabierzObiektZPlecaka(int i)
        {
            if (podniesionoID == -1)
            {
                podniesionoID = Plecak.plecakEkran[i].IDItemu;
                Plecak.plecakEkran[i].iloscItemkow--;
            }
        }

        private void odlozPrzedmiot(int i, Game1 gra)
        {

            if (MapaOperacje.mapa.tablicaKafli[gra.mapa.obiekty[i].ID, 5] == 0)
            {
                MapaOperacje.mapa.tablicaKafli[gra.mapa.obiekty[i].ID, 5] = podniesionoID;
            }
            if (MapaOperacje.mapa.tablicaKafli[gra.mapa.obiekty[i].ID, 5] == podniesionoID)
            {
                MapaOperacje.mapa.tablicaKafli[gra.mapa.obiekty[i].ID, 6]++;
                podniesionoID = -1;
            }
        }

    }
}
