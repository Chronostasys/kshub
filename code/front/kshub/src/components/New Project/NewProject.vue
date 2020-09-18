<template>
    <div :class="modalCssClass">
        <div class=" max-500px modal-card">
            <div class=" modal-card-head has-icons-right level is-mobile m0 is-gapless is-marginless">
                <label class=" level-item level-left"> 新建课设</label>
                <span class=" level-item level-right clickable icon has-text-right"
                    title="关闭"
                    @click="close">
                    <i class=" fa fa-close"></i>
                </span>
            </div>
            <div class=" modal-card-body">
                <div class="field">
                    <label class="label">课设名称</label>
                    <div class="control">
                        <input class="input" type="text" placeholder="项目名">
                    </div>
                </div>
                <div class="field">
                    <label class="label">关键词</label>
                    <div class="control">
                        <input class="input" type="text" placeholder="关键词">
                    </div>
                </div>
                <hr>
                <div class="field">
                    <label class="label has-text-left">所属课程</label>
                    <hr>
                        <div class="columns">
                            <div class="colunm">
                                <div class="field">
                                    <label class="label has-text-left">学校</label>
                                        <div class="control">
                                            <div class="select">
                                                <select>
                                                    <option>华中科技大学</option>
                                                </select>
                                            </div>
                                        </div>
                                </div>
                            </div>

                            <div class="colunm">
                                <div class="field">
                                    <label class="label has-text-left">院系</label>
                                        <div class="control">
                                            <div class="select">
                                                <select>
                                                    <option>网络安全学院</option>
                                                    <option>计算机学院</option>
                                                </select>
                                            </div>
                                        </div>
                                </div>
                            </div>

                            <div class="colunm">
                                <div class="field">
                                    <label class="label has-text-left">专业</label>
                                        <div class="control">
                                            <div class="select">
                                                <select>
                                                    <option>网络安全</option>
                                                    <option>软件工程</option>
                                                </select>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="">
                            选择班级，搜索框形式
                        </div>
                        <hr>
                </div>
                <div class="field">
                    <label class="label">备注</label>
                    <div class="control">
                        <textarea class="textarea" placeholder="备注"></textarea>
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
</template>

<script lang="ts">
import HelloWorld from '@/components/HelloWorld.vue'
import "bulma/css/bulma.css";
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
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
    @Prop()
    modalCssClass:string;
    close(){
        this.$emit('close')
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
