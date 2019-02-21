﻿using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Recipient> Recipients { get; }
        IBaseRepository<Tariff> Tariffs { get; }
        int Save();
    }

}
