using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARFUELPRICEPROJECT
{

    enum FUELTYPE
    {
        AI92 = 100, AI95 = 200, AI98 = 230, DIESEL = 80, LIQUDGAZ = 40
    }


    class carrequest   //Base class
    {
        //ALLFIELDS
        string fueltype;
        string gearbox;
        string mood;
        double motor;
        double normalemsalgore_L = 7.5;
        string where_car_dirven;
        double km;
        double nowcapacity;


        //ALLPROPERTY

        public double Km
        {
            get { return km; }
            set { km = value; }
        }
        public double Nowcapacity
        {
            get { return nowcapacity; }
            set { nowcapacity = value; }
        }
        public string Fueltype
        {
            get { return fueltype; }
            set { fueltype = value; }
        }
        public string Gearbox
        {
            get { return gearbox; }
            set { gearbox = value; }
        }
        public string Mood
        {
            get { return mood; }
            set { mood = value; }
        }

        public double Motor
        {
            get { return motor; }
            set { motor = value; }
        }
        public double Normalemsalgore_L
        {
            get { return normalemsalgore_L; }
            set { normalemsalgore_L = value; }
        }
        public string Where_car_dirven
        {
            get { return where_car_dirven; }
            set { where_car_dirven = value; }
        }

    }


    class calculatefuel : carrequest
    {



        public static double motor(double motor)  //Method  (1.8-2.0 cox zeif , 2.0-2.5 orta , 2,5-3.0 guclu,3.0-4.4 cox guclu)
        {
            if (motor >= 1.8 && motor <= 2.0)
            {
                return 0.75; //bunlar emsallardir . normal serfiyyat 1 dir 
            }
            else if (motor > 2.0 && motor <= 2.5)
            {
                return 1.2;
            }
            else if (motor > 2.5 && motor <= 3.0)
            {
                return 1.5;
            }
            else if (motor > 3 && motor <= 4.4)

            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public static double mood(string mood)  //Burada 3 cur avtomobil vardir ki  (econom,comfort(business),sport,sport+)
        {



            if (mood == "econom")
            {
                return 0.9;
            }
            else if (mood == "comfort")
            {
                return 1.1;
            }
            else if (mood == "sport")
            {
                return 1.4;
            }
            else if (mood == "sport+")
            {
                return 1.8;
            }
            else
            {
                return 0;
            }
        }


        public static double gearbox(string gearbox)   //Avtomobili mexanika suretler qutusu ile idare etdiyimiz zaman benzin serfiyyati texmini 15 % azalir
        {
            if (gearbox == "mexanika")
            {
                return 0.75;
            }
            else if (gearbox == "avtomat")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static double price(string fueltype)
        {
            switch (fueltype)
            {
                case "92":
                    return (int)FUELTYPE.AI92;
                case "premium":
                    return (int)FUELTYPE.AI95;
                case "dizel":
                    return (int)FUELTYPE.DIESEL;
                case "super":
                    return (int)FUELTYPE.AI98;
                case "qaz":
                    return (int)FUELTYPE.LIQUDGAZ;
                default: return 0;
            }
        }

    }


    internal class Program
    {

        static void Main(string[] args)
        {
            bool SAME = true;
            carrequest val = new calculatefuel(); //polimorphsm

            Console.WriteLine("Zehmet olmasa Secim edeceyiniz avtomibilin gostericilerini secin!");
            Console.WriteLine();
            Console.WriteLine("Ilk once motorun gucu ne qeder olsun? ");


            Console.WriteLine("(1.8-2.0 cox zeif , 2.0-2.5 orta , 2,5-3.0 guclu,3.0-4.4 cox guclu)");
            val.Motor = double.Parse(Console.ReadLine()); //Mator
            Console.WriteLine("Avtomobilinizi hansi moodda suresiz? (econom,comfort,sport,sport+)");
            val.Mood = Console.ReadLine(); //Mood
            Console.WriteLine("avtomobilde suretler qutusu nedir? (mexanika,avtomat)");
            val.Gearbox = Console.ReadLine();    //Gearbox
            Console.WriteLine("Hansi yanacaq novunden istifade edirsiz? (92,premium,super,qaz,dizel)");
            val.Fueltype = Console.ReadLine();
            Console.WriteLine("avtomobilinizi esasen harada idare edesiz?(seherdaxili,seherxarici)");
            val.Where_car_dirven = Console.ReadLine();
            Console.WriteLine("Nece km yol gedesiz?(reqemle)");
            val.Km = double.Parse(Console.ReadLine());
            Console.WriteLine("avtomobilinzde nece L yanacaq qalib?");
            val.Nowcapacity = double.Parse(Console.ReadLine());

            double verilenleregore_L = calculatefuel.mood(val.Mood) * calculatefuel.gearbox(val.Gearbox) * calculatefuel.motor(val.Motor) * val.Normalemsalgore_L;
            double price = (calculatefuel.price(val.Fueltype) * verilenleregore_L) / 100;
            double aftercapacity = val.Nowcapacity - (val.Km * verilenleregore_L / 100);
            double usedcapacity = val.Km * verilenleregore_L / 100;

            if (calculatefuel.mood(val.Mood) == 0 || calculatefuel.gearbox(val.Gearbox) == 0 || calculatefuel.mood(val.Mood) == 0)
            {
                SAME = true;
            }
            else
            {
                SAME = false;
            }
            if (SAME == true)
            {
                Console.WriteLine("sehv daxil etmisiz,zehmet olmasa melumatlari yoxlayin!");
            }
            else
            {
                if (aftercapacity >= 0)
                {
                    Console.WriteLine("GEDECEYINIZ MENTEQEYE CATANDAN SONRA " + aftercapacity + " L YANACAQ QALACAQDIR!");

                }
                else
                {
                    Console.WriteLine("GEDECEYINIZ MENTEQEYE QEDER YANACAQ-DOLDURMA MNETEQESINE YAXINLASMALISIZ!");
                }
                Console.WriteLine(usedcapacity + " L YANACAQ ISTIFADE EDECEKSIZ GEDECEYINIZ YERE QEDER!");
                Console.WriteLine("SIZIN HER 100 KM ISTIFADE ETDIYINIZ YANACAQ " + verilenleregore_L + " L -DIR.");
                Console.WriteLine("HER 100 KM-E " + price + " AZN VESAIT XERCLENIR");
            }



            Console.ReadLine();
        }
    }
}