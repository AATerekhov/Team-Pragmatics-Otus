using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Application.Interfaces
{
    public interface IInform
    {
        string Title { get; set; }
        void Send();
    }
}
