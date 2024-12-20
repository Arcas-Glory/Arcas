<template>
    <el-row gutter={20} justify="center" class="user-info-section">
      <el-col :span="8">
        <el-card class="user-info-card">
          <!-- 用户名 -->
          <div class="user-info">
            <el-descriptions title="Customized style list" :column="1" border>
              <el-descriptions-item label="借阅者">
                {{ bookDetail.username }}
              </el-descriptions-item>
              <el-descriptions-item label="电话">
                {{ bookDetail.telephone }}
              </el-descriptions-item>
              <el-descriptions-item label="评论">
                <el-input
                  v-model="remarks"
                  type="textarea"
                  :autosize="{ minRows: 2, maxRows: 4 }"
                  placeholder="请输入留言（可选）"
                  class="message-input"
                />
            </el-descriptions-item>
            </el-descriptions>
            
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-button type="primary" @click="ReturnBook">归还</el-button>
  </template>

  <script setup>
    import {useRouter, useRoute } from 'vue-router'
    import {watch,ref,reactive,provide, onMounted} from 'vue';
    import { useAuthStore } from '../../stores/authStore';
    import axios from 'axios'

    const router = useRouter()
    const route = useRoute()
    const remarks = ref()
    const authStore = useAuthStore()
    const thisBookID = route.params.bookID
    console.log('此图书的ID为：' + thisBookID)
    import { ElMessage } from 'element-plus';

    // 假设从某个地方获取用户信息
    const bookDetail = reactive({
      username: 'kooriookami',
      telephone: '18100000000',
      place: 'Suzhou',
      remarks: 'School',
      address: 'No.1188, Wuzhong Avenue, Wuzhong District, Suzhou, Jiangsu Province',
      avatar: '../src/assets/images/gbc.jpg'
      //头像之后需要改
    });
    //归还图书的前后端交互API

    const ReturnBook = async () => {
      try {
        const response = await axios.post('http://localhost:5209/api/Auth/ReturnBook', {
          userID: authStore.userID,
          token: authStore.token,
          commentContent: remarks.value,
          bookID:thisBookID
        }, {
          headers: {
            'Content-Type': 'application/json' // 确保请求头正确
          }
        })
        if (response.data.vertify === 'successful') {
          ElMessage.success('提交成功！');
          console.log("返回数据："+response.data.commentContent)
        } else {
          ElMessage.error('提交失败：' + response.data.vertify);
        }
      } catch (error) {
        ElMessage.error('登录失败，请检查网络或后端服务');
        console.error(error);
      }
    };

    
  </script>

  <style scoped>

  /* 用户信息卡片样式 */
  .user-info-section {
    margin-top: 30px;
    padding: 20px;
  }

  .user-info-card {
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }

  /* 用户信息内容 */
  .user-info {
    margin-top: 10px;
  }


  :deep(.my-label) {
    background: var(--el-color-success-light-9) !important;
  }
  :deep(.my-content) {
    background: var(--el-color-danger-light-9);
  }
  </style>

  