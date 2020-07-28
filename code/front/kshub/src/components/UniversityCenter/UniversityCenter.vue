<template>
  <div class="modal is-active">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title font_head_Line">修改基本信息</p>
      </header>
      <section class="modal-card-body">
        <div class="field font_title">
          <p style="color:red;" v-if="ifNeedChangeInformation">您所填信息不完整，请完善信息</p>
        </div>
        <div class="container">
          <div class="field">
            <label class="label font_title" style="margin-top:5%">学校</label>
            <div class="columns is-0 is-multiline">
              <div
                class="column is-one-third"
                v-for="(keywords, index) in information.keywords"
                v-bind:key="index"
              >
                <div class="control has-icons-right">
                  <input
                    class="input input_normal"
                    type="text"
                    placeholder="输入学校名称"
                    v-model="information.keywords[index]"
                  />
                  <span class="icon is-small is-right">
                    <i
                      class="fa fa-times-circle fa-x"
                      style="pointer-events: initial;cursor:pointer"
                      @click="deletekeywords(index)"
                    ></i>
                  </span>
                </div>
              </div>
              <div class="column is-one-third" v-if="keywordsLength()<6">
                <i
                  class="fa fa-plus-circle fa-2x addIcon_normal"
                  style="margin-top:5px"
                  @click="addkeywords()"
                ></i>
              </div>
            </div>
          </div>

          <div class="field" style="margin-top:29px">
            <label class="label font_title" style="display: inline-flex;">可选择以下学校</label>
            <div class="select is-small input_normal" style="display:inline-flex;margin-left:10%">
              <select v-model="selectedFirstObject">
                <option
                  v-for="(allkeywords, index) in allkeywords"
                  v-bind:key="index"
                >{{allkeywords.firstSubject}}</option>
              </select>
            </div>
            <div class="tags" style="margin-top:2.5%">
              <span
                class="tag tag_normal"
                v-for="(secondSubject, index) in getSecondSubject()"
                v-bind:key="index"
                @click="addSecondSubject(secondSubject)"
                style="cursor:pointer"
              >{{secondSubject}}</span>
            </div>
          </div>

          <div class="field">
            <label class="label font_title" style="margin-top:5%">学校简介</label>
            <div class="control">
              <textarea
                class="textarea input_normal"
                placeholder="请输入学校简介"
                v-model="information.biography"
                style="height:400px"
              ></textarea>
            </div>
          </div>
        </div>
      </section>
      <footer class="modal-card-foot">
        <button class="button button_action" @click="a()">保存信息</button>
        <button class="button button_normal" @click="b()">取消修改</button>
      </footer>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from "vue-property-decorator"
import "bulma/css/bulma.css"
import HelloWorld from '@/components/HelloWorld.vue'
import Axios from 'axios';

@Component({
    components:{
        HelloWorld
    }
})
export default class UniversityCenter extends Vue {
    @Prop()
    modalCssClass='modal';
    close(){
        this.$emit('close');
    }
    UniversityCenter(){

    }
}
</script>
