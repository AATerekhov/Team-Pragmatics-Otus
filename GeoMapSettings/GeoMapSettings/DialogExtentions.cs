using GeoMapSettings.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings
{
    public static class DialogExtentions
    {
        public static string WhatToDo()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(new[] { "Создать места", "Удалить все места", "Выход" }));
        }

        public static PlaceType SelectPlaceType(List<PlaceType> placeTypes)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<PlaceType>().AddChoices(placeTypes));
        }
        public static string Сontinue()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(new[] { "Да", "Нет" }));
        }
        public static int WriteNumber() 
        {
            return AnsiConsole.Prompt(
             new TextPrompt<int>("Сколько [green]запросов[/] отправлять одновременно.")
            .PromptStyle("green")
            .ValidationErrorMessage("Введите [red]кол-во запросов[/]")
            .Validate(number =>
            {
                return number switch
                {
                    <= 0 => ValidationResult.Error("[red]Колличество одновременно обрабатываемых запросов не может быть отрицательным.[/]"),
                    >= 50 => ValidationResult.Error("[red]Пакет одновременного выполнения слишком большой.[/]"),
                    _ => ValidationResult.Success(),
                };
            }));
        }
    }
}
