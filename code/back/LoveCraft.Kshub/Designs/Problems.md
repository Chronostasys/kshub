# 出现问题以及解决大全：
## 1. undefined /swagger/v1/swagger.json
在controller里面如果不加特性设置的话，（默认是get？），就会造成路径冲突导致swagger报错。  
不是数据库的问题！在类的方法里面加上特性`[Route("xxx")]`就可以。[错误详细描述与解决](https://btrehberi.com/swagger-failed-to-load-api-definition-fetch-error-undefined-hatasi-cozumu/)


## 2. 在对数据库查询的时候lambda表达式点不出来对应的成员
```csharp
var co=await collection.FindAsync(item=>item.)
```
比如说在这里的函数`FindAsync`中的iten对象里，本来