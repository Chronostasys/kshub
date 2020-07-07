<template>
<div>
  <NewProject :modalCssClass="cssClass" @close="close"/>
    <Login :modalCssClass="loginClass"
        @close="closeLogin"></Login>
    <nav class="navbar is-white" role="navigation" aria-label="main navigation">
        <div class="navbar-brand" @click="MyMenu">
            <a class="navbar-item" href="/">
                <img src="@/assets/lovecraft.png">
            </a>

            <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="myMenu">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
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
                        <i class="fa fa-group"></i>
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
                        <a class="button is-link" @click="login">
                            Log in
                        </a>
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
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import Axios from 'axios';



@Component({
  components:{
    NewProject,
    Login
  }
})

export default class Menu extends Vue {
  @Prop()
  userInfo!:any;
  cssClass = 'modal';
  loginClass = 'modal'
  MyMenuClass=''
  MyMenu(){
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
}

</script>


<style scoped>
/* 并不想让这个样式全局起作用 */
img{
    width: 128px;
    object-fit: cover;
}
</style>

<style>
.fix{
    position: absolute;
    width: 24px;
    height: 48px;
}
</style>
