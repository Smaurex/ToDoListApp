using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ToDoListApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://todo-list.dcism.org";

        public ApiService()
        {
            var handler = new HttpClientHandler();

            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(15) // ✅ Prevents infinite hang
            };
        }

        // SIGN UP
        public async Task<string> SignUp(string fname, string lname, string email, string password, string confirmPassword)
        {
            var data = new
            {
                first_name = fname,
                last_name = lname,
                email = email,
                password = password,
                confirm_password = confirmPassword
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/signup_action.php", content);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        // SIGN IN
        public async Task<string> SignIn(string email, string password)
        {
            var url = $"{BaseUrl}/signin_action.php?email={email}&password={password}";
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}