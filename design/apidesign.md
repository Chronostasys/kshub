# api设计更新
由于寒假开发期间，后端出现了诸多问题，特在这里重新设计一些关键接口。  

## 关于网站的工作流程

首先，我将在下方描述一遍我们网站为了完成客户需求的一个标准工作流程

### 前端显示课程列表的页面
- 用户点击进入某个课程，前端使用api获取这个课程的详情
- 后端在详情中返回所属院系的id，主要负责人的id
- 前端获取到课程介绍等信息，渲染
  - 前端调用查询院系详情的api获取院系的详情，并渲染
  - 前端调用查询用户信息的api获取主要负责人的详细信息，并渲染`

### 前端显示学校列表的页面
- 用户进入某个学校
  - 前端调用
​`/api​/University​/GetUniversity/{id}`获取学校详情
  - 前端调用`/api/College/GetUniColleges`获取属于该校的学院
- 前端获取对应信息后分别渲染
- 用户点入某个学院
  - 前端调用`/api/College/GetCollege/{id}`获取学院的详情
  - 前端调用`/api/Course/GetCourses`获取属于该学院的课程列表
- 前端获取对应信息后分别渲染
- 用户点入某个课程
  - 前端调用`/api/Course/GetCourse/{id}`获取课程的详情
  - 前端调用`/api/Ks`获取属于该课程的课设列表
- 前端获取对应信息后分别渲染
- 用户点入某个课设
  - 前端调用`/api/Ks/{id}`获取课设的详情
  - 前端调用`/api/User`获取参与该课设的学生列表

