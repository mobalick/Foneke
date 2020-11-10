using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Extentions;
using FoNeke.Web.Models.Dto;
using FoNeke.Web.Models.Dto.Messages;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using MassTransit;
using WebSocketSharp;

namespace FoNeke.Services
{
    public class SMSService : ISMSService
    {
        private readonly GsmCommMain _comm;

        public SMSService(IBusControl bus)
        {
            IsBusy = true;
            var commPort = ConfigurationManager.AppSettings["COMPort"];
            _comm = new GsmCommMain(commPort, 115200, 300);
            OpenCom();
            IsBusy = false;
        }

        private void OpenCom()
        {
            try
            {
                _comm.Open();
                //_comm.MessageReceived += comm_MessageReceived;
                //comm_MessageReceived(_comm, null);
            }
            catch (Exception e)
            {
                _comm.Close();
                this.Log().Error(e);
                throw ;
            }
            

        }


        //private void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        //{
        //    try
        //    {
        //        var comm = (GsmCommMain)sender;

        //        string storage = e != null ? ((dynamic)e.IndicationObject).Storage : "SM";

        //        var messages = comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, storage).ToList();

        //        var list = messages.Select(m => new Sms
        //        {
        //            From = ((dynamic)m.Data).OriginatingAddress.ToString(),
        //            Text = m.Data.UserDataText,
        //            Date = ((dynamic)m.Data).SCTimestamp.ToDateTime()
        //        }).ToList();

        //        if (list.Any())
        //        {
        //            //SenToWebSocket(list);
        //            SenToBus(list);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        this.Log().Error("Erreur reception sms ", exception);
        //    }

        //    //DeleteSms();
        //}

        

        private static void SenToWebSocket(List<Sms> list)
        {
            var url = $"ws://{ConfigurationManager.AppSettings["HostServerName"]}:{ConfigurationManager.AppSettings["WebSocketPort"]}/{ConfigurationManager.AppSettings["WebSocketService"]}";

            using (var ws = new WebSocket(url))
            {
                ws.SetCredentials(ConfigurationManager.AppSettings["WebSocketUser"], ConfigurationManager.AppSettings["WebSocketPassword"], true);
                ws.Connect();

                var message = new WebSocketMessage<List<Sms>>
                {
                    MessageType = WebSocketMessageType.SmsReceived,
                    ApplicationName = ConfigurationManager.AppSettings["ApplicationName"],
                    MessageObject = list
                };

                ws.Send(message.ToJsonString());
                ws.Close();
            }
        }

        public string SendSms(Sms sms)
        {
            IsBusy = true;
            if (!_comm.IsConnected())
            {
                this.Log().Error($"Erreur envoi sms le device n'est pas connecté ");
                return "Phone is not connected";
            }

            if (!_comm.IsOpen())
            {
                _comm.Open();
            }

            return SendSms(sms.To, sms.Text);
        }

        public string SendSms(string number, string texte)
        {
            IsBusy = true;

            try
            {
                const byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
                var pdu = new SmsSubmitPdu(texte, number, dcs);
                _comm.SendMessage(pdu);
            }
            catch (Exception e)
            {
                this.Log().Error($"Erreur envoi sms to :{number} - text : {texte}",e);
                IsBusy = false;
                return "error";
            }

            IsBusy = false;
            return "success";
        }

        public List<Sms> ReadSms()
        {
            if (!_comm.IsConnected())
            {
                this.Log().Error($"Erreur envoi sms le device n'est pas connecté ");
            }
            if (!_comm.IsOpen())
            {
                _comm.Open();
            }

            var messages= _comm.ReadMessages(PhoneMessageStatus.All, "SM").ToList();
            
            return messages.Select(m => new Sms
            {
                From = ((dynamic)m.Data).OriginatingAddress.ToString(),
                Text = m.Data.UserDataText,
                Date = ((dynamic)m.Data).SCTimestamp.ToDateTime()
            }).ToList();
        }


        public void DeleteSms()
        {
            _comm.DeleteMessages(DeleteScope.ReadAndSent, "SM");
        }

        public bool IsBusy { get; set; }

        public void Dispose()
        {
            _comm?.Close();
        }
    }

}
