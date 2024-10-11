﻿using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
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

    }
}
