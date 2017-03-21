using System;
using System.Collections.Generic;
using Cost_Management.Models;
using Cost_Management.Services;

using System.Web.Http;

namespace Cost_Management.Controllers
{
    public class BookLunchController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post(string form_id,string channel_id,string create_time,[FromBody]AppMetadata value)
        {
            AppRespones values = new AppRespones();


            try
            {
                MasterServices data = new MasterServices();
                if(value.FormName== "費用報銷表單")
                {
                    data.DBCreate_1(form_id, channel_id, create_time, value);
                }
                else
                {
                    data.DBCreate_2(form_id, channel_id, create_time, value);
                }
               
                values.AppStatus = "0000";
                values.ErrorContent = "成功";
                MasterServices.status = "";
                return Ok(values);
            }
            catch (Exception ex)
            {
                values.AppStatus = "9999";
                values.ErrorContent = MasterServices.status;
                MasterServices.status = "";
                return Ok(values);
                
            }

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}