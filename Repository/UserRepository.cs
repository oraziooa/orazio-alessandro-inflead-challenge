using Challange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Challange.Data.DTO;

namespace Challange.Data.Repositories
{
    public class UserRepository
    {
        private readonly DbChallangeContext _context;
        private readonly HttpClient _httpClient;

        public UserRepository(DbChallangeContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<User?> GetUserFromExternalApiAsync(string? gender, string? email, string? username)
        {
            var query = "urlAPIEsterna"; //da sostituire con url all api esterna

            if (!string.IsNullOrEmpty(gender))
                query += $"&gender={gender}";
            if (!string.IsNullOrEmpty(email))
                query += $"&email={email}";
            if (!string.IsNullOrEmpty(username))
                query += $"&username={username}";

            var response = await _httpClient.GetStringAsync(query);
            var user = JsonConvert.DeserializeObject<UserDTO>(response);

            if (user == null) return null;

            Address? addressEntity = null;

            if (user.Address != null)
            {
                addressEntity = new Address
                {
                    Id = Guid.NewGuid(),
                    City = user.Address.City ?? "Unknown",
                    Street = user.Address.Street ?? "Unknown",
                    Zipcode = user.Address.Zipcode ?? "00000",
                    State = user.Address.State ?? "Unknown",
                    Creationdate = DateTime.UtcNow
                };

                _context.Addresses.Add(addressEntity);
            }

            var userEntity = new User
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Username = user.Username,
                Fullname = user.FullName,
                Profilepicurl = user.ProfilePicUrl,
                Gender = user.Gender,
                Phonenumber = user.PhoneNumber,
                Employment = user.Employment,
                Keyskill = user.KeySkill,
                Addressid = addressEntity?.Id,
                Creationdate = DateTime.UtcNow
            };

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return userEntity;
        }


        public async Task<User?> GetUserByFilterAsync(string? gender, string? email, string? username)
        {
            // Cerco nel database
            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u =>
                    (string.IsNullOrEmpty(gender) || u.Gender == gender) &&
                    (string.IsNullOrEmpty(email) || u.Email == email) &&
                    (string.IsNullOrEmpty(username) || u.Username == username));

            // Se non lo trovo, cerco sull api esterna e lo salvo a db
            if (user == null)
            {
                user = await GetUserFromExternalApiAsync(gender, email, username);
            }

            return user;
        }
    }
}
