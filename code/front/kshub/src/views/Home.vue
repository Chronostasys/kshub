<template>
<div>
  <div class="tile is-ancestor">
    <div class="tile is-vertical is-4">
      <div class="tile is-vertical">
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      </div>
    </div>
    <div class="tile is-vertical is-4">
      <div class="tile is-vertical">
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      </div>
    </div>
    <div class="tile is-vertical is-4">
      <div class="tile is-vertical">
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      <article class="tile is-child box">
        <p class="title">Middle box</p>
        <p class="subtitle">With an image</p>
        <figure class="image is-4by3">
          <img src="https://bulma.zcopy.site/images/placeholders/640x480.png">
        </figure>
      </article>
      </div>
    </div>
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
