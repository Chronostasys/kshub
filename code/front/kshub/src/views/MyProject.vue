<template>
    <div class="container">
      <div class="columns">
        <div class="column is-one-quarter">
          <div class="subtitle is-hidden-touch">My Course</div>
          <ul>
            <li :key="item.id" class="menu-list" v-for="item in items">
              <a href="#" :class="item.ishighlight?'is-active':''" @click="()=>highlight(item)" :key="item1">
                <span class="icon"><i class="fa fa-table"></i></span>{{item.name}}
              </a>
            </li>
          </ul>
        </div>
        
        <div :style="'float:left;margin-top: 30px;width: 1px; background: darkgray;'+'height:'+windowheight.toString()+'px'"></div>

        <div class="">

        </div>
      </div>
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
    get windowheight(){
    return document.body.offsetHeight-120
  }
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

<style scoped>
.container{
  max-width:none
}
</style>
