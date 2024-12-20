<template>
  <el-descriptions title="书籍详情" border>
    <el-descriptions-item
      :rowspan="2"
      :width="140"
      label="Photo"
      align="center"
    >
      <el-image
        style="width: 100px; height: 100px"
        :src="book.image"
      />
    </el-descriptions-item>
    <el-descriptions-item label="书名">{{ book.name }}</el-descriptions-item>
    <el-descriptions-item label="书籍ISBN">{{ book.ISBN }}</el-descriptions-item>
    <el-descriptions-item label="上传时间">{{ book.upLoadTime }}</el-descriptions-item>
    <el-descriptions-item label="标签">
        <div v-for="(tag,index) in book.tags" key="index">
            <el-tag size="small" >{{ tag }}</el-tag>
        </div>
      <el-tag size="small">School</el-tag>
    </el-descriptions-item>
    <el-descriptions-item label="评价">
      No.1188, Wuzhong Avenue, Wuzhong District, Suzhou, Jiangsu Province
    </el-descriptions-item>
  </el-descriptions>
  <div class="button">
    <el-button color="#626aef" @click="goToBorrowPage">发起借阅</el-button>
  </div>
</template>
  
<script lang="ts" setup>
import { defineProps, onMounted,ref } from 'vue';
import { useRoute,useRouter } from 'vue-router';

const book = ref({}) //响应式对象，用于接收后端发送过来图书数据

const route = useRoute();
const router = useRouter();
const bookId = route.params.id;

onMounted(async () => {
  // 一个方法来根据ID获取书籍详情
  console.log(bookId)
  // const bookDetails = await getBookDetailsById(bookId);
  // props.book = bookDetails;
  //将获取到的数据放入book对象中存放
});

const goToBorrowPage = () => {
  router.push({ name: 'BookBorrowPage', params: { id: bookId } });
  
  //转到借阅图书界面的路由
}

</script>
  