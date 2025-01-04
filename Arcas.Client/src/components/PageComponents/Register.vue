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
    <el-form-item label="username" prop="id">
      <el-input v-model.number="ruleForm.id" />
    </el-form-item>
    <el-form-item label="Password" prop="pass">
      <el-input v-model="ruleForm.pass" type="password" autocomplete="off" />
    </el-form-item>
    <el-form-item label="Confirm" prop="checkPass">
      <el-input v-model="ruleForm.checkPass" type="password" autocomplete="off" />
    </el-form-item>
    <el-form-item label="nickname" prop="name">
      <el-input v-model.number="ruleForm.name" />
    </el-form-item>
    <!-- <el-form-item label="Mobile" prop="mobile">
      <el-input v-model.number="ruleForm.mobile" />
    </el-form-item> -->

    <el-form-item>
      <el-button ref="buttonRef" type="primary" @click="submitForm(ruleFormRef)">
        Submit
    </el-button>

    <el-popover
    ref="popoverRef"
    :virtual-ref="buttonRef"
    trigger="click"
    title="善意的提醒"
    virtual-triggering
  >
    <span> 提交成功 </span>
    </el-popover>

      
    <el-button @click="resetForm(ruleFormRef)">Reset</el-button>
    </el-form-item>
  </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref, unref } from 'vue'
import type { FormInstance, FormRules } from 'element-plus'
import { ClickOutside as vClickOutside } from 'element-plus'
import axios from 'axios'
import { useRouter,useRoute } from 'vue-router'
import { ElMessage } from 'element-plus';

const ruleFormRef = ref<FormInstance>()
const router = useRouter()
const route = useRoute()

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

const checkName = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('Please input the Id'))
  }
  if(value.toString().length > 12) {
    callback(new Error("Name is to long"))
  } else {
    callback()
  }
}

const validatePass = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Please input the password'))
  } else {
    if (ruleForm.checkPass !== '') {
      if (!ruleFormRef.value) return
      ruleFormRef.value.validateField('checkPass')
    }
    callback()
  }
}
const validatePass2 = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Please input the password again'))
  } else if (value !== ruleForm.pass) {
    callback(new Error("Two inputs don't match!"))
  } else {
    callback()
  }
}

const ruleForm = reactive({
  pass: '',
  checkPass: '',
  id: '',
  name:'',
  //mobile:''
})

const rules = reactive<FormRules<typeof ruleForm>>({
  pass: [{ validator: validatePass, trigger: 'blur' }],
  checkPass: [{ validator: validatePass2, trigger: 'blur' }],
  id: [{ validator: checkId, trigger: 'blur' }],
  name:[{ validator: checkName, trigger: 'blur' }]
})

const submitForm = async (formEl: FormInstance | undefined) => {
  if (!formEl) return
  try {
    // 触发表单验证
    await formEl.validate()

    // 构建要提交的数据
    const data = {
      username: String(ruleForm.id),
      password: String(ruleForm.pass),
      nickname: String(ruleForm.name)
    }

    // 调用注册API
    const response = await axios.post('http://localhost:5117/api/auth/register', {
      username: String(ruleForm.id),
      password: String(ruleForm.pass),
      nickname: String(ruleForm.name)
    }, {
        headers: {
          'Content-Type': 'application/json' // 确保请求头正确
        }
      })

    if (response.data && response.data.vertify === 'success') {
      //submitMessage.value = 'Registration successful!'+"Token:" +response.data.user.Token
      ElMessage.success('注册成功！');
      setTimeout(()=> {
          router.push({name:'Login'})
        },1000)
      // console.log('Registration successful!'+"Token:" +response.data.user.Token)
      // unref(popoverRef).doClose()
    } else {
      //submitMessage.value = 'Registration failed: ' + response.data.vertify
      ElMessage.error('注册失败：' + response.data.vertify);
    }
  } catch (error) {
    // submitMessage.value = 'Error: ' + (error instanceof Error ? error.message : 'Unknown error')
    ElMessage.error('注册失败:' + error);
    //console.error(error);
  }
}


const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}

const buttonRef = ref()
const popoverRef = ref()


</script>
