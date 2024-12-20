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
              <el-descriptions-item label="联系电话">
                {{ details.telephone }}
              </el-descriptions-item>
              <el-descriptions-item label="收货地址">
                {{ details.content }}
              </el-descriptions-item>
              <el-descriptions-item label="详情">
                {{ details.detailsA }}
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

  const router = useRouter()
  const route = useRoute()
  const emailStore = useEmailsStore()
  const thisExchangeID = ref()
  console.log("exchangeID = " + thisExchangeID.value)
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
      telephone: ''
  })

  // 根据路由传进来的exchangeID来查询订单详情
  const GetDetails = () => {
    console.log("开始寻找")
    const emailDetails = emailStore.emails.find(email => email.exchangeID === thisExchangeID.value);
    if (emailDetails) {
      // 如果找到了对应的 email 对象，可以将其赋值给 userInfo 或者其他响应式数据
      details.value = emailDetails
      console.log('成功根据Store数据赋值')
    } else {
      console.log('没有找到对应的邮件详情');
      alert('未找到对应详情')
    }
  } 

  onMounted(async () => {
    console.log('本地存储中的 emails: ', localStorage.getItem('emails'))
    console.log("Store存储：");
    console.log(emailStore.nowExchangeID); 
    if(!emailStore.nowExchangeID) {
      thisExchangeID.value = route.params.exchangeID
      emailStore.setNowExchangeID(thisExchangeID)
      console.log("成功设置emailStore的exchangeID：" + emailStore.nowExchangeID)
    }
    else {
      thisExchangeID.value = emailStore.nowExchangeID
      console.log("刷新，得到的exchangeID：" + emailStore.nowExchangeID)
    }
    console.log('加载得到的exchangeID：' + thisExchangeID.value)
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
  