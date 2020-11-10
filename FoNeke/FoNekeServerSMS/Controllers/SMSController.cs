using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoNekeServerSMS.Models;

namespace FoNekeServerSMS.Controllers
{
    public class SMSController : ApiController
    {
        // GET: api/SMS
        public IEnumerable<string> Get()
        {
            var comm = new GsmCommMain("COM5", 115200, 300);
            comm.ReadMessages(PhoneMessageStatus.All, "");

            return new string[] { "value1", "value2" };
        }

        // GET: api/SMS/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SMS
        [HttpPost]
        public void Post([FromBody]PostModel model)
        {
            var comm = new GsmCommMain("COM5", 115200, 300);  //9600
            try
            {
                comm.Open();
                

                comm.MessageReceived += comm_MessageReceived;


                byte dcs = (byte) DataCodingScheme.GeneralCoding.Alpha7BitDefault;


                var pdu = new SmsSubmitPdu(model.Texte, model.Number, dcs);
                comm.SendMessage(pdu);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
               // comm.Close();
            }



        }

        private void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var comm = (GsmCommMain) sender;

            var messages = comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, ((dynamic)e.IndicationObject).Storage);

            Console.WriteLine(e);
        }

        // PUT: api/SMS/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SMS/5
        public void Delete(int id)
        {
        }
    }
}
