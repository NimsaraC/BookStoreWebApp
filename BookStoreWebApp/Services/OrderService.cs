using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersByUserAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/order/{id}");
        }
    }
}
