using GeoMapSettings.Access;
using GeoMapSettings.Model;
using GeoMapSettings.Oms;
using Spectre.Console;
using System.Diagnostics;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;


namespace GeoMapSettings
{
    public class Program
    {
        static ControllingFlow controllingFlow;
        static void Main(string[] args)
        {
            var _client = new SimpleHttpClient("http://localhost:52199");
            controllingFlow = new ControllingFlow();
            var loader = new Loader(@"C:\Users\User\Downloads\leningrad_oblast-latest.osm.pbf");
            var command = string.Empty;
            while (command != "Выход")
            {
                command = DialogExtentions.WhatToDo();
                if (command == "Создать места")
                {
                    var places = loader.Load(DialogExtentions.SelectPlaceType(GetPlaceType().Result.ToList()));
                    controllingFlow.SetAccess(new CreaterAccess(_client));
                    DialogPlaceFunc(places,"Создание");
                }
                else if (command == "Удалить все места")
                {
                    var selectType = DialogExtentions.SelectPlaceType(GetPlaceType().Result.ToList());
                    var places = GetPlase(selectType.Id).Result;
                    controllingFlow.SetAccess(new DeletedAccess(_client));
                    DialogPlaceFunc(places.ToList(), "Удаление");
                }
            }                    
            
            Console.WriteLine("Окончание программы.");

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

            void DialogPlaceFunc(List<Place> places, string comment = "Загрузка")
            {
                var number = DialogExtentions.WriteNumber();
                if (number != 0)
                {
                    controllingFlow.SetSemathore(number);
                    List<Task> tasks = new List<Task>();
                    places.ForEach(p => tasks.Add(controllingFlow.MakeStrategi(p)));
                    Task.WaitAll(tasks.ToArray());//Ожидаем выполнение всех транзакций.
                    Console.WriteLine($"{comment} завершено(а).");
                }
            }
        }

    }
}
