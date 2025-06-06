﻿using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Model.DTO;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _accountService.GetAllAccountsAsync();

            return Ok(accounts);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAccountByUsernameAsync(string username)
        {
            var account = await _accountService.GetAccountByUsernameAsync(username);
            if (account == null)
                return NotFound("Account not found");

            return Ok(account);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBalanceAsync([FromBody] UpdateBalanceDto dto)
        {
            var success = await _accountService.UpdateBalanceAsync(dto);
            if (!success)
                return BadRequest("Failed to update balance");

            return Ok("Balance updated successfully");
        }

        #region
        /// <summary>
        /// These functionalities are currently disabled.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAccountAsync([FromBody] Account account)
        {
            return Forbid("This functionality is currently disabled."); // if need this functionality, remove this line and uncomment the code below
            //var success = await _accountService.AddAccountAsync(account);
            //if (!success)
            //    return BadRequest("Failed to add account");

            //return Ok("Account added successfully");
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteAccountAsync(int customerId)
        {
            return Forbid("This functionality is currently disabled."); // if need this functionality, remove this line and uncomment the code below
            //var success = await _accountService.DeleteAccountAsync(customerId);
            //if (!success)
            //    return BadRequest("Failed to delete account");

            //return Ok("Account deleted successfully");
        }

        [HttpGet("balance/{username}")]
        public async Task<IActionResult> GetBalanceByUsernameAsync(string username)
        {
            return Forbid("This functionality is currently disabled."); // if need this functionality, remove this line and uncomment the code below
            //var balance = await _accountService.GetBalanceByUsernameAsync(username);
            //return Ok(balance);
        }
        #endregion
    }
}