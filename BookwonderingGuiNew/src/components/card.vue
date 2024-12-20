<template>
    <el-card @click="" style=" width: 240px; height: 320px; justify-content: center;align-items: center;">
      <template #header>{{ image.title }}</template>
      <el-image :src="image.cover" style="width:90%; height: auto" />
    </el-card>
</template>

<script setup>
const props = defineProps({
  image: {
    type: Object,
    default: ()=>({})
  }
});

</script>
  