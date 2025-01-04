<template>
    <div class="selector" style="display: flex; align-items: right; gap: 10px;">
      <el-select
        v-model="shareData.item"
        placeholder="选择图书类别"
        size="large"
        style="width: 240px"
        @onchange="Print"
      >
        <el-option
          v-for="item in options"
          :key="item.value"
          :label="item.label"
          :value="item.value"
        />
      </el-select>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { ref , inject} from 'vue'
  
  const options = [
    {
      value: 1,
      label: '现实',
    },
    {
      value: 2,
      label: '浪漫',
    },
    {
      value: 3,
      label: '历史',
    },
    {
      value: 4,
      label: '奇幻',
    },
    {
      value: 5,
      label: '悬疑',
    },
  ]
  const shareData = inject('shareData')
  console.log('Selector:' + shareData.item)

  const Print = () => {
    console.log('Selector:' + shareData.item)
  }
  </script>