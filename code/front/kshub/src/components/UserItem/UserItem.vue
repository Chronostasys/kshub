<template>
    <div class="font_base">
        <div class="media">
            <div class="media-left">
                <figure class="image is-64x64" style="cursor:pointer;" @click="jumpToProfile()">
                    <img class="is-rounded" :src="information.avatarUrl" style="width:64px;height:64px;" />
                </figure>
            </div>
            <div class="media-content" style="overflow:hidden;">
                <div class="level" style="margin-bottom:7px;">
                    <div class="level-left">
                        <p
                            classs="font_title-normal"
                            style="cursor:pointer;"
                            @click="jumpToProfile()"
                        >{{information.name}}</p>
                    </div>
                    <div class="level-right">
                        <!-- <IconButton
                        style="margin-right:5px;float:right"
                        :toggleUrl="getToggleUrl()"
                        :checkUrl="getCheckUrl()"
                        :data="information.followers"
                        name="关注作者"
                        iconTrue="fa-star"
                        iconFalse="fa-star-o"
                        ></IconButton> -->
                    </div>
                </div>
                <p class="font_abstract" style="margin-top:4px">{{information.affiliation}}</p>
                <div class="tags" style="margin-top:6px">
                    <span
                        class="tag tag_normal"
                        v-if="(keywords, index) in information.keywords"
                        v-bind:key="index"
                    >{{keywords}}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import "bulma/css/bulma.css";
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import { UserDTO, getUserEmpty } from "../../DTO/Account/UserDTO";
import Axios from "axios";
import IconButton from "../../components/FunctionIcon/IconButton.vue";

@Component({
    components: {
        IconButton
    }
})
export default class User extends Vue{
    @Prop() brief!: UserDTO;
    @Prop() src!: string;

    private information: UserDTO = getUserEmpty();

    private created() {
        const
    }
}


