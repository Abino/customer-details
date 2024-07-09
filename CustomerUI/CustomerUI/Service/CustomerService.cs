using CustomerUI.Extension;
using CustomerUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace CustomerUI.Service
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public CustomerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Customer>>($"{_apiUrl}/Customers");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Customer>($"{_apiUrl}/Customers/{id}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAsync(Customer customer)
        {
            try
            {
                await _httpClient.PostAsJsonAsync($"{_apiUrl}/customers", customer);

                //var responsemsg = await _httpClient.PostAsync($"{_apiUrl}/customers",
                //    new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

                //var responseData = await responsemsg.Content.ReadAsStreamAsync();

                //return responseData.DeserializeJson<List<Customer>>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"{_apiUrl}/customers/{customer.Id}", customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"{_apiUrl}/customers/{id}");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
