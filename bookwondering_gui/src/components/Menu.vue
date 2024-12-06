<template>
  <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick">
    <el-tab-pane label="Login" name="Login">Login</el-tab-pane>
    <el-tab-pane label="Register" name="Register">Register</el-tab-pane>
    <el-tab-pane label="BookDisplay" name="BookDisplay">BookDisplay</el-tab-pane>
  </el-tabs>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import type { TabsPaneContext } from 'element-plus'
import {useRouter} from 'vue-router'

const router = useRouter()
const activeName = ref('Login')

const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(tab.paneName)
  switch (tab.paneName) {
    case 'Menu':
      router.push('/')
      break
    case 'Login':
      router.push('/login')
      break
    case 'Register':
      router.push('/register')
      break
    case 'BookDisplay':
      router.push('/bookdisplay')
    default:
      break
  }
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
