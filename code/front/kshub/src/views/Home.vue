<template>
<div>
  <div class="columns">
    <div class="column is-three-quarters">
      <div class="box">
        <article  v-for="(item,i) in this.Ks" :key=i  class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img :src="item.coverUrl">
            </figure>
          </div>
          <div class="media-content" @click="jumpArticle()">
            <div class="content">
              <p>
                <strong >{{item.name}}</strong> <small>@{{item.projectManager}}</small>
                <br>
                {{item.description}}
              </p>
            </div>
          </div>
        </article>
      </div>
    </div>
    <div class="column">
      <article class="message is-danger">
        <div class="message-header">
          <p>网站公告</p>
        </div>
        <div class="message-body">
          广告招租中！！！！
        </div>
        <div class="level">
          <div class="level-left">
          </div>
          <div class=" level-right">
            2021/1/16
          </div>
        </div>
      </article>
    </div>
  </div>
</div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import NewProject from '../components/New Project/NewProject'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import Axios from 'axios';

@Component({
  components:{
    NewProject
  }
})

export default class Home extends Vue {
  cssClass = 'modal';
  Ks=[];
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  getCourses(){
    Axios.get('/api/Courses').then((res)=>{
      console.log(res.data);
      courses=res.data;
      for(item in courses)
      {
        console.log(item.name);
      }
      }).catch((err)=>{console.log(err);
      alert(err);
      });
  }
  getKs(){
    Axios.get('/api/Ks').then((res)=>{
      console.log(res.data);
      this.Ks=res.data;
      for(item in this.Ks){
        console.log(item)
      }
      }).catch((err)=>{console.log(err);
      })
  }
  jumpArticle(){
    Axios.get()
  }
  created(){
    this.getKs()
  }
}
</script>
