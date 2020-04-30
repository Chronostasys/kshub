<template>
    <div>
        <html>
        <body>
        <div id="app">
            

        <section class="main-content columns is-fullheight absolute">
        
            <aside class="fix column is-2 is-narrow-mobile is-fullheight section is-hidden-mobile">
                <p class="menu-label is-hidden-touch">My Course</p>
                <ul class="menu-list">
                <li>
                    <a href="#" class="">
                    <span class="icon"><i class="fa fa-table"></i></span> My course 1
                    </a>
                </li>
                <li>
                    <a href="#" class="">
                    <span class="icon"><i class="fa fa-table"></i></span> My course 2
                    </a>
                        <a href="#">
                        <span class="icon is-small"><i class="fa fa-table"></i></span> My course 3
                        </a>
                    </li>
                    <li>
                        <a href="#">
                        <span class="icon is-small"><i class="fa fa-table"></i></span> My course 4
                        </a>
                </li>
                <li>
                    <a href="#" class="">
                    <span class="icon"><i class="fa fa-table"></i></span> My course 5
                    </a>
                </li>
                </ul>
          </aside>

            <section class="container column is-10 is-fullheight-with-navbar">
                <br/>
                <br/>
                <div>Hello World</div>
            </section>
            
        </section>
        </div>
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

<style>
.fix{
    position: absolute;
    width: 24px;
    height: 48px;
}
</style>
