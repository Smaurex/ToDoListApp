using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;

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
                Timeout = TimeSpan.FromSeconds(40) // ✅ Prevents infinite hang
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

        public async Task<string> GetTasks(string status, int userId)
        {
            var url = $"{BaseUrl}/getItems_action.php?status={status}&user_id={userId}";
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> AddTask(string name, string desc, int userId)
        {
            var data = new
            {
                item_name = name,
                item_description = desc,
                user_id = userId
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}/addItem_action.php", content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateTask(int itemId, string name, string desc)
        {
            var data = new
            {
                item_id = itemId,
                item_name = name,
                item_description = desc
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/editItem_action.php",
                content
            );

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangeStatus(int itemId, string status)
        {
            var data = new
            {
                item_id = itemId,
                status = status
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/statusItem_action.php",
                content
            );

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteTask(int itemId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/deleteItem_action.php?item_id={itemId}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}