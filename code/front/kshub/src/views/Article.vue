<template>
  <div id="editarticle" class="container">
    <div>
      <div class="container">
        <div class="columns">
          <div class="column is-one-quarter" style="padding-right:20px;">
            <p class="font_head_Line" style="margin-top:33px">文章作者</p>
            <p class="font_head_Line" style="margin-top:50px">所属课题</p>
          </div>
          <div class="column is-three-quarters" style="padding-top:20px;padding-left:30px;">
            <div class="medium-editor">
              <div class="media" style="align-items: center;">
                <span class="tag is-info is-medium">draft</span>
                <div class="media-content">
                  <p class="font_head_Line"></p>
                </div>
                <div class="media-right">
                  <div class="buttons" >
                    <button class="button button_normal" >编辑</button>
                  </div>
                </div>
              </div>
              <p style="font-size:18px;margin-top:20px">
                <b>摘要</b>
              </p>
              <p class="tags" style="font-size:18px;margin-bottom:5px;">
                <b>关键词：</b>
                <span
                  class="tag tag_normal"
                  style="margin-bottom:0px"
                ></span>
              </p>
              <p class="font_small color_grey">
                <span>更新日期：</span>
                <span style="margin-left:20px;">阅读量：</span>
              </p>
            </div>
            <div
              class="medium-editor"
              id="reader"
              style="padding-top:20px"
            ></div>
            <div class="columns">
              <div class="column is-half" style="padding-top:20px">
                <IconButton
                  style="margin-right:40px"
                  name="点赞"
                  iconTrue="fa-thumbs-up"
                  iconFalse="fa-thumbs-o-up"
                ></IconButton>
                <IconButton
                  name="收藏"
                  iconTrue="fa-star"
                  iconFalse="fa-star-o"
                ></IconButton>
              </div>
              <div class="column is-half">
                <!-- 下一波 -->
              </div>
            </div>
          </div>
        </div>
        <div>
          <p class="font_head_Line"></p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import "bulma/css/bulma.css";
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import Login from '../components/Login/Login.vue'
import axios from "axios";
import NewProject from '../components/New Project/NewProject'
import { Component, Prop, Vue, Watch } from "vue-property-decorator";

@Component({
  components:{
    NewProject
  }
})

export default class Article extends Vue {
}
</script>