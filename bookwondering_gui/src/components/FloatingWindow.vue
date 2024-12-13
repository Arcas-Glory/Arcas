<template>
    <div style="display: flex; align-items: center">
      <el-popover placement="bottom" :width="800" trigger="click">
        <template #reference>
          <el-button type="success">上传</el-button>
        </template>
        <BookUpLoad />
        <el-text class="mx-1" type="primary">请添加图书截图<br><br></el-text>
        <el-form :inline="true" :model="formInline" class="demo-form-inline">
    <el-form-item label="Approved by">
      <el-input v-model="formInline.user" placeholder="Approved by" clearable />
    </el-form-item>
    <el-form-item label="Activity zone">
      <el-select
        v-model="formInline.region"
        placeholder="Activity zone"
        clearable
      >
        <el-option label="Zone one" value="shanghai" />
        <el-option label="Zone two" value="beijing" />
      </el-select>
    </el-form-item>
    <el-form-item label="Activity time">
      <el-date-picker
        v-model="formInline.date"
        type="date"
        placeholder="Pick a date"
        clearable
      />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">Query</el-button>
    </el-form-item>
  </el-form>
        <el-button type="success">上传该图书</el-button>
      </el-popover>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { reactive } from 'vue';
  import BookUpLoad from './BookUpLoad.vue';

  const formInline = reactive({
    user: '',
    region: '',
    date: '',
    })

    const onSubmit = () => {
    console.log('submit!')
    }
  </script>

<style>
    .demo-form-inline .el-input {
    --el-input-width: 220px;
    }

    .demo-form-inline .el-select {
    --el-select-width: 220px;
    }
</style>