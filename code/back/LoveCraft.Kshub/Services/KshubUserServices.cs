using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using HashLibrary;
using EzPasswordValidator.Validators;
using System.Linq;
using MongoDB.Driver;
namespace LoveCraft.Kshub.Services
{
    //继承DbQuery就不需要在里面声明个成员了（继承方便啊，在里面搞个DbQuery对象就有点蠢了。
    
    public class KshubUserServices:DbQueryServices<KshubUser>
    {
        public KshubUserServices(IDatabaseSettings settings)
            :base(settings,settings.ConnectionString){ }
        /// <summary>
        /// return a user spcified by id or return a default value. 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async ValueTask<KshubUser> FindUserAsync(string studentId)
        {
            return await (await collection.FindAsync(f => f.StudentId == studentId)).FirstOrDefaultAsync();
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
            if(await CheckUserExistenceAsync(user))
            {
                if((await FindUserAsync(user.StudentId)) != null)
                {
                    throw new Exception("This Id has been register already!");
                }
                var validater = new PasswordValidator();
                //validater.SetLengthBounds(8, 20);
                //validater.AddCheck(EzPasswordValidator.Checks.CheckTypes.Letters);
                //validater.AddCheck(EzPasswordValidator.Checks.CheckTypes.Numbers);
                if (!validater.Validate(user.Password))
                {
                    throw new Exception("Password is not strong enough!");
                }
                var pwhash = HashLibrary.HashedPassword.New(user.Password);
                user.Password = pwhash.Hash + pwhash.Salt;
                await AddAsync(user);
            }
            else
            {
                throw new Exception($"You don't belong to {user.SchoolName}!");
            }
            return user;
        }

        
    }
}
