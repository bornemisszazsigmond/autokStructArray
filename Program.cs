using System;
using System.IO; //fájlbeolvasás
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autokStructArray
{
    class Program
    {
        struct auto
        {
            public string rendszam;
            public string tipus;
            public string szin;
            public DateTime uzembeHelyezes;
            public int ertek;
            public bool elsoTulaj;
            public string tulajdonos;
        }

        static void Main(string[] args)
        {
            auto[] kereskedes = new auto[100];

            //Fájlbeolvasás
            string[] temp = File.ReadAllLines("autok.csv");
            int N = temp.Length - 1;

            for (int i = 1; i < temp.Length; i++)
            {
                string[] temp2 = temp[i].Split(';');
                kereskedes[i-1].rendszam = temp2[0];
                kereskedes[i-1].tipus = temp2[1];
                kereskedes[i-1].szin = temp2[2];
                kereskedes[i-1].uzembeHelyezes = Convert.ToDateTime(temp2[3]);
                kereskedes[i-1].ertek = Convert.ToInt32(temp2[4]);
                kereskedes[i-1].elsoTulaj = Convert.ToBoolean(temp2[5]);
                kereskedes[i-1].tulajdonos = temp2[6];
            }

            Console.WriteLine("3. feladat");
            Console.WriteLine($"\tAutók száma: {N} db");

            Console.WriteLine("\n4. feladat");
            int opelDb = 0;
            double opelOsszesErtek = 0;
            for (int i = 0; i < N; i++)
            {
                if (kereskedes[i].tipus.Equals("Opel"))
                {
                    opelDb++;
                    opelOsszesErtek += kereskedes[i].ertek;
                }
            }
            Console.WriteLine($"\tOpel típusú autók száma: {opelDb} db");
            Console.WriteLine($"\tOpel típusú autók átlagos értéke: {opelOsszesErtek/opelDb:c0}");

            Console.WriteLine("\n5. feladat");
            DateTime most = DateTime.Now;
            int audiDb = 0;
            double audiOsszesEv = 0;
            for (int i = 0; i < N; i++)
            {
                if(kereskedes[i].tipus.Equals("Audi"))
                {
                    audiDb++;
                    audiOsszesEv += (int)((most - kereskedes[i].uzembeHelyezes).Days/365.0);
                }
            }
            Console.WriteLine($"\tAudi típusú gépkocsik átlagos életkora: {audiOsszesEv/audiDb:f2} év");

            Console.WriteLine("\n6. feladat");
            int egyTulajFo = 0;
            for (int i = 0; i < N; i++)
            {
                if (kereskedes[i].elsoTulaj) egyTulajFo++;
            }
            Console.WriteLine($"\tEgyetlen tulajdonosa {egyTulajFo} db, több tulajdonosa {N-egyTulajFo} db gépjárműnek volt.");
            if (egyTulajFo > N - egyTulajFo) Console.WriteLine("\tElső tulajdonos gépkocsiból van több az autókereskedésben.");
            else if(egyTulajFo < N - egyTulajFo) Console.WriteLine("\tTöbb tulajdonos gépkocsiból van több az autókereskedésben.");
            else Console.WriteLine("\tEgál.");

            //7. feladat
            StreamWriter sw = new StreamWriter("leertekeltSuzuki.csv",false,Encoding.UTF8);
            sw.WriteLine(temp[0]);
            for (int i = 0; i < N; i++)
            {
                if (kereskedes[i].tipus.Equals("Suzuki"))
                    sw.WriteLine($"{kereskedes[i].rendszam};{kereskedes[i].tipus};{kereskedes[i].szin};{kereskedes[i].uzembeHelyezes:d};{kereskedes[i].ertek*0.8};{kereskedes[i].elsoTulaj};{kereskedes[i].tulajdonos}");
            }
            sw.Close();

            //8. feladat
            StreamWriter sw1 = new StreamWriter("leertekeltAudi.csv", false, Encoding.UTF8);
            sw1.WriteLine(temp[0]);
            for (int i = 0; i < N; i++)
            {
                if (kereskedes[i].tipus.Equals("Audi"))
                    sw1.WriteLine($"{kereskedes[i].rendszam};{kereskedes[i].tipus};{kereskedes[i].szin};{kereskedes[i].uzembeHelyezes:d};{kereskedes[i].ertek * 0.82};{kereskedes[i].elsoTulaj};{kereskedes[i].tulajdonos}");
            }
            sw1.Close();

            Console.ReadKey();
        }
    }
}
