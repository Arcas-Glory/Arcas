<template>
      <el-card class="reply-card">
      <!-- 回复区域 -->
      <div class="response-section">
        <h3>回复借书申请</h3>

        <!-- 留言框 -->
        <el-input
          v-model="message"
          type="textarea"
          :autosize="{ minRows: 2, maxRows: 4 }"
          placeholder="请输入留言（比如申请者通过什么途径支付运费）"
          class="message-input"
        />
      </div>

      <!-- 操作按钮 -->
      <div class="button-group">
        <el-button
          type="success"
          @click="handleAgree"
          class="action-button agree-button"
        >
          同意申请
        </el-button>
        <el-button
          type="danger"
          @click="handleDisagree"
          class="action-button disagree-button"
        >
          拒绝申请
        </el-button>
      </div>
    </el-card>
  </template>
  
  <script lang="ts" setup>
  import { ref,onMounted } from 'vue'
  import {useRouter,useRoute} from 'vue-router'
  import { useAuthStore } from '../../stores/authStore';
  import { ElMessage } from 'element-plus';
  import axios from 'axios';
  let isApproved = false

  const message = ref('') // 留言内容
  const response = ref('') // 用户的回复（同意或不同意）
  // 处理同意申请
  const handleAgree = () => {
    response.value = '申请已同意'
    isApproved = true
    // 在此可以添加调用 API 的逻辑来更新后端数据
    console.log('同意申请', message.value)  // 打印留言内容
    console.log('传入EmailReply的exchangeID：'+ thisExchangeID.value + " 是否同意该请求？：" + isApproved)
    submitReply()
  }

  const route = useRoute()
  const router = useRouter()
  const thisExchangeID = ref()
  const authStore = useAuthStore()

  // 处理拒绝申请
  const handleDisagree = () => {
    response.value = '申请已拒绝'
    // 在此可以添加调用 API 的逻辑来更新后端数据
    console.log('拒绝申请', message.value)  // 打印留言内容
    console.log('传入EmailReply的exchangeID：'+ thisExchangeID.value + " 是否同意该请求？：" + isApproved)
    submitReply()
  }

  onMounted(async () => {
    thisExchangeID.value = route.params.exchangeID
    console.log('exchangeID = ' + thisExchangeID.value)
  })

  const submitReply = async () => {
    try {
      const response = await axios.post('http://localhost:5209/Reply/reply', {
        token: authStore.token,
        userID: authStore.userID,
        exchangeID: thisExchangeID.value,
        detailsB: message.value,
        isAgree: isApproved,
      }, {
        headers: {
          'Content-Type': 'application/json' // 确保请求头正确
        }
      })

      if(response.data.vertify == "success") {
        ElMessage.success('成功回复')
        setTimeout(()=> {
          router.push({name:'BookDisplay'})
        },2000) //延迟几秒后进入主页面
      }
      else {
        ElMessage.error('后端返回vertify不为success')
      }
    } catch (error) {
      ElMessage.error('出现错误：' + error)
    }
  };

  </script>

  <style scoped>
  .reply-card {
    width: 500px;
    margin: 20px auto;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  }

  .response-section {
    margin-bottom: 20px;
    /* margin: 20px; */
  }

  h3 {
    font-size: 18px;
    font-weight: bold;
    color: #333;
    margin-bottom: 10px;
  }

  .message-input {
    width: 100%;
    background-color: #ffffff;
    border-radius: 6px;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
  }

  .button-group {
    display: flex;
    justify-content: space-between;
  }

  .action-button {
    width: 48%;
  }

  .agree-button {
    background-color: #67c23a;
    color: white;
  }

  .disagree-button {
    background-color: #f56c6c;
    color: white;
  }

  button:hover {
    opacity: 0.9;
  }
  </style>

  