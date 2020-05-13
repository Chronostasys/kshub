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
using LoveCraft.Kshub.Exceptions;
namespace LoveCraft.Kshub.Services
{

    public class KshubUserServices/*<TUser>*/ : UserService<KshubUser>
    {
        public KshubUserServices(IDatabaseSettings settings)
            : base(settings, settings.ConnectionString) { }
        /// <summary>
        /// return a user spcified by id or return a default value. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async ValueTask<KshubUser> FindUserAsync(string userId)
        {
            var user =await (await collection.FindAsync(f => f.UserId == userId)).FirstOrDefaultAsync();
            return user;
        }
        public async ValueTask<KshubUser> FindUserAsync(Guid userId)
        {
            var user = await (await collection.FindAsync(f => f.Id == userId)).FirstOrDefaultAsync();
            return user;
        }
        public string HashPasswordWithSalt(string password)
        {
            var pwhash = HashLibrary.HashedPassword.New(password);
            return pwhash.Hash + pwhash.Salt;
        }
        /// <summary>
        /// 学号姓名学校与数据库中匹配返回true
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async ValueTask<bool> CheckUserExistenceAsync(KshubUser user)
        {
            //需要在管理员加入用户的信息中进行检索，判断能不能加到数据库里面去
            //这玩意应该放在另一个service里面去，因为操作的不是一个数据表
            return true;
        }
        public async ValueTask<KshubUser> AddUserAsync(KshubUser user)
        {
            //try-catch结构更好还是if-else更好？
            if (await CheckUserExistenceAsync(user))
            {
                if ((await FindUserAsync(user.UserId)) != null)
                {
                    throw new Exception("This Id has been register already!");
                }
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
            }
            else
            {
                throw new _401Exception($"You don't belong to {user.SchoolName}!");
            }
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
                //这里设置了AuthenticationProperties的
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
    
    }
}
