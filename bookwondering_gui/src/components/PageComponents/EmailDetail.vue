<template>
    <el-descriptions title="User Info">
      <el-descriptions-item label="Username">kooriookami</el-descriptions-item>
      <el-descriptions-item label="Telephone">18100000000</el-descriptions-item>
      <el-descriptions-item label="Place">Suzhou</el-descriptions-item>
      <el-descriptions-item label="Remarks">
        <el-tag size="small">School</el-tag>
      </el-descriptions-item>
      <el-descriptions-item label="Address">
        No.1188, Wuzhong Avenue, Wuzhong District, Suzhou, Jiangsu Province
      </el-descriptions-item>
    </el-descriptions>
    <el-button type="primary" @click="goToEmailRely">回复</el-button>
  </template>

  <script setup>

  import {ref} from 'vue'
  import {useRouter,useRoute} from 'vue-router'

  const EmailDetail = ref({})
  const router = useRouter()
  const route = useRoute()

  const goToEmailRely = () => {
    router.push({name:'EmailReply'})
  }
  //该页面有一个根据邮件编号获取邮件内容的函数
  </script>
  