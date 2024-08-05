using GeoMapSettings.Model;
using GeoMapSettings.Oms;
using Spectre.Console;
using System.Diagnostics;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;


namespace GeoMapSettings
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _client = new SimpleHttpClient("http://localhost:52199");
            var loader = new Loader(@"C:\Users\User\Downloads\leningrad_oblast-latest.osm.pbf");
            var command = string.Empty;
            while (command != "Выход")
            {
                command = DialogExtentions.WhatToDo();
                if (command == "Создать места")
                {
                    var places = loader.Load(DialogExtentions.SelectPlaceType(GetPlaceType().Result.ToList()));
                    DialogPlaceFunc(places, CreatePlace,"Создание");
                }
                else if (command == "Удалить все места")
                {
                    var selectType = DialogExtentions.SelectPlaceType(GetPlaceType().Result.ToList());
                    var places = GetPlase(selectType.Id).Result;
                    DialogPlaceFunc(places.ToList(), DeletePlace, "Удаление");
                }
            }                    
            
            Console.WriteLine("Окончание программы.");

            async Task DeletePlaceType()
            {
                int index = AnsiConsole.Ask<int>("Input [green]index[/]: ");
                await _client.DeleteAsync<PlaceType, int>("/api/PlaceType", index);
            }
            async Task CreateNewPlaceType()
            {
                var placeType = new PlaceType
                {
                    Name = AnsiConsole.Ask<string>("Input [green]name[/]: "),
                    Description = AnsiConsole.Ask<string>("Input [green]description[/]: ")
                };

                var response = await _client.PostAsync<PlaceType>("/api/PlaceType", placeType);
                Console.ReadLine();
            }
            async Task CreatePlace(Place place)
            {
                await _client.PostAsyncNotResult("/api/Place", place);
            }

            async Task ShowPlaceTypes()
            {
                var response = await _client.GetAsync<IEnumerable<PlaceType>>("/api/PlaceType");
                var table = new Table();
                table.AddColumn("Id");
                table.AddColumn(new TableColumn("Name").Centered());
                table.AddColumn(new TableColumn("Description").Centered());

                response.ToList().ForEach(p => table.AddRow(p.Id.ToString(), p.Name, p.Description));
                AnsiConsole.Write(table);
                Console.ReadLine();
            }

            async Task<IEnumerable<PlaceType>> GetPlaceType()
            {
                var response = await _client.GetAsync<IEnumerable<PlaceType>>("/api/PlaceType");
                return response;
            }

            async Task<IEnumerable<Place>> GetPlase(int placeTypeId)
            {
                var response = await _client.GetAsync<IEnumerable<Place>>($"/api/Place/{placeTypeId}");
                return response;
            }

            async Task DeletePlace(Place place)
            {
                await _client.DeleteAsync<Place,Guid>("/api/Place", place.Id);
            }

            void AddResultForPackage(List<Place> places, int count, Func<Place, Task> func) 
            {
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < places.Count; i++)
                {
                    tasks.Add(func(places[i]));
                    if (i != 0 && i % count == 0)
                    {
                        Task.WaitAll(tasks.ToArray());
                        tasks.ForEach(t => t.Dispose());
                        tasks.Clear();
                        Console.WriteLine($"Загружено {i} пакетов...");
                        //if (Сontinue()=="Нет") return;
                    }
                }
            }

            void DialogPlaceFunc(List<Place> places, Func<Place,Task> func, string comment = "Загрузка")
            {
                var number = DialogExtentions.WriteNumber();
                Console.WriteLine($"Будет отправлено {(int)places.Count / number} пакетов.");
                AddResultForPackage(places, number, func);
                Console.WriteLine($"{comment} завершено(а).");
            }
        }

    }
}
