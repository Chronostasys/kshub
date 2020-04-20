# User类

```csharp
    public class User:Entity
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        //需不需要有手机号，email等相关信息
        public string email { get; set; }
    }
```