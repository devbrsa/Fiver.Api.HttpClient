﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Fiver.Api.HttpClient;

namespace Fiver.Api.HttpClient.Client
{
    class Program
    {
        const string baseUri = "http://localhost:56942/movies";

        static System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();
        static HttpRequestFactory _httpRequestFactory;

        static void Main(string[] args)
        {
            _httpRequestFactory = new HttpRequestFactory(_httpClient);
            try
            {
                while (true)
                {
                    Console.Write("Enter 1:GET(list), 2:GET(item), 3:POST, 4:PUT, 5:PATCH, 6:DELETE, X:Exit ");
                    var option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            GetList().Wait();
                            break;

                        case "2":
                            GetItem().Wait();
                            break;

                        case "3":
                            Post().Wait();
                            break;

                        case "4":
                            Put().Wait();
                            break;

                        case "5":
                            Patch().Wait();
                            break;

                        case "6":
                            Delete().Wait();
                            break;

                        case "x":
                        case "X":
                            return;

                        default:
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static async Task GetList()
        {
            
            var requestUri = $"{baseUri}";
            var response = await _httpRequestFactory.Get(requestUri).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
            //Console.WriteLine(response.ContentAsString());
            var outputModel = response.ContentAsType<List<MovieOutputModel>>();
            outputModel.ForEach(item => 
                            Console.WriteLine("{0} - {1}", item.Id, item.Title));
        }

        private static async Task GetItem()
        {
            var requestUri = $"{baseUri}/1";
            var response = await _httpRequestFactory.Get(requestUri).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
            //Console.WriteLine(response.ContentAsString());
            var outputModel = response.ContentAsType<MovieOutputModel>();
            Console.WriteLine("{0} - {1}", outputModel.Id, outputModel.Title);
        }

        private static async Task Post()
        {
            var model = new MovieInputModel
            {
                Id = 4,
                Title = "Thunderball",
                ReleaseYear = 1965,
                Summary = "James Bond heads to The Bahamas to recover two nuclear warheads stolen by SPECTRE agent Emilio Largo in an international extortion scheme."
            };

            var requestUri = $"{baseUri}";
            var response = await _httpRequestFactory.Post(requestUri, model).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
            //Console.WriteLine(response.ContentAsString());
            var outputModel = response.ContentAsType<MovieOutputModel>();
            Console.WriteLine("{0} - {1}", outputModel.Id, outputModel.Title);
        }

        private static async Task Put()
        {
            var model = new MovieInputModel
            {
                Id = 4,
                Title = "Thunderball-Put",
                ReleaseYear = 1965,
                Summary = "James Bond heads to The Bahamas to recover two nuclear warheads stolen by SPECTRE agent Emilio Largo in an international extortion scheme."
            };

            var requestUri = $"{baseUri}/4";
            var response = await _httpRequestFactory.Put(requestUri, model).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
        }

        private static async Task Patch()
        {
            var model = new []
            {
                new 
                {
                    op = "replace",
                    path = "/title",
                    value = "Thunderball-Patch"
                }
            };

            var requestUri = $"{baseUri}/4";
            var response = await _httpRequestFactory.Patch(requestUri, model).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
        }

        private static async Task Delete()
        {
            var requestUri = $"{baseUri}/4";
            var response = await _httpRequestFactory.Delete(requestUri).ConfigureAwait(false);

            Console.WriteLine($"Status: {response.StatusCode}");
        }
    }
}