<template>
    <el-descriptions title="Customized style list" :column="3" border>
      <el-descriptions-item
        label="Username"
        label-align="right"
        align="center"
        label-class-name="my-label"
        class-name="my-content"
        width="150px"
      >
        kooriookami
      </el-descriptions-item>
      <el-descriptions-item label="Telephone" label-align="right" align="center">
        18100000000
      </el-descriptions-item>
      <el-descriptions-item label="Place" label-align="right" align="center">
        Suzhou
      </el-descriptions-item>
      <el-descriptions-item label="Remarks" label-align="right" align="center">
        <el-tag size="small">School</el-tag>
      </el-descriptions-item>
      <el-descriptions-item label="Address" label-align="right" align="center">
        你好世界
      </el-descriptions-item>
    </el-descriptions>
    <el-button type="primary" @click="BookReturn">归还</el-button>
  </template>

  <script setup>
    import {useRouter, useRoute} from 'vue-router'

    const router = useRouter()
    const route = useRoute()

    const BookReturn = () => {
        //将该图书从私人占有加到公开借阅库中去
    }
  </script>

  <style scoped>
  :deep(.my-label) {
    background: var(--el-color-success-light-9) !important;
  }
  :deep(.my-content) {
    background: var(--el-color-danger-light-9);
  }
  </style>

  