<template>
  <el-carousel :interval="4000" type="card" height="200px">
    <el-carousel-item v-for="(image,index) in images" :key="index">
        <img :src="image">
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

.el-carousel__item img {
  width: 100%;
  height: 100%;
  object-fit: cover;
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

</script>
