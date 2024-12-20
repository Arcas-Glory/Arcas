<template>
  <div class="upload-container">
    <el-popover placement="bottom" :width="800" trigger="click">
      <template #reference>
        <el-button type="success">上传</el-button>
      </template>

      <!-- 图书上传组件 -->
      <BookUpLoad @bookCoverName="GetBookCover" />

      <!-- 提示信息 -->
      <el-text class="mx-1" type="primary">请添加图书截图<br><br></el-text>

      <!-- 图书信息表单 -->
      <el-form :inline="true" :model="formInline" class="book-form">
        <el-form-item label="书名">
          <el-input v-model="formInline.title" placeholder="请输入书名" clearable />
        </el-form-item>

        <el-form-item label="ISBN">
          <el-input v-model="formInline.isbn" placeholder="请输入ISBN" clearable />
        </el-form-item>

        <el-form-item label="作者">
          <el-input v-model="formInline.author" placeholder="请输入作者" clearable />
        </el-form-item>

        <el-form-item label="出版社">
          <el-input v-model="formInline.publisher" placeholder="请输入出版社" clearable />
        </el-form-item>

       <!-- 图书类别选择：单选框 -->
       <el-form-item label="图书类别">
          <el-radio-group v-model="formInline.category">
            <el-radio :label="'1'">现实</el-radio>
            <el-radio :label="'2'">浪漫</el-radio>
            <el-radio :label="'3'">历史</el-radio>
            <el-radio :label="'4'">奇幻</el-radio>
            <el-radio :label="'5'">悬疑</el-radio>
          </el-radio-group>
        </el-form-item>
        <br>

        <!-- 上传按钮 -->
        <el-form-item>
          <el-button type="primary" @click="handleUpload">上传该图书</el-button>
        </el-form-item>
      </el-form>
    </el-popover>
  </div>
</template>

<script lang="ts" setup>
  import { reactive,ref } from 'vue'
  import BookUpLoad from './BookUpLoad.vue'
  import { useAuthStore } from '../stores/authStore';
  import { ElMessage } from 'element-plus';
  import axios from 'axios'
  const authstore = useAuthStore()
  const bookCover = ref()

  const formInline = reactive({
    title: '',
    isbn: '',
    author: '',
    publisher: '',
    category: ''  // 新增字段保存类别
  })

  const GetBookCover = (data) => {
    bookCover.value = '../src/assets/images/bookImages/' + data
    console.log("上传文件的路径：" + bookCover.value)
  }

  // 将 handleUpload 函数声明为 async
  const handleUpload = async () => {
    if (!formInline.title || !formInline.isbn || !formInline.author || !formInline.publisher||!formInline.category||!bookCover.value) {
      return alert('请填写完整的书籍信息')
    }
    try {
      const response = await axios.post('http://localhost:5209/Books/upload', {
        userID:authstore.userID ,
        token: authstore.token,
        title: formInline.title,
        ISBN: formInline.isbn,
        cover:bookCover.value,
        author:formInline.author,
        category: formInline.category  // 传递类别字段（数字字符）
    }, {
      headers: {
        'Content-Type': 'application/json' // 确保请求头正确
      }
    });
      // 处理返回结果
      const vertify = response.data;
      if (vertify === 'success') {
        ElMessage.success('图书上传成功！')
      } else {
        ElMessage.error('后端未接受')
      }
    } catch (error) {
      console.log('上传失败:' + error);
      ElMessage.error('上传失败：' + error)
    }

    // 这里可以处理上传的逻辑
    console.log('上传书籍信息:', formInline)
    console.log('上传的封面:', '书籍封面文件')
  }
</script>

<style scoped>
.upload-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.book-form {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 800px;
  margin-top: 20px;
}

.book-form .el-form-item {
  margin-bottom: 15px;
}

.book-form .el-input {
  width: 300px;
}

.mx-1 {
  margin-top: 10px;
  font-size: 14px;
  color: #555;
}
</style>
