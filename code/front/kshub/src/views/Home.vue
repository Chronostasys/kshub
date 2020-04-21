<template>
<div>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <Login/>
  </div>
  <button class="button is-black">new project</button>
</div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import project from '../components/Project/project'
import { Prop } from 'vue-property-decorator'

export default {
  name: 'Home',
  components: {
    Login
  },
}
</script>
