using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LW3_task_5.Models;

namespace LW3_task_5.Service
{
    public class APIClient
    {
        const string baseAdres = "https://restcountries.com/v3.1/";
        static readonly HttpClient _http = new HttpClient();

        public string ErorMessage { get; private set; }
        public APIClient(int seconds)
        {
            _http.Timeout = TimeSpan.FromSeconds(seconds);
            _http.BaseAddress = new Uri(baseAdres);
        }
        public async Task<List<Country>> Request(string url)
        {
            try
            {
                var response = await _http.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var counties = JsonSerializer.Deserialize<List<Country>>(json);
                return counties;
            }
            catch (HttpRequestException hre)
            {
                ErorMessage = $"Помилка мережi або сервера {hre.Message}";
                if (hre.StatusCode.HasValue)
                {
                    switch (hre.StatusCode)
                    {
                        case HttpStatusCode.BadRequest: ErorMessage = "Неправильний запит, не можна обробити."; break;
                        case HttpStatusCode.Unauthorized: ErorMessage = "Сервер не може вас розпізнати"; break;
                        case HttpStatusCode.Forbidden: ErorMessage = "Сервер заборонив доступ"; break;
                        case HttpStatusCode.NotFound: ErorMessage = "Інформації за запитом не знайдено"; break;
                        case HttpStatusCode.InternalServerError: ErorMessage = "Внутрішні проблеми сервера"; break;
                        case HttpStatusCode.NotImplemented: ErorMessage = "Функцію не реалізовано"; break;
                        case HttpStatusCode.BadGateway: ErorMessage = "Поганий шлюз"; break;
                        case HttpStatusCode.ServiceUnavailable: ErorMessage = "Сервіс зараз недоступний"; break;
                        case HttpStatusCode.GatewayTimeout: ErorMessage = "Час очікування шлюзу закінчвся"; break;
                    }
                }
                return null;
            }
            catch (TaskCanceledException)
            {
                ErorMessage = "Вичерпався час очікування";
                return null;
            }
            catch (JsonException)
            {
                ErorMessage = "Невірний формат.";
                return null;
            }
            catch (Exception e)
            {
                ErorMessage = $"Помилка {e.Message}";
                return null;
            }
        }
    }
}


