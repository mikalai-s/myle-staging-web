using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyleWeb.Controllers
{
    public class FilesController : ApiController
    {
        private static TraceSource TraceSource = new TraceSource("FilesController");

        //
        // Post: /Files/
        public HttpResponseMessage Post([FromUri]string filename)
        {
            var task = this.Request.Content.ReadAsStreamAsync();
            task.Wait();
            var requestStream = task.Result;

            try
            {
                var dir = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), DateTime.UtcNow.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(dir);
                var fileStream = File.Create(Path.Combine(dir, DateTime.UtcNow.ToString("HH-mm-ss") + ".caf"));

                requestStream.CopyTo(fileStream);
                fileStream.Close();
                requestStream.Close();
            }
            catch (Exception e)
            {
                TraceSource.TraceData(TraceEventType.Error, 0, e.ToString());
                throw e;//new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
	}
}