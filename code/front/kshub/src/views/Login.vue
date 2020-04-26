<template>
  <div class="container is-fluid">
    <div class="notification">
    <Login :modalCssClass="cssClass" @close="close"/>
    <div class="login">
      <img alt="EVA logo" src="../assets/EVA.png">
    </div>
    <button class="button is-black" @click="login">login</button>
  </div>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import NewProject from '../components/New Project/NewProject'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";

@Component({
  components:{
    Login
  }
})

export default class Home extends Vue {
  cssClass = 'modal';
  heightlight = 'modal';
  change(){
    this.cssClass = 'modal is-active';
  }

  login(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
}
</script>