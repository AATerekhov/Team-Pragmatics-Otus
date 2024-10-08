﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.DataAccess.BusinessLogic.Models.Base
{
    public interface IModel<TId> 
        where TId : struct
    {
        public TId Id { get; init; }
    }
}
