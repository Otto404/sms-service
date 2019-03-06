﻿using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.RecipientViewModels;

namespace BAL.Managers
{
    public interface IRecipientManager
    {
        IEnumerable<RecipientViewModel> GetRecipients(int companyId);
        RecipientViewModel GetRecipientById(int id);
        void Insert(RecipientViewModel item, int companyId);
        void Update(RecipientViewModel item);
        void Delete(int id);
    }
}