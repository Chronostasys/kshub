<template>
    <div>
            

        <section class="main-content columns is-fullheight absolute">
        
            <aside class="fix column is-2 is-narrow-mobile is-fullheight section is-hidden-mobile">
                <p class="menu-label is-hidden-touch">My Course</p>
                <ul >
                  <li :key="item.id" class="menu-list" v-for="(item, index) in items">
                      <a href="#" :class="item.ishighlight?'is-active':''" @click="()=>highlight(item, index)" :key="item1">
                        <span class="icon"><i class="fa fa-table"></i></span>{{item.name}}
                      </a>
                  </li>
                </ul>
          </aside>

            <section class="container column is-offset-2 is-10 is-fullheight-with-navbar">
              <div class="container">
                <center>
                  <figure class="image is-96x96 clickable">
                    <img style="max-height: 96px;" class=" is-rounded" :src="courseDetail.coverUrl" />
                  </figure>
                </center>
                <h1 class="title">{{courseDetail.name}}</h1>
                <h2 class="subtitle">
                  {{courseDetail.description}}
                </h2>
              </div>
            </section>
            
        </section>
    </div>
</template>

<script lang="ts">
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
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
