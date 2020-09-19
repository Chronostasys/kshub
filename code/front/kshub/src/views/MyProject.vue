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
                        <div class="select">
                            <select>
                                <option>华中科技大学</option>
                                <option>空</option>
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
                                <option>网络安全</option>
                                <option>软件工程</option>
                                <option>人工智能</option>
                                <option>空</option>
                            </select>
                        </div>
                    </div>
                </div>
              </div>

                <button class="button is-link">确定</button>

            </div>
        </div>
        
        <div :style="'float:left;margin-top: 30px;width: 1px; background: darkgray;'+'height:'+windowheight.toString()+'px'"></div>

        <div class="">

        </div>
      </div>
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
