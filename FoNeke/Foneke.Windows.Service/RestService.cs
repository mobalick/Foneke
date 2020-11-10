using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FoNeke.Services;
using FoNeke.Services.Interfaces;
using FoNeke.Web.Models.Dto;
using FoNeke.Web.Repositories.Interfaces;
using Microsoft.Owin;
using Owin;
using ServiceStack;
using MassTransit;
using FoNeke.Web.Models.Dto.Messages;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Extentions;

namespace Foneke.Windows.SmsServer
{
    public class RestService
    {
        private readonly WebSocket _ws;
        private readonly ISMSService _smsService;
        public IBusControl _bus { get; set; }


        public RestService(ISMSService smsService, IDeviceMakeRepository deviceMakeRepository, IBusControl bus)
        {
            _bus = bus;
                        
            _smsService = smsService;

            //var url = $"ws://{ConfigurationManager.AppSettings["HostServerName"]}:{ConfigurationManager.AppSettings["WebSocketPort"]}/{ConfigurationManager.AppSettings["WebSocketService"]}";
            //_ws = new WebSocket(url);
            //_ws.OnMessage += _ws_OnMessage;
            //_ws.OnError += _ws_OnError;

            //_ws.SetCredentials(ConfigurationManager.AppSettings["WebSocketUser"], ConfigurationManager.AppSettings["WebSocketPassword"], true);
            //_ws.Connect();
        }

        private void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            this.Log().Info($"Message Recieved from webSocket : {e.Data.ToJsonString()}");


            var message = e.Data.FromJson<WebSocketMessage<SmsModel>>();
            if (message.MessageType == WebSocketMessageType.SendSms)
            {
                var smsModel =  message.MessageObject;
                _smsService.SendSms(smsModel.Number, smsModel.Text);
            }

            if (message.MessageType == WebSocketMessageType.DeleteSms)
            {
                _smsService.DeleteSms();
            }

        }

        public void Start()
        {
            this.Log().Info("Foneke.Windows.Service Started.");
            
        }

        private void _ws_OnError(object sender, ErrorEventArgs e)
        {
            this.Log().Error("Error in webSocket. ",e.Exception);
        }

        public void Stop()
        {
            this.Log().Info("Foneke.Windows.Service Stoped.");

            _ws?.Close();
            (_ws as IDisposable)?.Dispose();
        }
    }


   
}
