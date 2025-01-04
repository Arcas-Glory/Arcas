<template>
    <div>
      <img :src="imageUrl" alt="验证码图片" v-if="imageUrl" @click="getCode" />
      <div v-else>图片加载中...</div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  const imageUrl = ref(null);
  
  const getCode = async () => {
    try {
      const response = await axios({
        url: "http://localhost:5117/Login/GetValidateCodeImages?t=222",
        method: "GET",
        responseType: 'blob' // 重要：确保响应类型为blob
      });
      imageUrl.value = URL.createObjectURL(response.data);
    } catch (error) {
      console.error('Error fetching the image:', error);
    }
  };
  
  onMounted(getCode); // 使用onMounted钩子在组件挂载后调用getCode
  </script>
  
  <style scoped>
  img {
    width: 10%; /* 或者您想要的尺寸 */
    height: auto;
  }
  </style>