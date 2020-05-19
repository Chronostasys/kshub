# 出现问题以及解决大全：
## 1. undefined /swagger/v1/swagger.json
在controller里面如果不加特性设置的话，（默认是get？），就会造成路径冲突导致swagger报错。  
不是数据库的问题！在类的方法里面加上特性`[Route("xxx")]`就可以。[错误详细描述与解决](https://btrehberi.com/swagger-failed-to-load-api-definition-fetch-error-undefined-hatasi-cozumu/)

## 2. `HttpContext`和`User.Identity.IsAuthenticated`这些东西都在`Controller`基类里面
## 3. 不使用特性而是检查数据库进行权限检查
特性`[Authorize(Roles ="admin")]`检查的是Cookie里的身份值，而cookie在本地存储如果在数据库里的相应信息发生变更而本地cookie没有发生变化，就会产生严重的后果。所以再进行权限等可以修改的相关信息验证时采用查询数据库进行验证而非相信cookue中的信息。

# 5.5 debug问题
## 4.没有进行完好的身份验证导致之后出现的问题
## 5.Role和Roles不一致，所以说在automapper的时候失败导致返回的dto为null
## 6.初始化类的时候没有new
```cs
var piece = new UserInCourse
{
    CourseId = courseId,
    UserId = userId,
    Roles =new List<string>{ CourseRoles.User, CourseRoles.Admin,CourseRoles.Owner }
};
```
## 接受不定数量参数的函数如何写？
```cs
public async ValueTask AddAsync(params T[] emails)
```
使用关键字`params`与泛型`T[]`即可。