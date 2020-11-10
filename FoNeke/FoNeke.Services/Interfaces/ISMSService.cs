using System;
using System.Collections.Generic;
using FoNeke.Web.Models.Dto;

namespace FoNeke.Services
{
   public interface ISMSService : IDisposable
    {
        string SendSms(Sms sms);

        string SendSms(string numba, string texte);

        List<Sms> ReadSms();

        void DeleteSms();
        bool IsBusy { get; set; }
    }
}