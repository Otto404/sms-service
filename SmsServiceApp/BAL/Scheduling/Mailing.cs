using System;
using System.Threading.Tasks;
using Model.Interfaces;
using AutoMapper;
using WebCustomerApp.Services;
using Model.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace BAL.Scheduling
{
    public class Mailing
    {
        private readonly IMailingManager mailingManager;
        private readonly IMapper mapper;

        public Mailing(IMailingManager mailingManager, IMapper mapper)
        {
            this.mailingManager = mailingManager;
            this.mapper = mapper;
        }

        public async Task Execute()
        {
            var result = await mailingManager.GetUnsentMessages();
            if (!result.Any())
                return;
            try
            {
                await SendMessages(result);
            }
            catch
            {
                return;
            }
            await mailingManager.MarkAsSent(result);
        }

        private async Task SendMessages(IEnumerable<MessageDTO> messages)
        {
            SmsSender sms = new SmsSender();
            if (sms.Connect())
            {
                if (sms.OpenSession())
                {
                    sms.SendMessages(messages);
                    if (sms.CloseSession())
                    {
                        sms.Disconnect();
                        Console.WriteLine("Connection close");
                    }
                    else
                        Console.WriteLine("Could not close session");
                }
                else
                {
                    Console.WriteLine("Session error");
                }
            }
            else
            {
                Console.WriteLine("Connection error");
            }
        }
    }
}
