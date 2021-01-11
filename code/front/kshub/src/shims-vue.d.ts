declare module '*.vue' {
  import Vue from 'vue';
  export default Vue;
}
declare module '@/src/views/*' {
  import Vue from 'vue'
  // noinspection JSDuplicatedDeclaration
  export default Vue
}

declare module '@/src/components/*' {
  import Vue from 'vue'
  // noinspection JSDuplicatedDeclaration
  export default Vue
}
declare module '@/src/components/menu' {
  import Vue from 'vue'
  // noinspection JSDuplicatedDeclaration
  export default Vue
}