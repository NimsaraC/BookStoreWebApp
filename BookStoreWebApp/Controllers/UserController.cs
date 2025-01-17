﻿using BookStoreWebApp.DTOs;
using BookStoreWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookStoreWebApp.Models;

namespace BookStoreWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;

        public UserController(UserService userService, OrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.UserLoginAsync(loginDto);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (user.Role == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Book");
                }
            }
              return View(loginDto);  
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.ReagisterUserAsync(userCreateDto);
                if(userCreateDto.Role == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Book");
                }
                
            }
            return View(userCreateDto);
        }
        public async Task<IActionResult> Details()
        {
            int id = 1;
            var user = await _userService.GetUserByIdAsync(id);
            var order = await _orderService.GetAllOrdersByUserAsync(id);

            var data = new UserProfile
            {
                User = user,
                Orders = order,
            };
            return View(data);
        }
    }
}
