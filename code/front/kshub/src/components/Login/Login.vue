<template>
    <div  :class="modalCssClass">
        <div class="modal-background"></div>
        <div class=" max-500px  modal-card">
            <div class=" modal-card-head field has-icons-right" style="margin:0px">
                <p class=" modal-card-title font_head_title">{{isReg?'注册':'登录'}}</p>
                <span class=" clickable  icon is-right" 
                    title="关闭"
                    @click="close">
                    <i class="fa fa-close"></i>
                </span>
            </div>
            <div class="modal-card-body">
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
                            v-model="userdata.userId"
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
                    <input type="checkbox" class="checkbox left" v-model="userdata.rememberme"/>
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
                    <button class="button button-indent is-model-right  " @click="close">取消</button>
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
import { RegDto, UserInfo } from '../../common/STRINGS';
import STRINGS from '../../common/STRINGS';

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
    userdata:RegDto=new RegDto();
    userInfo:UserInfo=new UserInfo();
    @Prop()
    modalCssClass='modal';
    close(){
        this.$emit('close');
    }
    login(){
        Axios.post(STRINGS.loginApi,this.userdata).then((params)=>{
            window.location.reload();
        }).catch((err)=>{
            this.isShowErrorMessage=true;
            this.errorMessage = "学号或密码不正确！";
        })
    }
    toggleReg(){
        this.isReg=!this.isReg;
    }
    register(){
        this.validatePassword();
        Axios.post(STRINGS.registerApi,this.userdata).then((params)=>{
            window.location.reload();
        }).catch((err)=>{
            this.isShowErrorMessage = true;
            this.errorMessage = "注册失败";
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

<style lang="scss">
.clickable{
    cursor: pointer;
}
.max-500px {
    max-width: 500px;
}
.isforgivable{
    background-color:rgb(26, 22, 14);
}
</style>