<template>
  <el-upload 
    action="#" 
    list-type="picture-card" 
    :auto-upload="false" 
    :file-list="fileList" 
    @change="handleFileChange" >
    <el-icon><Plus /></el-icon>

    <template #file="{ file }">
      <div>
        <img class="el-upload-list__item-thumbnail" :src="file.url" alt="" />
        <span class="el-upload-list__item-actions">
          <span
            class="el-upload-list__item-preview"
            @click="handlePictureCardPreview(file)"
          >
            <el-icon><zoom-in /></el-icon>
          </span>
          <span
            v-if="!disabled"
            class="el-upload-list__item-download"
            @click="handleDownload(file)"
          >
            <el-icon><Download /></el-icon>
          </span>
          <span
            v-if="!disabled"
            class="el-upload-list__item-delete"
            @click="handleRemove(file)"
          >
            <el-icon><Delete /></el-icon>
          </span>
        </span>
      </div>
    </template>
  </el-upload>

  <el-dialog v-model="dialogVisible">
    <img w-full :src="dialogImageUrl" alt="Preview Image" />
  </el-dialog>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { Delete, Download, Plus, ZoomIn } from '@element-plus/icons-vue'

import type { UploadFile } from 'element-plus'

const dialogImageUrl = ref('')
const dialogVisible = ref(false)
const disabled = ref(false)
const fileList = ref<UploadFile[]>([])   //存储已上传文件的列表
const emit = defineEmits()

const handleRemove = (file: UploadFile) => {
  console.log(fileList.value)
  const index = fileList.value.findIndex(f => f.uid == file.uid)
  console.log('index = ' + index)
  if (index !== -1) {
    fileList.value.splice(index, 1)
  }
  // 强制刷新响应式数据
  fileList.value = [...fileList.value]
  console.log(fileList)
}

const handlePictureCardPreview = (file: UploadFile) => {
  dialogImageUrl.value = file.url! // 设置预览图像的URL
  dialogVisible.value = true
}

const handleDownload = (file: UploadFile) => {
  console.log(file)
  // 下载逻辑（如果需要）
}

const handleFileChange = (file: UploadFile) => {
  console.log('添加')
  console.log(file)

  // 通过 URL.createObjectURL 获取文件的临时 URL
  const fileUrl = URL.createObjectURL(file.raw)
  console.log("上传文件的URL：" + fileUrl)
  emit('bookCoverName', file.name)
  console.log("上传文件的名字：" + file.name)
  file.url = fileUrl // 将 URL 设置到 file 对象中

  // 将文件添加到 fileList 中
  fileList.value.push(file)
  
}
</script>
