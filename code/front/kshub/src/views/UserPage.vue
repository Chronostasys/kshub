<template>
  <div>
    <div class="columns card">
      <div class=" column is-6 hero">
        <center class="hero-body">
          <figure class="image is-96x96 clickable">
            <img style="max-height: 96px;" class=" is-rounded" src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg" />
          </figure>
          <br/>
          <div class="title">{{pageUserInfo.name}}</div>
          <small class=" has-icons-left">
            <span class="fa fa-envelope"></span>
            {{pageUserInfo.email}}
          </small>
          <div class="subtitle has-icons-left">
            <span class=" fa fa-id-card"></span>
              {{pageUserInfo.userId}}
          </div>
          <nav style="margin-top:22px;" class="level font_main">
            <div class="level-item has-text-centered">
              <div>
                <p>获赞</p>
                <p>{{0}}</p><!--information.awesomes-->
                <p>项目</p>
                <p>{{0}}</p><!--information.projectNum-->
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p>粉丝</p>
                <p>{{0}}</p><!--information.followers-->
                <p>文章</p>
                <p>{{0}}</p><!--information.articleNum-->
              </div>
            </div>
            <div class="level-item has-text-centered">
              <div>
                <p>关注</p>
                <p>{{0}}</p><!--information.follows-->
                <p>经验</p>
                <p>{{0}}</p><!--information.exp-->
              </div>
            </div>
          </nav>
        </center>
      </div>
      <div class=" column is-6 hero">
        <div class="hero-body">
          <div class=" card has-text-left" style="height:100%">
            <div class=" card-header-title">Intro:</div>
            <div class=" card-content">
              {{pageUserInfo.introduction===''?'这个人什么也没写':pageUserInfo.introduction}}
            </div>
          </div>
        </div>
      </div>
    </div>
    <hr style="margin-top:0%" />

    <div class="columns">
      <div class="column is-one-third">
        <center>
          <p class="font_head_Line">参与课题</p>
        </center>
      </div>
      <div class="column is-two-thirds">
        <ProjectList :number="3" :src="myProjectSrc" v-if="ifShowMember"></ProjectList>
      </div>
    </div>

    <hr />

    <div class="columns is-0">
      <div class="column is-one-third">
        <center>
          <p class="font_head_Line">最新文章</p>
        </center>
      </div>
      <div class="column is-two-thirds">
        <ArticleList :src="articleSrc" page="1" pagesize="3" :enable="false"></ArticleList>
        <div style="text-align:right"> 
          <a class="font_main" :href="articleUrl">更多文章</a>
        </div>
      </div>
    </div>
    <a v-if="isSelf" class=button  @click="Signout()">退出登录</a>
  </div>
</template>

<script lang="ts">
// @ is an alias to /src
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import { UserInfo, STRINGS } from '../common/STRINGS';
import Axios from 'axios';

@Component({})
export default class UserPage extends Vue {
  @Prop()
  userInfo:UserInfo;
  pageUserInfo:UserInfo = new UserInfo();
  id = '';
  
  get isSelf() : boolean {
    return this.id===this.userInfo.userId;
  }
  
  created(){
    console.log(this.userInfo);
    this.id = this.$route.params.id;
    console.log(this.id);
    Axios.get(STRINGS.userInfoApi+'?id='+this.id).then(res=>{
      this.pageUserInfo = res.data;
      console.log(res.data);
    });
  }

  Signout(){
    Axios.post(STRINGS.signoutApi).then((params)=>{
      window.location.replace('/');
    }).catch((err)=>{
    })
  }
}
</script>

