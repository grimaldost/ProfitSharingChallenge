using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProfitSharingChallenge.Models
{
    public interface IEmployeesData
    {
        Task<EmployeeItem[]> GetEmployeesAsync();
        Task<EmployeeItem> GetEmployeeAsync(string matricula);
    }

    public class EmployeesData : IEmployeesData
    {
        HttpClient _client;

        public EmployeesData(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<EmployeeItem[]> GetEmployeesAsync()
        {
            EmployeeItem[] employees = null;
            HttpResponseMessage response = await _client.GetAsync("employees.json");
            if (response.IsSuccessStatusCode)
            {
                employees = await response.Content.ReadAsAsync<EmployeeItem[]>();
            }
            return employees;
        }

        public async Task<EmployeeItem> GetEmployeeAsync(string matricula)
        {
            EmployeeItem[] employees = null;
            HttpResponseMessage response = await _client.GetAsync("employees.json");

            if (response.IsSuccessStatusCode)
                employees = await response.Content.ReadAsAsync<EmployeeItem[]>();
            else
                return null;

            foreach(EmployeeItem e in employees)
            {
                if (e.matricula == matricula)
                    return e;
            }
            return null;
        }

        public void AddParticipationAsync(ParticipationItem participation)
        {
            return;
        }

    }
}
