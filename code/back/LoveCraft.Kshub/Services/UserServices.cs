using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using HashLibrary;
using EzPasswordValidator.Validators;
using MongoDB.Driver;
using LimFx.Business.Services;
using LimFx.Business.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using DocumentFormat.OpenXml.InkML;
using LimFx.Business.Exceptions;
using System.Threading.Tasks.Sources;
using OpenXmlPowerTools;

namespace LoveCraft.Kshub.Services
{
    public class UserServices : UserService<KshubUser>
    {
        public UserServices(IDatabaseSettings settings)
            : base(settings, settings.ConnectionString)
        {
            var user = new KshubUser
            {
                Id = Guid.NewGuid(),
                Name = "Grillen",
                SchoolName = "",
                Introduction = "Grillen",
                Email = "2016231075@qq.com",
                IsEmailConfirmed = true,
                UserId = "AdminAccount",
                PassWordHash = "Gutentag2020@", //_secretRecord.GrillenPassword,

                Roles = new List<string> { KshubRoles.Admin, KshubRoles.Grillen, KshubRoles.User },
            };
            try
            {
                user.PassWordHash = HashPasswordWithSalt(user.PassWordHash);
                collection.InsertOne(user);
            }
            catch (Exception e)
            {

            }
        }
        /// <summary>
        /// return certain user or throw an exception if not find
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async ValueTask<KshubUser> FindUserAsync(string userId)
        {
            try
            {
                var filter = Builders<KshubUser>.Filter.Eq(t => t.UserId, userId);
                var user = await collection.Find(filter).FirstAsync();
                return user;
            }
            catch (Exception e)
            {
                throw new _401Exception("Cannot find this User");
            }
        }
        public async ValueTask<KshubUser> FindUserAsync(Guid id)
        {
            try
            {
                var filter = Builders<KshubUser>.Filter.Eq(t => t.Id, id);
                return await collection.Find(filter).FirstAsync();

            }
            catch (Exception e)
            {
                throw new _401Exception("Cannot find this User");
            }
        }
        public string HashPasswordWithSalt(string password)
        {
            var pwhash = HashLibrary.HashedPassword.New(password);
            return pwhash.Hash + pwhash.Salt;
        }

        public async ValueTask CheckAvailableAsync(string email, string userId)
        {
            bool havaException = false;
            try
            {
                await FindUserAsync(userId);
                havaException = true;
            }
            catch { }
            if (havaException)
                throw new _401Exception("This userId has been taken");
            havaException = false;
            try
            {
                await GetUserByEmailAsync(email);
                havaException = true;
            }
            catch { }
            if (havaException)
                throw new _401Exception("This email has been taken");

        }
        /// <summary>
        /// Add an user with checking UserId and email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async ValueTask<KshubUser> AddUserWithCheckAsync(KshubUser user)
        {
            await CheckAvailableAsync(user.Email, user.UserId);
            var validater = new PasswordValidator();
            //validater.SetLengthBounds(8, 20);
            //validater.AddCheck(EzPasswordValidator.Checks.CheckTypes.Letters);
            //validater.AddCheck(EzPasswordValidator.Checks.CheckTypes.Numbers);
            if (!validater.Validate(user.PassWordHash))
            {
                throw new _401Exception("Password is not strong enough!");
            }
            user.PassWordHash = HashPasswordWithSalt(user.PassWordHash);
            await AddAsync(user);
            return user;
        }

        public async ValueTask<bool> LogInAsync(KshubUser user, HttpContext httpContext, bool rememberMe = true, bool validPassword = true)
        {

            var u = await FindUserAsync(user.UserId);
            if (u == null)
            {
                throw new _401Exception("Cannot find this Id");
            }
            bool auth = true;
            if (validPassword)
            {
                var hash = u.PassWordHash.Substring(0, 32);
                var salt = u.PassWordHash.Substring(32);
                var h = new HashedPassword(hash, salt);
                auth = h.Check(user.PassWordHash);
            }
            if (auth)
            {
                //set AuthenticationProperties
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                };

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, u.Id.ToString()),
                };
                for (int i = 0; i < u.Roles.Count; i++)
                {
                    claims.Add(new Claim(ClaimTypes.Role, u.Roles[i]));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                return true;
            }
            else
            {
                throw new _400Exception("Password and email do not match!");
            }
        }
        public async ValueTask SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
        public async ValueTask CheckAuthAsync(string roles,Guid guid)
        {
            try
            {
                var t = await collection.Find(t => t.Id == guid && t.Roles.Contains(roles)).ToListAsync();
            }
            catch
            {
                throw new _403Exception();
            }
        }
    }
}