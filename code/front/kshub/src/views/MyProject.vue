<template>
<div>
    <nav class="navbar" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
        <a class="navbar-item" href="">
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
        <a class="navbar-item">
            Home
        </a>

        <a class="navbar-item">
            About
        </a>

        <div class="navbar-item has-dropdown is-hoverable">
            <a class="navbar-link">
            Course
            </a>

            <div class="navbar-dropdown">
            <a class="navbar-item">
                My Course
            </a>
            <NewProject :modalCssClass="cssClass" @close="close"/>
            <a class="navbar-item" @click="newProj">
                New Course
            </a>
            <a class="navbar-item">
                Contact
            </a>
            <hr class="navbar-divider">
            <a class="navbar-item">
                Report an issue
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
    <div class="beside">


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
                <a href="#" class="is-active">
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
