<template>
    <div class="columns is-multiline">
        <div 
            class="column"
            v-for="(userInformation, index) in usersInformation"
            :key="index"
            :class="columnStyle"
        >
            <UserItem :brief="userInformation" ref="userItemComponent"></UserItem>
        </div>
    </div>
</template>

<script lang="ts">
import "bulma/css/bulma.css";
import { Component, Prop, Vue, Watch}  from 'vue-property-decorator';
import { UserDTO } from "../../DTO/Account";
import UserItem from "../UserItem/UserItem.vue";
import Axios from 'axios'

@Component({
    component: {
        UserItem
    }
})
export default class User extends Vue{
    @Prop() usersBrief!: UserDTO[];
    @Prop() src!: string;
    @Prop() number!: number;

    private columnStyle = "is-full";
    private userInformation: UserDTO[] = [];

    private created(){
        if(this.src){
            const that = this;
            Axios.get(this.src).then(function(response){
                if(response.data.length) {
                    that.usersInformation = response.data;
                }else {
                    that.noUser();
                }
            });
        }   else {
            this.usersInformation = this.usersBrief;
        }
        if(this.number) {
            if(this.number == 2) {
                this.columnStyle = "is-half";
            }else if (this.number == 3){
                this.columnStyle = "is-one-third";
            }else if (this.number == 3){
                this.columnStyle = "is-one-quarter";
            }else if (this.number == 3){
                this.columnStyle = "is-one-fifth";
            }
        }
    }
    private noUser(){
        this.$emit("noUser");
    }
    private refresh(){
        if(this.src) {
            const that = this;
            Axios.get(this.src).then(function(response){
                if (response.data.length) {
                    that.usersInformation = response.data;
                    const itemComponent = that.$refs.userItemComponent as any;
                    that.$nextTick(function(){
                        for (let i = 0; i < itemComponent.length; i++) {
                            itemComponent[i].refresh();
                        }
                    });
                }else {
                    that.noUser();
                }
            });
        } else {
            this.usersInformation = this.usersBrief;
        }
    }
}
</script>

<style >

</style>