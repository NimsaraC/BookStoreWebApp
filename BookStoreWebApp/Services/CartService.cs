using BookStoreWebApp.DTOs;

namespace BookStoreWebApp.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CartService> _logger;

        public CartService(HttpClient httpClient, ILogger<CartService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<CartDto> GetCartItemsAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CartDto>($"api/cart/{id}");
        }
        public async Task<bool> AddCartAsync(AddCartItemDto addCartItemDto, int id)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/cart/{id}/items", addCartItemDto);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> UpdateQuantiytAsync(int editItemQuantity, int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cart/items/{id}/quantity", editItemQuantity);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
                var response = await _httpClient.DeleteAsync($"api/Cart/items/{id}");
                response.EnsureSuccessStatusCode();
                return true;
        }

    }
}
