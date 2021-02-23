<template>
    <div class="container">
      <div class="columns">
        <div class="column is-one-quarter">
          <div class="subtitle is-hidden-touch">课程筛选</div>
            <div class="field">
              <div class="columns">
                <div class="column">
                  <span class="tag is-large is-link is-light">学校</span>
                </div>
                <div class="column">
                  <div class="control">
                    <div class="select" v-for="(Universities,i) in this.Universities" :key="i">
                      <select>
                          <option>空</option>
                          <option>{{Universities.name}}</option>
                      </select>
                    </div>
                  </div>
                </div>
              </div>

              <div class="columns">
                <div class="column">
                  <span class="tag is-large is-link is-light">院系</span>
                </div>
                <div class="column">
                  <div class="control">
                    <div class="select">
                      <select>
                        <option>空</option>
                        <option>计算机学院</option>
                        <option>网络安全学院</option>
                      </select>
                    </div>
                  </div>
                </div>
              </div>

              <div class="columns">
                <div class="column">
                  <span class="tag is-large is-link is-light">专业</span>
                </div>
                <div class="column">
                  <div class="control">
                    <div class="select">
                      <select>
                        <option>空</option>
                        <option>软件工程</option>
                        <option>人工智能</option>
                        <option>网络安全</option>
                      </select>
                    </div>
                  </div>
                </div>
              </div>

              <div class="columns">
                <div class="column">
                  <div class="field">
                    <p class="control">
                      <input class="input" type="text" placeholder="关键词" >
                    </p>
                  </div>
                </div>
              </div>

                <button class="button is-link">确定</button>

            </div>
        </div>
        
        <div class="column is-three-quarters">
          <div class="tile is-ancestor">
            <div class="tile is-parent" >
              <div v-for="(courses,i) in this.courses" :key="i" class="card">
                <header class="card-header">
                  <p class="card-header-title">
                    {{courses.name}}
                  </p>
                </header>
                <div class="card-content">
                  <div class="content">
                    {{courses.description}}
                  </div>
                </div>
                <footer class="card-footer">
                  <a href="#" class="card-footer-item">添加</a>
                  <a href="" class="card-footer-item" @click="jumpCourses(courses.id)">详细</a>
                  <a href="#" class="card-footer-item">删除</a>
                </footer>
              </div>
            </div>
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
import axios from 'axios';



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
  courses=[];
  item=[];
  prevHighlightIndex = 0;
  Universities=[];
  created(){
    this.getCourses();
    this.getUniversities();
  }
  getUniversities(){
    Axios.get('/api/University/GetUniversities')
    .then((ress)=>{
        console.log(ress.data);
        this.Universities=ress.data;
    }).catch((err)=>{console.log(err);
    });
    }
  getCourses(){
    axios.get(`/api/Course/GetCourses?page=${this.page}&pagesize=${this.size}&IsAscending=true`)
    .then((res)=>{
      console.log(res.data);
      this.courses=res.data;
    }).catch((err)=>{console.log(err);
    })
  }
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  jumpCourses(id:string){
    // 见../router/index.ts 38行
    this.$router.push("/Courses/"+id);
    
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
