﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Application.Entities
{
    internal class Question : Message
    {
        public List<Answer> Answers { get; set; }
    }
}