using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Application.Entities
{
    public abstract class AbstractMessage : IInform
    {
        public string Title { get; set; }

        public abstract void Send();
    }
}
