﻿using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public interface ICompanyManager
    {
        IEnumerable<Company> GetCompanies();
    }
}
