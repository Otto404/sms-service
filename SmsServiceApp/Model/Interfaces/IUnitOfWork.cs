﻿using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Recipient> Recipients { get; }
        IBaseRepository<Company> Companies { get; }
        UserManager<ApplicationUser> Users { get; }
        IBaseRepository<StopWord> StopWords { get; }
        IBaseRepository<Operator> Operators { get; }
        int Save();
    }
}
