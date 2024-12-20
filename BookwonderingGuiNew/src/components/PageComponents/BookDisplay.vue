<template>
  <Mgr :images=imageList />
  <br>
  <Selector />
  <br>
  <el-row class="center-row" :gutter="15" v-for="(imageGroup, index) in imageGroups" :key="index">
    <el-col  :span="4" v-for="image in imageGroup" :key="image.id">
      <Card @click.native="goToDetails(image)" :image="image" />
    </el-col>
  </el-row>
</template>

<style>
  .center-row{
    margin-bottom: 16px;
    justify-content: center;
    align-items: center;
  }
</style>

<script setup lang="js">
  import Mgr from '../Mgr.vue';
  import Card from '../card.vue';
  import Selector from '../Selector.vue';
  import { useRouter } from 'vue-router';
  import {watch,ref,reactive,provide, onMounted} from 'vue';
  import { ElMessage } from 'element-plus';
  import axios from 'axios';

  const router = useRouter()
  const imageList = [
          '../src/assets/images/01.png',
          '../src/assets/images/02.png',
          '../src/assets/images/03.png'
  ]
  const BookImages = reactive([])

  const filteredImageGroups = ref([]);
  const imageGroups = ref([]);

  //点击卡片图片触发的事件
  const goToDetails = (image) => {
    console.log("Go to Details")
    router.push({ name: 'BookDetails', params: { id: image.bookID } });
  };

  const shareData = reactive({item:''})
  provide('shareData',shareData)

  const GetAllBooks = async () => {
    try {
      const response = await axios.get('http://localhost:5209/api/Books/getAllBooks');
      console.log('API响应:', response.data);

      // 确保返回的数据是一个数组，且每个字段有默认值
      BookImages.length = 0; // 清空现有数组数据
      response.data.forEach(book => {
        BookImages.push({
          bookID:book.bookID || 'None',
          title: book.title || 'None',
          ISBN: book.ISBN || 'None',
          bookState: book.bookState || 'None',
          cover: book.cover || 'None',
          category: book.category || 'None',
        });
      });
      ElMessage.success('成功从后端获取书库');
      console.log('BookImages已更新:');
      console.log(BookImages);
    } catch (error) {
      ElMessage.error('出现错误：' + error);
      console.log(error);
    }
  };

  onMounted(async () => {
    // 等待GetAllBooks完成
    await GetAllBooks();
    
    // GetAllBooks执行完后，再修改shareData.item
    shareData.item = '现实';
  });

  // 修改getFilteredImages方法，确保它能处理空数组
  function getFilteredImages(category) {
    if (!Array.isArray(BookImages)) {
      console.error('BookImages is not an array');
      return [];
    }

    // 确保 BookImages 已经加载，并且 category 参数是有效的
    let temp = BookImages.filter(image => image.category === category);
    console.log('筛选后的图书：')
    console.log(temp)
    console.log('BookImages:')
    console.log(BookImages)
    return temp;
  }

  // 监视器，当在Selector组件中修改shareData.item时会触发
  watch(() => shareData.item, (newItem) => {
    // 如果 BookImages 还未初始化，直接返回
    if (!BookImages.length) {
      console.warn('BookImages还未从后端加载');
      return;
    }

    filteredImageGroups.value = getFilteredImages(newItem);

    // 将图书按每组5个进行分组
    imageGroups.value = filteredImageGroups.value.reduce((acc, image, idx) => {
      const groupIndex = Math.floor(idx / 5);
      if (!acc[groupIndex]) {
        acc[groupIndex] = [];
      }
      acc[groupIndex].push(image);
      return acc;
    }, []);

    console.log('ImageGroup:');
    console.log(imageGroups.value);
  }, { immediate: true });
</script>
  