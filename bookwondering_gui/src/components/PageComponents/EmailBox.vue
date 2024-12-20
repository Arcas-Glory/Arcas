<template>
    <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
      <li @click="goToEmailDetails" v-for="i in count" :key="i" class="infinite-list-item">{{ i }}</li>
    </ul>
  </template>
  
  <script lang="ts" setup>
  import { ref,onMounted } from 'vue'
  import {useRouter,useRoute} from 'vue-router'
  const count = ref(0)
  const load = () => {
    count.value += 2
  }

  const router = useRouter()
  const route = useRoute()
  const email = ref({})

  onMounted(async () => {
    // 一个方法来根据用户账号获取用户的信箱数据
  });

  const goToEmailDetails = () => {
    router.push({name:'EmailDetail'})
  }

  </script>
  
  <style>
  .infinite-list {
    height: 600px; /* 增加高度到600px */
    padding: 0;
    margin: 0;
    list-style: none;
    overflow: auto; /* 确保滚动条正常显示 */
  }
  .infinite-list .infinite-list-item {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 50px;
    background: var(--el-color-primary-light-9);
    margin: 10px;
    color: var(--el-color-primary);
  }
  .infinite-list .infinite-list-item + .infinite-list-item {
    margin-top: 10px;
  }
  </style>