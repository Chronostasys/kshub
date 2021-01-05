<template>
    <div :class="modalCssClass">
        <div class=" max-500px modal-card">
            <!-- <div class="model-card-head field has-icons-right">
                <h1 class=" modal-card-title ">New project
                    <span class=" clickable icon is-pulled-right"
                        title="关闭"
                        @click="close">
                        <i class=" fa fa-close"></i>
                    </span>
                </h1>
            </div> -->
            <div class=" modal-card-head has-icons-right level m0 is-gapless is-marginless">
                <div class=" level-item level-left">Project</div>
                <span class=" level-item level-right clickable icon has-text-right"
                    title="关闭"
                    @click="close">
                    <i class=" fa fa-close"></i>
                </span>
            </div>
            <div class=" modal-card-body">
                <div class="field">
                    <label class="label">Name</label>
                    <div class="control">
                        <input class="input" type="text" placeholder="Text input">
                    </div>
                </div>
                <div class="field">
                    <label class="label">Username</label>
                    <div class="control has-icons-left has-icons-right">
                        <input class="input is-success" type="text" placeholder="Text input" value="bulma">
                        <span class="icon is-small is-left">
                            <i class="fas fa-user"></i>
                        </span>
                        <span class="icon is-small is-right">
                            <i class="fas fa-check"></i>
                        </span>
                    </div>
                        <p class="help is-success">This username is available</p>
                </div>

                <div class="field">
                    <label class="label">Email</label>
                    <div class="control has-icons-left has-icons-right">
                        <input class="input is-danger" type="email" placeholder="Email input" value="hello@">
                        <span class="icon is-small is-left">
                            <i class="fas fa-envelope"></i>
                        </span>
                        <span class="icon is-small is-right">
                            <i class="fas fa-exclamation-triangle"></i>
                        </span>
                    </div>
                    <p class="help is-danger">This email is invalid</p>
                </div>

                <div class="field">
                    <label class="label">Subject</label>
                        <div class="control">
                            <div class="select">
                                <select>
                                    <option>Select dropdown</option>
                                    <option>With options</option>
                                </select>
                            </div>
                        </div>
                </div>

                <div class="field">
                    <label class="label">Message</label>
                    <div class="control">
                        <textarea class="textarea" placeholder="Textarea"></textarea>
                    </div>
                </div>

                <div class="field">
                    <div class="control">
                        <label class="checkbox">
                            <input type="checkbox">
                                I agree to the <a href="#">terms and conditions</a>
                        </label>
                    </div>
                </div>

                <div class="field">
                    <div class="control">
                        <label class="radio">
                            <input type="radio" name="question">
                                Yes
                        </label>
                        <label class="radio">
                            <input type="radio" name="question">
                                No
                        </label>
                    </div>
                </div>
            </div>
            <div class=" modal-card-foot">
                <div class="field is-grouped">
                    <div class="control">
                        <button class="button is-link">Submit</button>
                    </div>
                    <div class="control">
                        <button class="button is-link is-light">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import "bulma/css/bulma.css";
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import Axios from 'axios';

@Component({
    components:{
        
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
