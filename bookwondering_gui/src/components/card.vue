<template>
    <el-card @click="" style=" width: 240px; height: 320px; justify-content: center;align-items: center;">
      <template #header>{{ image.name }}</template>
      <el-image :src="image.src" style="width:90%; height: auto" />
    </el-card>
</template>

<script setup>
import { defineProps } from 'vue';
const props = defineProps({
  image: {
    type: Object,
    default: ()=>({})
  }
});

</script>
  