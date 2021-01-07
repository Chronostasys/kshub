<template>
    <div>
        <section class="main-content columns is-fullheight absolute">
        
            <aside class="fix column is-2 is-narrow-mobile is-fullheight section is-hidden-mobile">
                <p class="menu-label is-hidden-touch">My Course</p>
                <ul >
                  <li :key="item.id" class="menu-list" v-for="item in items">
                      <a href="#" :class="item.ishighlight?'is-active':''" @click="()=>highlight(item)" :key="item1">
                        <span class="icon"><i class="fa fa-table"></i></span>{{item.name}}
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
</template>

<script lang="ts">
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import NewProject from '../components/New Project/NewProject.vue'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";



@Component({
  components:{
    NewProject
  }
})

export default class Home extends Vue {
  cssClass = 'modal';
  items = [{
    id:1,
    name:"zhouquan",
    ishighlight:false
  },{
    id:2,
    name:"zuoyue",
    ishighlight:false
  }];
  newProj(){
    this.cssClass = 'modal is-active';
  }
  close(){
    this.cssClass = 'modal';
  }
  @Prop()
  highLight='highlight'
  highlight(item:any){
    for (let index = 0; index < this.items.length; index++) {
      const element = this.items[index];
      element.ishighlight=false;
    }
    item.ishighlight=true;
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
