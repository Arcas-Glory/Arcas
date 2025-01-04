<template>
  <el-form :model="formData" :rules="rules" ref="formRef" label-width="120px" class="form-container">
    <!-- 收货地址字段 -->
    <el-form-item label="收货地址" prop="content">
      <el-input
        v-model="formData.content"
        type="textarea"
        rows="4"
        placeholder="请输入收货地址"
        maxlength="500" />
    </el-form-item>

    <!-- 申请内容字段 -->
    <el-form-item label="申请内容" prop="detailsA">
      <el-input
        v-model="formData.detailsA"
        type="textarea"
        rows="6"
        placeholder="请输入申请内容"
        maxlength="1000" />
    </el-form-item>

    <el-form-item>
      <el-button type="primary" @click="submitForm">提交申请</el-button>
    </el-form-item>
  </el-form>
</template>

<script setup>
  import {useRouter, useRoute} from 'vue-router';
  import {ref, onMounted} from 'vue';
  import { ElForm, ElFormItem, ElInput, ElButton,ElMessage } from 'element-plus';
  import { useAuthStore } from '../../stores/authStore';
  import axios from 'axios'; // 引入axios
  const authstore = useAuthStore();

  const route = useRoute();
  const bookId = route.params.id;
  const token = authstore.token;
  const userID = authstore.userID;

  //测试用记得改
  // const bookId = "1"
  // const token = "123456"
  // const userID="1"


  // 定义表单数据
  const formData = ref({
    content: '',
    detailsA: ''
  });

  // 定义表单验证规则
  const rules = {
    content: [{ required: true, message: '收货地址不能为空', trigger: 'blur' }],
    detailsA: [{ required: true, message: '申请内容不能为空', trigger: 'blur' }]
  };

  // 提交表单
  const submitForm = async() => {
    try {
      const response = await axios.post('http://localhost:5117/api/email/apply', {
        token: authstore.token,
        userID: authstore.userID,
        bookID: route.params.id,
        content: formData.value.content,
        detailsA: formData.value.detailsA
      },{
      headers: {
        'Content-Type': 'application/json' // 确保请求头正确
      }
    });

      if (response.data.vertify === 'success') {
        // 申请成功后的处理逻辑
        // console.log("response.data.vertify")
        ElMessage.success('申请成功！')
      } else {
        // 申请失败的处理逻辑
        ElMessage.error('申请失败，请稍后再试')
        console.log(response)
        console.log({
          token: token,
          userID: userID,
          bookID: route.params.id,
          content: formData.value.content,
          detailsA: formData.value.detailsA
        })
      }
    } catch (error) {
      console.error('申请失败:', error)
      ElMessage.error('请求失败，请检查网络连接')
    }
  // };
  };


  //onMounted(async () => {
    /* 模拟发送借书请求的API
    {
      bookID: bookId,
      content: formData.content,
      detailsA: formData.detailsA
    }
    */
  //});
</script>

<style scoped>
/* 表单容器样式 */
.form-container {
  width: 60%;
  margin: 0 auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* 每个表单项的底部间距 */
.el-form-item {
  margin-bottom: 20px;
}

/* 输入框背景色和圆角 */
.el-input {
  background-color: #f3f3f3;
  border-radius: 4px;
}

/* 提交按钮样式 */
.el-button {
  width: 100%;
  background-color: #409EFF;
  border-color: #409EFF;
  color: white;
  font-weight: bold;
  border-radius: 4px;
}

/* 提交按钮的悬浮效果 */
.el-button:hover {
  background-color: #66b1ff;
  border-color: #66b1ff;
}
</style>


