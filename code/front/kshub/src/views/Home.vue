<template>
  <div>
    <html>
    <body>
      <section class="main-content columns is-fullheight absolute">
        <aside class="fix column is-2 is-narrow-mobile is-fullheight section is-hidden-mobile">
          <div>Hello World</div>
        </aside>
      </section>
      <section class="container column is-10 is-fullheight-with-navbar">
        <br/>
        <br/>
        <div>Bye~</div>
      </section>
    </body>
    </html>
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
