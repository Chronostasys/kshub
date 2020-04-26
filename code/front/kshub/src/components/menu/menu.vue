<template>
<div>
    <nav class="navbar" role="navigation" aria-label="main navigation">
        <div class="navbar-brand">
            <a class="navbar-item" href="/">
                <img src="https://cdn.jsdelivr.net/gh/vmlite/s/bulma/images/bulma-logo.png" width="112" height="28">
            </a>

            <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            </a>
        </div>

        <div id="navbarBasicExample" class="navbar-menu">
            <div class="navbar-start">
                <a class="navbar-item" @click="jumpHome">
                    Home
                </a>

                <a class="navbar-item"  @click="jumpAbout">
                    About
                </a>

                <div class="navbar-item has-dropdown is-hoverable">
                    <a class="navbar-link">
                    Course
                    </a>

                    <div class="navbar-dropdown">
                        <a class="navbar-item" @click="jumpMyCourse">
                            My Course
                        </a>
                        <NewProject :modalCssClass="cssClass" @close="close"/>
                        <a class="navbar-item" @click="newProj">
                            New Course
                        </a>
                        <hr class="navbar-divider">
                        <a class="navbar-item">
                            Something else
                        </a>
                    </div>
                </div>
            </div>
            <div class="navbar-end">
                <div class="navbar-item">
                    <div class="buttons">
                        <a class="button is-primary">
                            <strong>Sign up</strong>
                        </a>
                        <a class="button is-light">
                            Log in
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </nav>
</div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '@/components/Login/Login.vue'
import NewProject from '@/components/New Project/NewProject'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";



@Component({
  components:{
    NewProject
  }
})

export default class Menu extends Vue {
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
  jumpAbout(){
      this.$router.push("/About");
  }
  jumpMyCourse(){
      this.$router.push("/MyProject");
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
