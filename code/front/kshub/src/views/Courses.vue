<template>
    <div class="container">
      <div class="columns">
        <div class="column is-one-quarter">
          <figure class="">
            <img class="is-rounded" :src="course.coverUrl">
          </figure>
          <label class="课程名">{{course.name}}</label>
          <div class="">
            <article class="message is-link">
              <div class="message-body">
                {{course.description}}
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
              <tbody class="Kstable" v-for="(Ks,i) in this.Ks" :key="i">
                <tr>
                  <td>{{manager[0].name}}</td>
                  <td></td>
                  <td>{{manager[0].id}}</td>
                  <td><a class="article" @click="gotoarticle()">{{Ks.name}}</a></td>
                  <td>{{Ks.description}}</td>
                  <td>{{Ks.projectManager}}</td> 
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
  course=[];
  user=[];
  page = 0;
  items = [];
  Ks=[];
  manager=[];
  projectManager:string ='';
  managerid='';
  prevHighlightIndex = 0;
  id:string = "";
  created(){
    // 从url获取id
    this.id = this.$route.params['id'];
    this.getCourse();
    this.getKs();

    Axios.get(`/api/Course/GetCourses?page=${this.page}&pagesize=${this.size}&IsAscending=true`)
    .then((re)=>{
      this.items = (re.data as any[]).map(v=>{
        v.ishighlight = false;
        return v;
      });
    })
  }
addCourse(){
    this.getCourse()
  }
getCourse(){
    Axios.get('/api/Course/GetCourse?id='+this.id)
    .then((res)=>{
      console.log(res.data);
      this.course=res.data;
      }).catch((err)=>{console.log(err);
      });
  }
  getUsers(managerid){
    Axios.get('/api/User?id='+managerid)
    .then((res)=>{
      console.log(res.data);
      this.manager=res.data;
    }).catch((err)=>{
      console.log(err);
    })
  }
  getKs(){
    Axios.get('/api/Ks/id').then((res)=>{
      console.log(res.data);
      this.Ks=res.data;
      console.log(this.Ks);
      let ids = this.Ks.map((ks,i,l)=>ks.projectManager) as string[];
      let s = ids.join('&id=');
      this.getUsers(s);
    }).catch((err)=>{console.log(err);
    });
  }
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  gotoarticle(){
    this.$router.push("/Article")
  }
  @Prop()
  highLight='highlight'
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
