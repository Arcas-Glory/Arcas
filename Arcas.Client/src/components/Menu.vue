<template>
  <div style="display: flex; align-items: right; gap: 10px;">
    <HeadPortrait @click="goToUser" />
    <el-button type="primary" @click="goToEmail">信箱</el-button>
    <el-button type="primary" @click="handleLogin">登录</el-button>
    <el-button type="success" @click="handleRegister">注册</el-button>
    <el-button type="success" @click="ReTurn">返回</el-button>
    <FloatingWindow v-if="!isLoginOrRegisterPage" />
  </div>
</template>

<script lang="ts" setup>
import { ref,watch } from 'vue'
import {useRouter} from 'vue-router'
import FloatingWindow from './FloatingWindow.vue';
import HeadPortrait from './HeadPortrait.vue';

const router = useRouter()
const isLogin = ref(false); // 用于跟踪是否登录
const isLoginOrRegisterPage = ref(false);// 用于跟踪是否处于登录或者注册界面

watch(router.currentRoute, (to) => { //路由监听确保上传按钮的可见性
  if (to.name === 'Login' || to.name === 'Register') {
    isLoginOrRegisterPage.value = true;
  } else {
    isLoginOrRegisterPage.value = false;
  }
});

const ReTurn = () => {
  router.push({name:'BookDisplay'})
}

const handleLogin = () => {
  if (isLogin.value) {
    alert('已登录，点击退出登录');
    router.push('/'); // 退出登录后跳转到首页
    isLogin.value = false; // 更新登录状态
  } else {
    router.push({name:'Login'}); // 跳转到登录页面
  }
};

const handleRegister = () => {
  router.push({name:'Register'}); // 跳转到注册页面
};


const goToEmail = () => {
  //转向邮箱的路由
  router.push({name:'EmailBox',params:{userID:'84564'}})
}

const goToUser = () => {
  //转向用户的路由
  router.push({name:'UserDetails',params:{userID:'84564'}});
}

</script>

<style>
.demo-tabs > .el-tabs__content {
  padding: 32px;
  color: #6b778c;
  font-size: 32px;
  font-weight: 600;
}
</style>
