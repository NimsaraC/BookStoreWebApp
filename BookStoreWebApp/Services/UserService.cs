using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/user");
        }
        public async Task<UserDto> UserLoginAsync(LoginDto login)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", login);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserDto>();

                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ReagisterUserAsync(UserCreateDto userCreateDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user", userCreateDto);
            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/user/{id}");
        }

    }
}
