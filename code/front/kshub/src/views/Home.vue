<template>
<div>
  <div class="columns">
    <div class="column is-three-quarters">
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>Mr.zhou</strong> <small>@Mr.zhou</small>
                <br>
                abaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaabaa
              </p>
            </div>
          </div>
        </article>
      </div>
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>John Smith</strong> <small>@johnsmith</small> <small>31m</small>
                <br>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean efficitur sit amet massa fringilla egestas. Nullam condimentum luctus turpis.
              </p>
            </div>
          </div>
        </article>
      </div>
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>John Smith</strong> <small>@johnsmith</small> <small>31m</small>
                <br>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean efficitur sit amet massa fringilla egestas. Nullam condimentum luctus turpis.
              </p>
            </div>
          </div>
        </article>
      </div>
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>John Smith</strong> <small>@johnsmith</small> <small>31m</small>
                <br>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean efficitur sit amet massa fringilla egestas. Nullam condimentum luctus turpis.
              </p>
            </div>
          </div>
        </article>
      </div>
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>John Smith</strong> <small>@johnsmith</small> <small>31m</small>
                <br>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean efficitur sit amet massa fringilla egestas. Nullam condimentum luctus turpis.
              </p>
            </div>
          </div>
        </article>
      </div>
      <div class="box">
        <article class="media">
          <div class="media-left">
            <figure class="image is-64x64" style="overflow:hidden">
              <img src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg">
            </figure>
          </div>
          <div class="media-content">
            <div class="content">
              <p>
                <strong>John Smith</strong> <small>@johnsmith</small> <small>31m</small>
                <br>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean efficitur sit amet massa fringilla egestas. Nullam condimentum luctus turpis.
              </p>
            </div>
          </div>
        </article>
      </div>
    </div>
    <div class="column">
      <article class="message is-danger">
        <div class="message-header">
          <p>阿巴阿巴阿巴阿巴</p>
          <button class="delete" aria-label="delete"></button>
        </div>
        <div class="message-body">
          不知道拿来干嘛就只好阿巴阿巴阿巴阿巴了
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
}
</script>
