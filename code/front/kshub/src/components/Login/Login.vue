<template>
  <div :class="modalCssClass">
        <div class=" max-500px  modal-card">
            <div class=" modal-card-head field has-icons-right">
                <h1 class=" modal-card-title">Login</h1>
                <span class=" clickable  icon is-right" 
                    title="关闭"
                    @click="close">
                    <i class=" fa fa-close"></i>
                </span>
            </div>
            <div class="isforgivable modal-card-body">
                <div class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-envelope"></i>
                        </span>
                        <input :placeholder="'邮箱'" class=" input"/>
                    </p>
                </div>
                <input type="password" placeholder="密码" class="field control  input"/>
                <input type="checkbox" class=" level-left field control  checkbox left"/>
            </div>
            <div class=" modal-card-foot">
                <div class=" field">
                    <button @click="login" class=" button button-outline">登录</button>
                </div>
            </div>
        </div>
  </div>
</template>
<script lang="ts">
import "bulma/css/bulma.css";
// @ is an alias to /src
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import HelloWorld from '@/components/HelloWorld.vue';
import Axios from 'axios';

@Component({
    components:{
        HelloWorld
    }
})
export default class Login extends Vue {
    userdata:any={
            email: "string",
            rememberme: true,
            password: "string"
        };
    modalCssClass="is-active modal";
    close(){
        this.modalCssClass = 'modal';
    }
    login(){
        
        Axios.post('/api/User/LogIn',this.userdata).then((params)=>{
            var id = params.data.id;
            this.userdata.id=id;
        }).catch((err)=>{
            console.log(err);
            alert(err);
        })
    }
}
</script>

<style>
.clickable{
    cursor: pointer;
}
.max-500px {
    max-width: 500px;
}
.isforgivable{
    background-color: chartreuse;
}
</style>