using FrontEnd.Models;
using FrontEnd.Services.Contracts;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Reflection.Emit;

namespace FrontEnd.Services.Repository
{
    public class CEASRepo : ICEAS
    {
        private string _RequestUrl;
        HttpClient _httpClient;

        public CEASRepo(string url, HttpClient httpClient)
        {
            _RequestUrl = url;
            _httpClient = httpClient;
        }

        public async Task<SintaxisModel> CheckSintaxisAsync(int idCode, string code)
        {
            SintaxisModel sintaxisModel = new SintaxisModel();
            var requestData = new { idCode, code };
            var json = JsonSerializer.Serialize(requestData);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_RequestUrl + "Compiler/checkSyntax"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    sintaxisModel = JsonSerializer.Deserialize<SintaxisModel>(jsonResponse);
                }
            }

            return sintaxisModel;
        }

        public async Task<RunModel> RunCompilerAsync(int idCode, string code)
        {
            RunModel outputModel = new RunModel();
            var requestData = new { idCode, code };
            var json = JsonSerializer.Serialize(requestData);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_RequestUrl + "Compiler/run"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    outputModel = JsonSerializer.Deserialize<RunModel>(res);
                }
                else
                {
                    outputModel = new RunModel
                    {
                        idResponse = -1,
                        output = $"Error: {response.StatusCode}"
                    };
                }
            }
            return outputModel;
        }
    }
}
