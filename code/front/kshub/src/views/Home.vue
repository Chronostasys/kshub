<template>
  <div>
    <project :modalCssClass="cssClass" @close="close"/>
    <div class="home">
      <img alt="Vue logo" src="../assets/logo.png">
    </div>
    <button class="button is-black" @click="newProj">new project</button>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import project from '../components/Project/project'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";

@Component({
  components:{
    project
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
