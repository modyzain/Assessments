using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App.UI.Services
{
    public interface IExecuteAPIService
    {
        Task<IRestResponse> ExecuteAPI(string url, Method method, DataFormat format, object body);
    }

    public class ExecuteAPIService : IExecuteAPIService
    {
        private readonly IConfiguration _configuration;

        public ExecuteAPIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IRestResponse> ExecuteAPI(string url, Method method, DataFormat format, object body)
        {
            var client = new RestClient($"{_configuration["ServiceAPI"]}{url}");
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            var request = new RestRequest(method) { RequestFormat = format };

            if (body != null)
            {
                if (method == Method.GET)
                    request.AddParameter("param", body);
                else request.AddJsonBody(body);
            }

            var response = await Task.FromResult(client.Execute(request));
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
                return response;
            else return null;
        }
    }
}
