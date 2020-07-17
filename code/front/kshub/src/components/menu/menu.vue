<template>
<div>
  <NewProject :modalCssClass="cssClass" @close="close"/>
    <Login :modalCssClass="loginClass"
        @close="closeLogin"></Login>
    <nav class="navbar is-white" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
            <a class="navbar-item" href="/">
                <img src="@/assets/lovecraft.png" style="width: 128px;object-fit: cover;">
            </a>

            <a @click="MyMenu" role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="myMenu">
            <div>
                <span aria-hidden="true"></span>
                <span aria-hidden="true"></span>
                <span aria-hidden="true"></span>
            </div>
            </a>
        </div>

        <div>
            <div id="myMenu" :class="'navbar-menu is-hidden-desktop ' +MyMenuClass" style="padding-left: 2%;">
                <div class="control has-icons-right">
                    <span class="icon is-right">
                        <i class="fa fa-search" style="pointer-events:initial;cursor:pointer;">
                        </i>
                    </span>
                    <input type="text" placeholder="搜索您感兴趣的内容..." autocomplete="off" class="input search-input is-rounded" style="width:100%;">
                <input type="password" autocomplete="new-password" style="display: none;">
                </div>
                <hr class="dropdown-divider">
                <button data-target="modalRegister" aria-haspopup="true" class="button modal-button button_normal is-outlined" style="width: 95%; display: none;">登录</button>
                <div>
                    <a class="dropdown-item" @click="jumpHome">
                        <i class="fa fa-pencil"></i>
                        主页
                    </a>
                    <a class="dropdown-item" @click="jumpAbout">
                        <i class="fa fa-exclamation-circle"></i>
                        关于
                    </a>
                    <hr class="dropdown-divider">
                    <a class="dropdown-item" @click="jumpMyCourse">
                        <i class="fa fa-home"></i>
                        我的课设
                    </a>
                    <a class="dropdown-item" @click="newProj">
                        <i class="fa fa-book"></i>
                        新建课设
                    </a>
                    <a v-if="userInfo.roles.indexOf(strs.Roles.anonymous)>-1" class="dropdown-item" @click="login">
                        <i class="fa fa-user"></i>
                        登录
                    </a>
                    <a v-else class="dropdown-item" @click="jumpUserPage">
                        <i class="fa fa-user"></i>
                        我的主页
                    </a>
                </div>
            </div>
        </div>

        <div id="navbarBasicExample" class="navbar-menu is-mobile">
            <div class="navbar-start">
                <a class="navbar-item" @click="jumpHome">
                    Home
                </a>

                <a class="navbar-item"  @click="jumpAbout">
                    About
                </a>
                <div class="navbar-item has-dropdown is-hoverable">
                    <a class="navbar-link">
                    Course
                    </a>

                    <div class="navbar-dropdown">
                        <a class="navbar-item" @click="jumpMyCourse">
                            My Course
                        </a>
                        <a class="navbar-item" @click="newProj">
                            New Course
                        </a>
                        <hr class="navbar-divider">
                        <a class="navbar-item">
                            Something else
                        </a>
                    </div>
                </div>
            </div>
            <div class="navbar-end">
                <div class="navbar-item">
                    <div class="buttons">
                        <a v-if="userInfo.roles.indexOf(strs.Roles.anonymous)>-1" class="button is-link" @click="login">
                            Log in
                        </a>
                        <div v-else class="">
                            <figure class="image is-32x32 clickable" title="我的主页"  @click="jumpUserPage">
                                <img style="max-height:32px;overflow:hidden;" class=" is-rounded" src="http://img3.cache.netease.com/photo/0031/2017-03-22/CG5RTM5L4UUJ0031.jpg" />
                            </figure>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </nav>
</div>
</template>

<script lang="ts">
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '@/components/Login/Login.vue'
import NewProject from '@/components/New Project/NewProject.vue'
import { Component, Prop, Vue, Watch } from "vue-property-decorator"
import UserCenter from '@/components/UserCenter/UserCenter.vue'
import Axios from 'axios';
import { UserInfo, STRINGS } from '../../common/STRINGS'

@Component({
  components:{
    NewProject,
    Login,
    UserCenter
  }
})

export default class Menu extends Vue {
  @Prop()
  userInfo:UserInfo;
  cssClass = 'modal';
  loginClass = 'modal'
  MyMenuClass='';
  strs = STRINGS;
  created(){
      console.log(this.userInfo);
      this.$router.afterEach((to,from)=>{
          this.MyMenuClass='';
      });
  }
  MyMenu(){
      console.log('click');
      if(this.MyMenuClass==='')
      this.MyMenuClass='is-active'
      else
      this.MyMenuClass=''
  }
  login(){
    this.loginClass = 'modal is-active';
  }
  closeLogin(){
    this.loginClass = 'modal';
  }
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  jumpHome(){
      this.$router.push("/");
  }
  jumpAbout(){
      this.$router.push("/About");
  }
  jumpMyCourse(){
      this.$router.push("/MyProject");
  }
  jumpUserPage(){
      this.$router.push("/user/"+this.userInfo.userId);
  }
  signout(){
      Axios.post(STRINGS.signoutApi,).then((params)=>{
          window.location.reload();
        }).catch((err)=>{
            console.log(err);
            alert(err);
        });
  }
}

</script>


<style scoped>
/* 并不想让这个样式全局起作用 */
/* img{
    width: 128px;
    object-fit: cover;
} */
</style>

<style>
.fix{
    position: absolute;
    width: 24px;
    height: 48px;
}
img{
    object-fit: cover;
}
</style>
