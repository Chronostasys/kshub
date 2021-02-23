<template>
    <div :class="modalCssClass">
        <div class=" modal-background">
            <div class="max-500px modal-card">
                <div class="modal-card-head has-icons-right level is-mobile m0 is-gapless is-marginless">
                    <label class="level-item level-left">新建课程</label>
                    <span class="level-item level-right clickabel icon has-text-right"
                        title="关闭"
                        @click="close">
                        <i class=" fa-fa-close"></i>
                    </span>
                </div>
                <div class="modal-card-body">
                    <div class="field">
                        <label class="label">课程名称</label>
                        <div class="control">
                            <input class="input" type="text" placeholder="课程名">
                        </div>
                    </div>
                <hr>
                    <div class="field">
                        <label class="label has-text-left">选择课程隶属的学校院系</label>
                        <hr>
                        <div class="columns">
                            <div class="column">
                                <div class="field">
                                    <label class="label has-text-left">学校</label>
                                    <div class="control">
                                        <div class="select">
                                            <select>
                                                <option>空</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column">
                                <div class="field">
                                    <label class="label has-text-left">院系</label>
                                    <div class="control">
                                        <div class="select">
                                            <select>
                                                <option>空</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="column">
                                <div class="field">
                                    <label class="label has-text-left">专业</label>
                                    <div class="select">
                                        <select>
                                            <option>空</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=" modal-card-foot">
                    <div class="field is-grouped">
                        <div class="control">
                            <button class="button is-link">创建</button>
                        </div>
                        <div class="control">
                            <button class="button is-link is-light" @click="close">取消
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import "bulma/css/bulma.css";
import {Component,Prop,Vue,Watch} from "vue-property-decorator";
import Axios from 'axios';

@Component({
    components:{

    }
})
export default class Login extends Vue {
    userdata:any={
        email:"string",
        rememberme:true,
        password:"string"
    };
@Prop()
modalCssClass:string;
created(){

}
close(){
    this.$emit('close')
}
}

</script>

<style>
</style>