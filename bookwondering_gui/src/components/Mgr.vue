<template>
  <el-carousel :interval="4000" type="card" height="200px">
    <el-carousel-item v-for="(image,index) in images" :key="index">
        <img :src=image :alt=image>
        <h1>{{ image }}</h1>
    </el-carousel-item>
  </el-carousel>
</template>

<style scoped>
.el-carousel__item h3 {
  color: #475669;
  opacity: 0.75;
  line-height: 200px;
  margin: 0;
  text-align: center;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}
</style>

<script lang="js" setup>
    import { defineProps } from 'vue';

    const props = defineProps({
        images: {
            type:Array,
            default:() => []
        }
    });


    // for(let i = 0; i < props.images.length; i++) {
    //     console.log(props.images[i])
    // }
</script>
