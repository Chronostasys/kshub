# 出现问题以及解决大全：
## 1. undefined /swagger/v1/swagger.json
在controller里面如果不加特性设置的话，（默认是get？），就会造成路径冲突导致swagger报错。  
不是数据库的问题！在类的方法里面加上特性`[Route("xxx")]`就可以。[错误详细描述与解决](https://btrehberi.com/swagger-failed-to-load-api-definition-fetch-error-undefined-hatasi-cozumu/)
