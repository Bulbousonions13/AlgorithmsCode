using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace AlgorithmsCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "..\\..\\..\\ObjectFile.txt";
            CarFileUtility.Write(100,filePath);

            CarObject[] readArray = CarFileUtility.Read(100,filePath);

            Console.WriteLine( "Number of Loops : " + Search<CarObject>.Linear(readArray, 40000.00, 46000.00)); 

            //foreach(CarObject j in readArray){ j.toString(); }

            Console.ReadLine();
                        
        }
    }

    public static class Search<T> {

        public static int Linear(T[] array, double lowPrice, double highPrice) {

            int loops = 0;

            if (array.GetType() == typeof(CarObject[])) {

                CarObject[] castarray = array as CarObject[];

                for (int i = 0; i < array.Length; i++) {
                    loops++;
                    if (castarray[i].price >= lowPrice && castarray[i].price <= highPrice) {
                        Console.WriteLine( "First value in specified range found at array index : " + i + " --->"+castarray[i].price);
                        return loops;
                    }                    
                }
                Console.WriteLine( " No match found! ");
            }
            return loops;
        }
    }

    

    public static class CarFileUtility {

        public static void Write(int n, string filePath) {

            CarObject[] array = new CarObject[n];

            for (int i = 0; i < n; i++)
            {
                CarObject testObject = new CarObject();
                array[i] = testObject;               
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(array) );
        }

        public static CarObject[] Read(int n, string filePath) {

            StreamReader reader = new StreamReader(filePath);
            int counter = 0;
            CarObject[] array= new CarObject[n];
            while (!reader.EndOfStream && counter<n) {

                string s = "";
                if (reader.Peek() == '{')
                {
                    s += (char)reader.Read();
                    while (reader.Peek() != '}')
                    {
                        s += (char)reader.Read();
                    }
                    s += '}';                    
                    array[counter]= JsonConvert.DeserializeObject<CarObject>(s);
                    //array[counter].toString();
                    counter++;
                }
                else { reader.Read(); }
            }
            return array;
        }
    }

    
    public class CarObject
    {
        public double price;

        // list of car makers
        public enum Cars
        {
            Toyota, Honda, Acura, Mercedes, Ford, Volkswagon, Nissan, Porche, GMC, Fiat, MINI, Lexus, Chevy, Tesla, Bentley, BMW, Buick,
            Cadillac, Dodge, GM, RangeRover, Audi, Saturn, Kia, Hyundai, Lincoln, Mercury, Jeep, Jaguar, Subaru, EndOfList
        };

        public string brand;

        public CarObject()
        {
            price = (new Random().NextDouble() * 100000);                   // $100,000 max price
            price = Math.Round(price, 2, MidpointRounding.AwayFromZero);    //round to two cents round up at .005 and over
            int randPick = new Random().Next((int)Cars.EndOfList);          //pick a random car maker
            brand = ((Cars)randPick).ToString();                            //assign the brand name
        }

        public void toString()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Price : " + price);
            Console.WriteLine("brand : " + brand);
            Console.WriteLine("-----------------");
        }
    }
}
