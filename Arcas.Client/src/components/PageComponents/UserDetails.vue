  <template>
    <el-row gutter={20} justify="center" class="user-info-section">
      <el-col :span="8">
        <el-card class="user-info-card">
          <!-- 头像 -->
          <div class="avatar-container">
            <el-image :src="userInfo.avatar" alt="User Avatar" class="user-avatar" />
            <div class="edit-avatar">
              <el-button icon="el-icon-edit" size="mini" @click="editAvatar" class="edit-avatar-button">
                修改头像
              </el-button>
            </div>
          </div>
          <!-- 用户名 -->
          <div class="user-info">
            <el-descriptions title="User Information" :column="1" border>
              <el-descriptions-item label="Username">
                {{ authStore.username }}
              </el-descriptions-item>
              <el-descriptions-item label="Remarks">
                <el-tag size="small">看书发烧友</el-tag>
              </el-descriptions-item>
            </el-descriptions>
            
          </div>
        </el-card>
      </el-col>

    </el-row>

    <!-- 借阅书籍展示部分 -->
    <el-text class="mx-1" type="danger">正在借阅的图书</el-text>
    <Selector />
    <el-row class="center-row" :gutter="15" v-for="(imageGroup, index) in imageGroups" :key="index">
      <el-col :span="4" v-for="image in imageGroup" :key="image.id">
        <Card @click.native="goToDetails(image)" :image="image" />
      </el-col>
    </el-row>
  </template>
  
  <script setup>
    import Card from '../card.vue';
    import Selector from '../Selector.vue';
    import { useRouter } from 'vue-router';
    import {watch,ref,reactive,provide,onMounted} from 'vue';
    import { useAuthStore } from '../../stores/authStore';
    import { ElMessage } from 'element-plus';
    import axios from 'axios';
    const BookImages = ref([])
    const filteredImageGroups = ref([])
    const imageGroups = ref([])
    const router = useRouter()
    const authStore = useAuthStore()

    // 假设从某个地方获取用户信息
    const userInfo = reactive({
      username: '',
      telephone: '',
      place: '',
      remarks: '',
      address: '',
      avatar: '../src/assets/images/gbc.jpg'
      //头像之后需要改
    });

    //点击卡片图片触发的事件
    const goToDetails = (image) => {
      console.log("Go to Details")
      router.push({ name: 'BookReturnPage', params:{bookID:'84564'} })
    };

    //监听选择的类别变化
    const shareData = reactive({item:'现实'})
    provide('shareData',shareData)

    // 根据类别筛选图片
    function getFilteredImages(category) {
      let temp = BookImages.value.filter((image) => Number(image.category) === Number(category))
      // console.log('筛选后的图书：')
      // console.log(temp)
      return temp
    }

    //监视器，当在Selector组件中修改shareData.item时会触发
    watch(() => shareData.item, (newItem) => {
      // console.log("oldItem:" + shareData.item + " newItem:" + newItem)
      filteredImageGroups.value = getFilteredImages(newItem);
      console.log(BookImages.value)
      console.log('筛选后的图书为：' + newItem)
      console.log(filteredImageGroups.value)
      imageGroups.value = filteredImageGroups.value.reduce((acc, image, idx) => {
        const groupIndex = Math.floor(idx / 5)
        if (!acc[groupIndex]) {
          acc[groupIndex] = []
        }
        acc[groupIndex].push(image)
        return acc
      }, [])
      // console.log('得到的图书数组为：')
      // console.log(imageGroups.value)
    }, { immediate: true });

    const GetMyBooks = async () => {
      try {
        const response = await axios.get('http://localhost:5117/api/Book/getmine', {
          params: { userID: Number(authStore.userID) }, // 确保 userID 是查询参数
          headers: { 'token': String(authStore.token) } // 确保 token 是请求头
        });
        console.log('API响应:', response.data);
        const verifyResult = response.data.vertify;
        BookImages.value = response.data.bookLists;
        console.log(BookImages.value);
        if (!verifyResult) ElMessage.error('请求失败verifyResult = :' + verifyResult);
        else {
          ElMessage.success('成功获取图书详情');
          console.log(BookImages.value)
        }
      } catch (error) {
        ElMessage.error('获取图书详情出现错误:' + error);
      }
    };

    onMounted(async() => {
      await GetMyBooks();
    });

    // 修改头像的功能
    const editAvatar = () => {
      alert('修改头像功能');
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

  /* 头像容器 */
  .avatar-container {
    position: relative;
    text-align: center;
    width: 100%;
    height: 100%;
  }

  .user-avatar {
    border-radius: 50%;
    border: 4px solid #dcdfe6;
    width: 100px; /* 限定宽度 */
    height: 100px; /* 限定高度 */
    object-fit: cover; /* 保持比例且裁剪超出的部分 */
  }

  .edit-avatar {
    position: absolute;
    bottom: 10px;
    right: 10px;
  }

  .edit-avatar-button {
    background-color: rgba(255, 255, 255, 0.7);
    border-color: rgba(255, 255, 255, 0.7);
    font-size: 12px;
  }

  /* 用户信息内容 */
  .user-info {
    margin-top: 20px;
  }

  .el-descriptions-item {
    font-size: 14px;
  }

  /* 地址信息 */
  .address-info {
    margin-top: 20px;
  }

  /* 图书展示 */
  .center-row {
    margin-top: 20px;
  }

  .mx-1 {
    font-weight: bold;
    font-size: 16px;
  }
  </style>