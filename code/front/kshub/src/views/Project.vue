<template>
  <div class="container is-fluid">
    <div class="notification">
    <NewProject :modalCssClass="cssClass" @close="close"/>
    <div class="home">
      <img alt="Vue logo" src="../assets/aqua.jpg">
    </div>
    <button class="button is-black" @click="newProj">new project</button>
    <button class="button is-black">my project</button>
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
}
</script>
