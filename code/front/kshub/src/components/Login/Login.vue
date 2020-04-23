<template>
    <div :class="modalCssClass">
        <div class=" max-500px  modal-card">
            <div class=" modal-card-head field has-icons-right">
                <p class=" modal-card-title font_head_title">{{isReg?'注册':'登录'}}</p>
                <span class=" clickable  icon is-right" 
                    title="关闭"
                    @click="close">
                    <i class="fa fa-user-circle-o"></i>
                </span>
            </div>
            <div class="isforgivable modal-card-body">
                <div v-if="isReg" class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-id-card"></i>
                        </span>
                        <input
                            type="id" 
                            placeholder="请输入用户名" 
                            class=" input"
                            @click="removeError"
                            v-model="userdata.id"
                        />
                    </p>
                </div>
                <div class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-envelope"></i>
                        </span>
                        <input 
                            :placeholder="'请输入邮箱'" 
                            class=" input"
                            @click="removeError"
                            v-model="userdata.email"
                        />
                    </p>
                </div>
                <div class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-lock"></i>
                        </span>
                        <input 
                            type="password" 
                            placeholder="请输入密码" 
                            class="field control  input"
                            @click="removeError"
                            v-model="userdata.password"
                        />
                    </p>
                </div>
                <div v-if="isReg" class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-lock"></i>
                        </span>
                        <input 
                            type="password" 
                            placeholder="请确认密码" 
                            class="field control  input"
                            @click="removeError"
                            v-model="confirmPassword"
                        />
                    </p>
                </div>
                <div v-if="!isReg" class=" level-left field control">
                    <input type="checkbox" class="checkbox left"/>
                    记住我
                </div>
                <span >
                    <a class="is-marginless is-paddingless" style="float:left;height:24px;"
                        @click="toggleReg"
                        >{{isReg?'有账号?现在登陆':'无账号?现在注册'}}</a>
                    <a class=" is-marginless is-paddingless"
                        style="float:right;height:24px;"
                        >忘记密码?</a>
                </span>
                <br/>
                <span class=" has-text-left has-text-danger" v-if="isShowErrorMessage">{{errorMessage}}</span>
            </div>
            <div class=" modal-card-foot levle">
                <div class=" level-item">
                    <button v-if="!isReg" @click="login" class=" button button-outline  ">登录</button>
                    <button v-if="isReg" @click="register" class=" button button-outline ">注册</button>
                    <button class="button button-indent  " @click="close">取消</button>
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
    isReg=true;
    isShowErrorMessage=false;
    errorMessage='';
    confirmPassword='';
    userdata:any={
            id: "",
            email: "",
            rememberme: false,
            password: "",
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
    toggleReg(){
        this.isReg=!this.isReg;
    }
    register(){
        this.validatePassword();
                Axios.post('/api/User/LogIn',this.userdata).then((params)=>{
        }).catch((err)=>{
            console.log(err.response.userdata.errorMessage);
            alert(err);
        })
    }
    removeError(){
    this.isShowErrorMessage=false;
  }
    validatePassword(){
    if(this.confirmPassword!==this.userdata.password){
      this.isShowErrorMessage=true;
      this.errorMessage='确认密码和密码不相同！';
    }
    else if (this.userdata.password.length<=8) {
      this.isShowErrorMessage=true;
      this.errorMessage='密码长度应该大于8！';
    }
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
    background-color:coral;
}
</style>