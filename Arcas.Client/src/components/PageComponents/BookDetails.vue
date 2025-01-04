   <template>
    <el-card class="book-detail-card">
      <!-- 书籍详情 -->
      <el-descriptions title="书籍详情" border>
        <el-descriptions-item
          :rowspan="2"
          :width="140"
          label="封面"
          align="center"
        >
          <el-image
            style="width: 120px; height: 180px; object-fit: cover"
            :src="book.cover"
            alt="书籍封面"
          />
        </el-descriptions-item>
        <el-descriptions-item label="书名">{{ book.title }}</el-descriptions-item>
        <el-descriptions-item label="书籍ISBN">{{ book.isbn }}</el-descriptions-item>
        <el-descriptions-item label="拥有用户">{{ book.nickname }}</el-descriptions-item>
        <el-descriptions-item label="标签">
            <el-tag size="small" type="info">{{ book.category }}</el-tag>
        </el-descriptions-item>
      </el-descriptions>
  
      <!-- 发起借阅按钮 -->
      <div class="button-container">
        <el-button
          color="#626aef"
          size="large"
          @click="goToBorrowPage"
          class="borrow-button"
        >
          发起借阅
        </el-button>
      </div>
  
      <!-- 评论区 -->
      <el-card class="comment-section" shadow="hover">
        <h3>评论区</h3>
        <comment-list :comments="book.comments" />
      </el-card>
    </el-card>
  </template>
  
  <script lang="ts" setup>
  import { ref, onMounted } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import {ElMessage} from 'element-plus'
  import CommentList from '../CommentList.vue';
  import axios from 'axios';
  
   const book = ref({
    vertify:'',
    title: '',
    bookID:'',
    isbn: '',
    nickname:'',
    state:'',
    category:'',
    cover: '',
    comments: [
      { username: 'Alice', avatar: '../src/assets/images/gbc.jpg', content: '这本书很好，值得一读。' },
      { username: 'Bob', avatar: '../src/assets/images/gbc.jpg', content: '有些部分看起来有点枯燥。' }
    ]
  });
  
  const route = useRoute();
  const router = useRouter();
  const thisBookId = ref();
  const categoryOptions = [
    {
      value: 1,
      label: '现实',
    },
    {
      value: 2,
      label: '浪漫',
    },
    {
      value: 3,
      label: '历史',
    },
    {
      value: 4,
      label: '奇幻',
    },
    {
      value: 5,
      label: '悬疑',
    },
  ];

  function getCategoryLabel(categoryValue) {
    const category = categoryOptions.find(item => item.value === categoryValue);
    return category ? category.label : '未知分类'; // 如果没有找到对应的分类，返回 '未知分类'
  }

  const GetBookDetails = async () => {
    thisBookId.value = route.params.id
    await axios.get('http://localhost:5117/api/book/get', {
      params: {
          bookID: String(thisBookId.value),
        }
    })
   .then(response => {
      console.log('API响应:', response.data);
      const verifyResult = response.data.vertify;
      book.value = response.data
      book.value.category = getCategoryLabel(response.data.category)
      console.log(book.value)
      if(!verifyResult) console.error('请求失败')
      else ElMessage.success('成功获取图书详情')
    })
    .catch(error => {
      console.error('出现错误:', error);
      // 假设在请求失败时设置验证结果
      ElMessage.error('获取图书详情出现错误')
    });
  };

  onMounted(() => {
    GetBookDetails()
  });
  
  const goToBorrowPage = () => {
    router.push({ name: 'BookBorrowPage', params: { id: thisBookId.value } });
  };
  </script>
  
  <style scoped>
  .book-detail-card {
    max-width: 900px;
    margin: 20px auto;
    padding: 20px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    background-color: #fff;
  }
  
  .button-container {
    text-align: center;
    margin: 20px 0;
  }
  
  .borrow-button {
    background-color: #626aef;
    color: white;
    border: none;
    font-weight: bold;
  }
  
  .comment-section {
    margin-top: 20px;
    padding: 15px;
    border-radius: 5px;
  }
  
  .comment-section h3 {
    margin-bottom: 10px;
    font-size: 18px;
    color: #333;
  }
  
  .el-tag {
    margin-right: 8px;
  }
  </style>
  