﻿using Mango.Service.EmailAPI.Data;
using Mango.Service.EmailAPI.Models;
using Mango.Service.EmailAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Service.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br />Cart Email Requested.");
            message.AppendLine("<br />Total: " + cartDto.CartHeader.CartTotal);
            message.AppendLine("<br />");
            message.AppendLine("<ul>");
            foreach (var item in cartDto.CartDetails)
            {
                message.AppendLine("<li>");
                message.AppendLine(item.Product.Name + " x " + item.Count);
                message.AppendLine("</li>");
            }
            message.AppendLine("</ul>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLogger = new()
                {
                    Email = email,
                    Message = message,
                    EmailSent = DateTime.Now
                };

                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLogger);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
