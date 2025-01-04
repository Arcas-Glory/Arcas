<template>
  <div style="display: flex; justify-content: center; padding-top: 50px;">
    <el-form
    ref="ruleFormRef"
    :model="ruleForm"
    status-icon
    :rules="rules"
    label-width="auto"
    class="demo-ruleForm"
  >
    <el-form-item label="Username" prop="id">
      <el-input v-model.number=ruleForm.id />
    </el-form-item>
    <el-form-item label="Password" prop="pass">
      <el-input v-model=ruleForm.pass type="password" autocomplete="off" />
    </el-form-item>

    <el-form-item>
      <el-button type="primary" @click="submitForm()">
        Submit
      </el-button>
      <el-button @click="resetForm(ruleFormRef)">Reset</el-button>
    </el-form-item>
  </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import { useAuthStore } from '../../stores/authStore';
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus';
import axios from 'axios'
import { useRouter } from 'vue-router';

const ruleFormRef = ref<FormInstance>()
const authStore = useAuthStore()
const router = useRouter()

const checkId = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('Please input the Id'))
  }
  setTimeout(() => {
      if (value.toString().length > 12) {
        callback(new Error('Id is to long')) // 限制Id的长度
      } else {
        callback()
      }
  }, 1000)
}

const validatePass = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Please input the password'))
  } else {
    callback()
  }
}

const ruleForm = reactive({
  pass: '',
  id: '',
})

const rules = reactive<FormRules<typeof ruleForm>>({
  pass: [{ validator: validatePass, trigger: 'blur' }],
  id: [{ validator: checkId, trigger: 'blur' }],
})

const submitForm = async () => {
  try {
    console.log(ruleForm.id + " | " + ruleForm.pass)
    const response = await axios.post('http://localhost:5117/api/auth/login', {
      username: String(ruleForm.id),
      password: String(ruleForm.pass),
    }, {
      headers: {
        'Content-Type': 'application/json' // 确保请求头正确
      }
    })
    console.log(ruleForm.id + " | " + ruleForm.pass)
      if (response.data.vertify === 'success') {
      ElMessage.success('登录成功！');
      authStore.setUser(response.data.token,response.data.userID,ruleForm.id)
      setTimeout(()=> {
          router.push({name:'BookDisplay'})
        },1000)
    } else {
      ElMessage.error('登录失败：' + response.data.vertify);
    }
  } catch (error) {
    ElMessage.error('登录失败，请检查网络或后端服务');
    console.error(error);
  }
};


const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}
</script>
