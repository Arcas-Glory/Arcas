<template>
  <el-descriptions title="Width horizontal list" border>
    <el-descriptions-item
      :rowspan="2"
      :width="140"
      label="Photo"
      align="center"
    >
      <el-image
        style="width: 100px; height: 100px"
        src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"
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
    <el-button color="#626aef">发起借阅</el-button>
    <el-button color="#626aef">结束借阅</el-button>
  </div>
</template>
  
<script lang="ts" setup>
import { defineProps, onMounted,ref } from 'vue';
import { useRoute } from 'vue-router';

const props = defineProps({
  book: {
    type: Object,
    default: () => ({}),
  },
});

const route = useRoute();
const bookId = route.params.id;

onMounted(async () => {
  // 假设有一个方法来根据ID获取书籍详情
  console.log(bookId)
  // const bookDetails = await getBookDetailsById(bookId);
  // props.book = bookDetails;
});

</script>
  