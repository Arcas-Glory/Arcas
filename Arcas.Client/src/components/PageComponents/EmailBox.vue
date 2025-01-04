  <template>
    <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
      <li 
        v-for="(emailItem, index) in emails" 
        :key="index" 
        class="infinite-list-item"
        @click="goToEmailDetails(emailItem)">
        <!-- 使用 el-card 来美化邮件条目 -->
        <el-card class="email-item-card" :body-style="{ padding: '10px' }">
          <div class="email-header">
            <span class="sender-name">{{ emailItem.nickname }}</span>
          </div>
          <div class="email-subject">
            <strong>{{ emailItem.detailsA }}</strong>
          </div>
        </el-card>
      </li>
    </ul>
  </template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useEmailsStore } from '../../stores/emailStore';
import { useAuthStore } from '../../stores/authStore';
import axios from 'axios';

// 邮件数据
const emails = ref([])

const count = ref(3)
const load = () => {
  count.value += 2
}
const emailStore = useEmailsStore()
const router = useRouter()
const authStore = useAuthStore()
const route = useRoute()



// 点击进入邮件详情
const goToEmailDetails = (emailItem: any) => {
  router.push({ name: 'EmailDetail', params: { exchangeID: emailItem.exchangeID } })
}

// 格式化时间
const formatDate = (date: string) => {
  const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit' }
  return new Date(date).toLocaleString('zh-CN', options)
}

const fetchEmails = () => {
    axios.get('http://localhost:5117/api/email/get', {
    params: {
      userID:String(authStore.userID),
      token: authStore.token
    }
  })
  .then(response => {
    console.log('API响应:', response.data);
    
    // 假设后端返回的数据结构中有一个字段叫 emails 包含邮件列表
    emails.value = response.data.emails.map(email => ({
      exchangeID: email.exchangeID || 'None',
      nickname: email.nickname || 'None',
      IDA: email.IDA || 'None',
      IDB: email.IDB || 'None',
      bookID: email.bookID || 'None',
      trackingID: email.trackingID || 'None',
      isApproved: email.isApproved || 'None',
      content: email.content || 'None',
      detailsA: email.detailsA || 'None',
      detailsB: email.detailsB || 'None',
    }));

    emailStore.setEmails(emails.value); // 将数据保存到Pinia store中

    // 延迟打印localStorage确保数据被保存
    setTimeout(() => {
      console.log("本地存储：");
      console.log(emailStore.emails); // 打印本地存储中的数据
    }, 1000); // 延迟1秒
  })
  .catch(error => {
    console.error('请求失败:', error);
  });
};


onMounted(fetchEmails);

</script>

<style scoped>
.infinite-list {
  height: 600px;
  padding: 0;
  margin: 0;
  list-style: none;
  overflow: auto;
}

.infinite-list-item {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 10px 0;
}

.email-item-card {
  display: flex;
  flex-direction: column;
  padding: 10px;
  background-color: var(--el-color-white);
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.email-header {
  display: flex;
  justify-content: space-between;
  font-size: 14px;
  color: var(--el-text-color);
}

.sender-name {
  font-weight: bold;
  color: var(--el-color-primary);
}

.email-time {
  color: var(--el-color-gray-500);
}

.email-subject {
  margin-top: 5px;
  font-size: 16px;
  font-weight: bold;
  color: var(--el-color-primary-dark);
}

.email-item-card:hover {
  background-color: var(--el-color-primary-light-5);
  cursor: pointer;
}
</style>
