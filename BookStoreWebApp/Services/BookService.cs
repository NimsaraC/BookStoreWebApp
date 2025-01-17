﻿using BookStoreWebApp.DTOs;
using BookStoreWebApp.Models;

namespace BookStoreWebApp.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BookService> _logger;

        public BookService(HttpClient httpClient, ILogger<BookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BookDto>>("api/book");
        }
        public async Task<bool> CreateBookAsync(BookCreateDto bookCreateDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/book", bookCreateDto);
            return true;
        }
        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookDto>($"api/book/{id}");
        }
        public async Task<bool> UpdateBookAsync(int id, BookCreateDto bookCreateDto)
        {
                var response = await _httpClient.PutAsJsonAsync($"api/book/{id}", bookCreateDto);
                return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/book/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
