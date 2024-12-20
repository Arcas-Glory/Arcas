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
  import {watch,ref,reactive,provide} from 'vue';
  const router = useRouter()
  const imageList = [
          '../src/assets/images/01.png',
          '../src/assets/images/02.png',
          '../src/assets/images/03.png'
        ]
  const BookImages = [{src:'../src/assets/images/bookImages/魔法少女.jpg',name:'魔法少女',id:'1234',catagory:'现实'},
                      {src:'../src/assets/images/bookImages/恋花梦.jpg',name:'恋花梦',id:'1235',catagory:'浪漫'},
                      {src:'../src/assets/images/bookImages/情缘就要虐.jpg',name:'情缘就要虐',id:'1236',catagory:'历史'},
                      {src:'../src/assets/images/bookImages/时崎狂三的狂乱之舞.jpg',name:'时崎狂三的狂乱之舞',id:'1237',catagory:'奇幻'},
                      {src:'../src/assets/images/bookImages/妖怪许愿店.jpg',name:'妖怪许愿店',id:'1238',catagory:'悬疑'},
                      {src:'../src/assets/images/bookImages/一袖揽风华.jpg',name:'一袖揽风华',id:'1239',catagory:'现实'}]

  const filteredImageGroups = ref([]);
  const imageGroups = ref([]);

  //点击卡片图片触发的事件
  const goToDetails = (image) => {
    console.log("Go to Details")
    router.push({ name: 'BookDetails', params: { id: image.id } });
  };

  const shareData = reactive({item:'现实'})
  provide('shareData',shareData)

  // 根据类别筛选图片
  function getFilteredImages(category) {
    let temp = BookImages.filter((image) => image.catagory === category);
    // console.log('筛选后的图书：')
    // console.log(temp)
    return temp
  }

  //监视器，当在Selector组件中修改shareData.item时会触发
  watch(() => shareData.item, (newItem) => {
    // console.log("oldItem:" + shareData.item + " newItem:" + newItem)
    filteredImageGroups.value = getFilteredImages(newItem);
    imageGroups.value = filteredImageGroups.value.reduce((acc, image, idx) => {
      const groupIndex = Math.floor(idx / 5)
      if (!acc[groupIndex]) {
        acc[groupIndex] = []
      }
      acc[groupIndex].push(image)
      return acc
    }, [])
    // console.log('得到的图书数组为：')
    // console.log(imageGroups.value)
  }, { immediate: true });
</script>
  