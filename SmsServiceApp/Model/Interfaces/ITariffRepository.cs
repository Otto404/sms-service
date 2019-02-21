﻿using System.Collections.Generic;
using WebCustomerApp.Models;

namespace Model.Interfaces
{
	public interface ITariffRepository : IBaseRepository<Tariff>
	{
		void ChangeTariffLimit(Tariff currentTariff, int newLimit);
	}
        void ChangeTariffPricing(Tariff currentTariff, decimal newPrice, string userRole);
        void ChangeTariffLimit(Tariff currentTariff, int newLimit, string userRole);      
    }
}