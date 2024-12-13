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
    <el-form-item label="Id" prop="id">
      <el-input v-model.number="ruleForm.id" />
    </el-form-item>
    <el-form-item label="Password" prop="pass">
      <el-input v-model="ruleForm.pass" type="password" autocomplete="off" />
    </el-form-item>

    <el-form-item>
      <el-button type="primary" @click="submitForm(ruleForm.id,ruleForm.pass)">
        Submit
      </el-button>
      <el-button @click="resetForm(ruleFormRef)">Reset</el-button>
    </el-form-item>
  </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import type { FormInstance, FormRules } from 'element-plus'
import axios from 'axios'

const ruleFormRef = ref<FormInstance>()

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


const submitForm = async (QQ,Pass) => {
  console.log("QQ = " + QQ + " Password = " + Pass)
  try {
    const response = await axios({
      method: 'get',
      url: 'http://localhost:5209/Login/CheckLogin?qq=' + QQ + "&pwd=" + Pass,
    });
    console.log(response.data); // 打印响应内容
    if(response.data == "Checked") {
      alert("成功登录")
    }
    else {
      alert("登入失败")
    }
  } catch (error) {
    console.error('Error:', error); // 打印错误信息
  }
};

const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}
</script>
