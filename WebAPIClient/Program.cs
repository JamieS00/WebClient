using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient // Note: actual namespace depends on the project name.
{
    class Car
    {
        [JsonProperty("id")]
        public int Make_ID { get; set; }

        [JsonProperty("name")]
        public string Make_Name { get; set; }

        [JsonProperty("modelID")]
        public int Model_ID { get; set; }


        public List<Types> Types { get; set; }
    }
    public class Type
    {

        [JsonProperty("id")]
        public int Make_ID { get; set; }


        [JsonProperty("name")]
        public string Make_Name { get; set; }


        [JsonProperty("Model_ID")]
        public int Model_ID { get; set; }


    }

    public class Types
    {

        [JsonProperty("type")]
        public Type Type;

    }


    internal class Program
    {
        //so we can send/recieve Http responses from a resource identified by URL
        private static readonly HttpClient client = new HttpClient();

        //calls a new private method that return task ProcessRepo
        static async Task Main(string[] args)
        {
            await ProcessRepo();
        }

        /*Task is an object that reprents work that has to be completed(Tells you if its completed
         * & this method doesnt return a value/executes asynchronosly.
         * Async runs independently from main, which uses # to await and bind results to promise (Task)

        - ProcessRepo will run until the user presses enter w/o writing anything 
        */
        private static async Task ProcessRepo()
        {
            //try catch - an exception will be thrown if the user enters an incorrect car name
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter name, If you press Enter without writing the program will exit.");

                    var carName = Console.ReadLine();

                    if (string.IsNullOrEmpty(carName))
                    {
                        break;
                    }

                    /*if user enters correct name
                     - GetAsync: makes youra API calls w/ user input*/
                    var result = await client.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/474/modelyear/2015?format=json");
                                                    
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var car = JsonConvert.DeserializeObject<Car>(resultRead);

                    //display results 
                    Console.WriteLine("-------");
                    Console.WriteLine("Car #" + car.Make_ID);
                    Console.WriteLine("Name:" + car.Make_Name);
                    Console.WriteLine("Model ID:" + car.Model_ID);
                    Console.WriteLine("Type(s):");
                    car.Types.ForEach(t => Console.WriteLine(" " + t.Type.Make_Name));
                    Console.WriteLine("\n------");

                }
                //user eneter incorrect car name 
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid Car Name!");
                }

                


               
            }
                
        }



    }
}