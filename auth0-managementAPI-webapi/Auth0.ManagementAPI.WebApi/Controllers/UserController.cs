﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth0.ManagementApi;
using Microsoft.Extensions.Configuration;
using Auth0.Core.Collections;
using Auth0.ManagementApi.Models;
using Auth0.ManagementAPI.WebApi.Services.Interfaces;

namespace Auth0.ManagementAPI.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthService AuthService;
        private ManagementApiClient _managementApiClient;

        private ManagementApiClient ManagementApiClient
        {
            get
            {
                if(_managementApiClient == null)
                {
                    string token = Task.Run(() => AuthService.GetTokenAsync()).Result;
                    _managementApiClient = new ManagementApiClient(token, AuthService.Domain);
                }
                return _managementApiClient;
            }
        }

        public UserController(IConfiguration configuration, IAuthService authService)
        {
            AuthService = authService;
        }

        // GET api/user
        [HttpGet]
        public async Task<IPagedList<User>> GetAllUsersAsync()
        {
            try
            {
                return await ManagementApiClient.Users.GetAllAsync();
            }
            catch (Exception ex)
            {
                //TODO LOG
            }

            return null;
        }
    }
}
