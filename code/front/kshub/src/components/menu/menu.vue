<template>
<div>
  <NewProject :modalCssClass="cssClass" @close="close"/>
    <Login :modalCssClass="loginClass"
        @close="closeLogin"></Login>
    <nav class="navbar is-white" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
            <a class="navbar-item" href="/">
                <img src="@/assets/lovecraft.png">
            </a>

            <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            </a>
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
                        <a v-if="!isLog" class="button is-link" @click="login">
                            Log in
                        </a>
                        <a v-if="isLog" class="button is-link" @click="signout">
                            Sign out
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
import { Component, Prop, Vue, Watch } from "vue-property-decorator"
import UserCenter from '@/components/UserCenter/UserCenter.vue'
import Axios from 'axios';

@Component({
  components:{
    NewProject,
    Login,
    UserCenter
  }
})

export default class Menu extends Vue {
  @Prop()
  userInfo!:any;
  cssClass = 'modal';
  loginClass = 'modal';
  isLog = false
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
  signout(){
      Axios.post('/api/KshubUser/Signout/',).then((params)=>{
        }).catch((err)=>{
            console.log(err);
            alert(err);
        });
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
