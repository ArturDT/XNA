/* Artur Dobrzanski */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krasnal
{
    [Serializable]
    public class MapaObiekt
    { //złe wyswietlanie (jeden kafelek za wysoko) do tego dodac trzeba zycai oraz ilsoc - pelna implementacja
        public int X = 100; //ilosc elementow na osi X
        public int Y = 100; // ilosc elementow na osi Y
        public int rozmiarMapy = 10000;
        public String nazwaMapy = "mapa1";
        public String opisMapy = "mapa testowa";
        public int[,] tablicaKafli = new int[10000, 7];
        //tablicaKafli[i, 0] - ID KAFLA         --- oddzielic tekstury kafli od tekstur obiektow!
        //tablicaKafli[i, 1] - HP OBIEKTU
        //tablicaKafli[i, 2] - ID OBIEKTU       --- oddzielic tekstury od obiektow!
        //tablicaKafli[i, 3] - ILOSC OBIEKTOW
        //------------------------------------
        //tablicaKafli[i, 4] - Dojrzalosc obiektu (dla np jagod aby +1 jagode dac)
        //tablicaKafli[i, 5] - ID itemu na ziemi
        //tablicaKafli[i, 6] - ilosc itemow na ziemi
    }
}
