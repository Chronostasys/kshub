<template>
    <div class="container">
      <div class="columns">
        <div class="column is-one-quarter">
            <figure class="">
                <img class="is-rounded" src="https://bulma.zcopy.site/images/placeholders/128x128.png">
            </figure>
            <label class="课程名">这里是课程名</label>
            <div class="">
                <article class="message is-link">
                <div class="message-body">
                    这里写课程简介
                </div>
                </article>
            </div>
        </div>
        
        <div :style="'float:left;margin-top: 30px;width: 1px; background: darkgray;'+'height:'+windowheight.toString()+'px'"></div>

        <div class="column">
          <div class="table">
            <thead>
              <tr>
                <th><abbr>姓名</abbr></th>
                <th><abbr>班级</abbr></th>
                <th><abbr>学号</abbr></th>
                <th><abbr>课设名称</abbr></th>
                <th><abbr>课设简介</abbr></th>
                <th><abbr>上传日期</abbr></th>
              </tr>
            </thead>
              <tbody>
                <tr>
                  <td>张三</td>
                  <td>网络安全1班</td>
                  <td>123456789</td>
                  <td><a class="article" @click="gotoarticle()">盗取游戏账号的研究</a></td>
                  <td>盗号是一门学问...</td>
                  <td>2020.1.02</td>
                </tr>
                <tr>
                  <td>老八</td>
                  <td>社会1班</td>
                  <td>12345678910</td>
                  <td><a class="article" @click="gotoarticle()">翔的研究</a></td>
                  <td>吃翔是一种艺术...</td>
                  <td>2020.1.02</td>
                </tr>
              </tbody>
          </div>
        </div>
      </div>
    </div>
</template>

<script lang="ts">
// @ is an alias to /src
import Login from '../components/Login/Login.vue'
import NewProject from '../components/New Project/NewProject.vue'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import Axios from 'axios';



@Component({
  components:{
    NewProject
  }
})

export default class Home extends Vue {
    get windowheight(){
    return document.body.offsetHeight-120
  }
  cssClass = 'modal';
  size = 10;
  courseDetail = {
    "id": "adad0f85-2fea-4ce1-8ca6-5fcc87b0b962",
    "name": "string13",
    "coverUrl": "string",
    "description": "string",
    "teacherIds": [],
    "scoreRating": {}
  };
  page = 0;
  items = [];
  prevHighlightIndex = 0;
  created(){
    Axios.get(`/api/Course/GetCourses?page=${this.page}&pagesize=${this.size}&IsAscending=true`)
    .then((re)=>{
      this.items = (re.data as any[]).map(v=>{
        v.ishighlight = false;
        return v;
      });
    })
  }
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  @Prop()
  highLight='highlight'
  highlight(item:any, index:number){
    const element = this.items[this.prevHighlightIndex];
    element.ishighlight=false;
    this.prevHighlightIndex = index;
    item.ishighlight=true;
    Axios.get('/api/Course/GetCourse?id='+item.id)
      .then(re=>{
        this.courseDetail = re.data;
      })
  }
}

</script>

<style>
.fix{
    position: absolute;
    width: 24px;
    height: 48px;
}
</style>

<style scoped>
.container{
  max-width:none
}
</style>
