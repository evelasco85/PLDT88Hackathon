﻿using Pldt.Browser.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Pldt.Browser.Api.Controllers
{
    public class LatencyController : ApiController
    {
        [HttpGet]
        public async Task<JsonResult<LatencyViewModel>> Get(string urlOrIp)
        {
            LatencyViewModel latency = new LatencyViewModel();

            latency.UrlOrIp = urlOrIp;
            latency.Status = "Unknown";

            string latencyApiTester = string.Format("http://api.hackertarget.com/nping/?q={0}", urlOrIp);

            try
            {
                //WebRequest request = WebRequest.Create(latencyApiTester);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //string responseFromServer = string.Empty;

                //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                //{
                //    responseFromServer = reader.ReadToEnd();
                //}               

                //IList<string> lineItems = responseFromServer
                //    .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                //    .ToList();

                //latency.Statistics = lineItems
                //    .Where(line =>
                //        !string.IsNullOrEmpty(line) &&
                //        line.ToLower().Contains("max rtt:")
                //    )
                //   .SelectMany(line => line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                //   .Select(latencyEntry =>
                //   {
                //       string[] segments = latencyEntry.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                //       if ((segments != null) && (segments.Count() < 2))
                //           return new StatisticField();

                //       return new StatisticField
                //       {
                //           Name = segments[0],
                //           Value = segments[1]
                //       };
                //   })
                //   .ToList();

                //latency.Status = latency
                //    .Statistics
                //    .Where(field => (field != null) && (!string.IsNullOrEmpty(field.Name)) && (field.Name.ToLower().Contains("avg")))
                //    .DefaultIfEmpty(new StatisticField { Name = "", Value = "" })
                //    .Select(field =>
                //    {
                //        string valueString = field.Value.Trim();

                //        if (string.IsNullOrEmpty(valueString))
                //            return "Unknown";

                //        valueString = Regex.Replace(valueString, "[^0-9.]", "");

                //        decimal latencyValue = 0M;

                //        if (!decimal.TryParse(valueString, out latencyValue))
                //            return "Unknown";

                //        return GetLatencyStatus(latencyValue);
                //    })
                //    .FirstOrDefault();

                var rnd = new Random(DateTime.Now.Millisecond);
                int randomLatencyValue = rnd.Next(0, 350);

                latency.Status = GetLatencyStatus(randomLatencyValue);
            }
            catch
            {

            }

            return  Json(latency);
        }

        string GetLatencyStatus(decimal value)
        {
            string status = "Unknown";

            //https://forum.unity3d.com/threads/question-about-acceptable-levels-of-latency-in-online-gaming.261271/
            if (value > 300)
                status = "Bad";
            else if (value > 170)
                status = "Slow";
            else if (value > 80)
                status = "Average";
            else
                status = "Good";

            return status;
        }
    }
}
