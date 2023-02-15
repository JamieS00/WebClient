using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebClient // Note: actual namespace depends on the project name.
{
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
                    Console.WriteLine("Enter name. If you press Enter without writing the program will exit.");

                    var carname = Console.ReadLine();

                    if (string.IsNullOrEmpty(carname))
                    {
                        break;
                    }
                }
                //user eneter incorrect car name 
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid Car Name!");
                }

                //user enters correct name
                var result = await client.GetAsync("https://vpic.nhtsa.dot.gov/api/");
                var resultRead = await result.Content.ReadAsByteArrayAsync();


               
            }
                
        }



    }
}