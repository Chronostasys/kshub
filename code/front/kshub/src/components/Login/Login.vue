<template>
    <div :class="modalCssClass">
        <div class=" max-500px  modal-card">
            <div class=" modal-card-head field has-icons-right">
                <p class=" modal-card-title font_head_title">{{isReg?'注册':'登录'}}</p>
                <span class=" clickable  icon is-right" 
                    title="关闭"
                    @click="close">
                    <i class="fa fa-close"></i>
                </span>
            </div>
            <div class="isforgivable modal-card-body">
                <div v-if="isReg" class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-user"></i>
                        </span>
                        <input
                            type="name" 
                            placeholder="请输入用户名" 
                            class="input"
                            @click="removeError"
                            v-model="userdata.name"
                        />
                    </p>
                </div>
                <div v-if="isReg" class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-envelope"></i>
                        </span>
                        <input
                            type="email"
                            placeholder="请输入邮箱" 
                            class="input"
                            @click="removeError"
                            v-model="userdata.email"
                        />
                    </p>
                </div>
                <div class="field">
                    <p class="control has-icons-left ">
                        <span class="icon is-small is-left">
                            <i class="fa fa-id-card"></i>
                        </span>
                        <input 
                            :placeholder="'请输入学号'" 
                            class=" input"
                            @click="removeError"
                            v-model="userdata.studentId"
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
    isReg=false;
    isShowErrorMessage=false;
    errorMessage='';
    confirmPassword='';
    userdata:any={
        name: null,
        studentId: null,
        password: null,
        schoolName: "string",
        introduction: "string",
        email: null,
        role: null
    };
    @Prop()
    modalCssClass='modal';
    close(){
        this.$emit('close');
    }
    login(){
        console.log('ok')
        Axios.post('/api/KshubUser/LogIn/',this.userdata).then((params)=>{
            var studentId = params.data.studentId;
            var password = params.data.password;
            this.userdata.studentId=studentId;
            this.userdata.password=password;
            console.log(params.data);
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
        Axios.post('/api/KshubUser/Adduser/',this.userdata).then((params)=>{
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
      this.errorMessage='两次输入的密码不相同！';
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
    background-color:wheat;
}
</style>