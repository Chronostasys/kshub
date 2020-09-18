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

