<template>
  <div id="app">
    <Menu/>
    <router-view/>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import { Component, Prop, Vue, Watch } from "vue-property-decorator"
import Menu from '../src/components/Menu/Menu';


@Component({
  components:{
    Menu
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
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>

