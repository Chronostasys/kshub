# 调用KshubUserController之中的FakeApi之后会添加四个user用户
```json
{
    new KshubUser
    {
        Id=new Guid(),
        Name="Test",
        UserId = "100",
        Email = "100@test.com",
        PassWordHash ="test100",
        Roles =new List<string> { "User"},
    },
    new KshubUser
    {
        Id=new Guid(),
        Name="Test1",
        UserId = "101",
        Email = "101@test.com",
        PassWordHash ="test101",
        Roles =new List<string> { "User"},
    },
    new KshubUser
    {
        Id=new Guid(),
        Name="Test2",
        UserId = "102",
        Email = "100@test.com",
        PassWordHash ="test100",
        Roles =new List<string> { "User"},
    },
    new KshubUser
    {
        Id=new Guid(),
        Name="Test3",
        UserId = "103",
        Email = "103@test.com",
        PassWordHash ="test103",
        Roles =new List<string> { "User"},
    }
};
```