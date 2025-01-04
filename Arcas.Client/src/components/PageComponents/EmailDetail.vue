<template>

    <el-row gutter={20} justify="center" class="user-info-section">
      <el-col :span="8">
        <el-card class="user-info-card">
          <!-- 用户名 -->
          <div class="user-info">
            <el-descriptions title="消息信息" :column="1" border>
              <el-descriptions-item label="用户昵称">
                {{ details.nickname }}
              </el-descriptions-item>
              <el-descriptions-item label="收货地址">
                {{ details.content }}
              </el-descriptions-item>
              <el-descriptions-item label="发起人请求">
                {{ details.detailsA }}
              </el-descriptions-item>
              <el-descriptions-item label="接受者回信">
                {{ details.detailsB }}
              </el-descriptions-item>
              <el-descriptions-item label="这笔漂流是否同意">
                {{ details.isApproved }}
              </el-descriptions-item>
            </el-descriptions>
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-button type="primary" @click="goToEmailRely">回复</el-button>
  </template>

  <script setup>

  import {ref,reactive,onMounted} from 'vue'
  import {useRouter,useRoute} from 'vue-router'
  import { useEmailsStore } from '../../stores/emailStore'
  import { ElMessage } from 'element-plus';

  const router = useRouter()
  const route = useRoute()
  const emailStore = useEmailsStore()
  const thisExchangeID = ref()
  
  const details = ref({
      exchangeID: '',
      nickname: '',
      IDA: '',
      IDB: '',
      bookID: '',
      trackingID: '',
      isApproved: '',
      content: '',
      detailsA: '',
      detailsB: '',
  })

  // 根据路由传进来的exchangeID来查询订单详情
  const GetDetails = () => {
    // console.log("开始寻找")
    console.log("寻找前exchangeID = " + thisExchangeID.value)
    console.log(emailStore.emails)
    emailStore.emails
    const emailDetails = emailStore.emails.find(email => email.exchangeID === Number(thisExchangeID.value));
    if (emailDetails) {
      // 如果找到了对应的 email 对象，可以将其赋值给 userInfo 或者其他响应式数据
      details.value = emailDetails
      ElMessage.success('详情获取成功')
    } else {
      ElMessage.error('没有找到对应的邮件详情')
      console.log(emailDetails)
    }
  } 

  onMounted(async () => {
    if(!emailStore.nowExchangeID) {
      thisExchangeID.value = route.params.exchangeID
      emailStore.setNowExchangeID(thisExchangeID)
      console.log("成功设置emailStore的exchangeID;" + emailStore.nowExchangeID)
    }
    else {
      thisExchangeID.value = emailStore.nowExchangeID
      console.log("刷新,得到的exchangeID:" + emailStore.nowExchangeID)
    }
    console.log("存储的emailStore")
    console.log(emailStore.emails)
    GetDetails()
  })

  const goToEmailRely = () => {
    router.push({name:'EmailReply',params:{exchangeID:details.exchangeID}})
  }
  //该页面有一个根据邮件编号获取邮件内容的函数
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

  </style>
  