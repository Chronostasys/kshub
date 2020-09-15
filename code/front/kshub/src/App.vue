<template>
  <div id="app">
    <Menu :userInfo="userInfo" style="margin-left: 10px;margin-right: 10px; margin-bottom: 10px;"/>
    <router-view
      :userInfo="userInfo" class="container"
    />
  </div>
</template>


<script lang="ts">
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import { Component, Prop, Vue, Watch } from "vue-property-decorator"
import Menu from '@/components/menu/menu.vue';
import Axios from 'axios';
import STRINGS from './common/STRINGS'
import { UserInfo } from './common/STRINGS'


@Component({
  components:{
    Menu
  }
})

export default class Home extends Vue {
  cssClass = 'modal';
  userInfo:UserInfo=new UserInfo();
  created(){
    Axios.post(STRINGS.loginApi,{}).then(res=>{
      this.userInfo = res.data;
      console.log(this.userInfo);
    })
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
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #17324d;
}

#nav {
  padding: 10px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
body{
  height: 100%;
}
html{
  height: 100%;
}
</style>
