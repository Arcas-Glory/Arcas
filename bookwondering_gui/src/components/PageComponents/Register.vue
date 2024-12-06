<template>
  <div style="display: flex; justify-content: center; padding-top: 50px;">
    <el-form
    ref="ruleFormRef"
    style="max-width: auto"
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
    <el-form-item label="Confirm" prop="checkPass">
      <el-input v-model="ruleForm.checkPass" type="password" autocomplete="off" />
    </el-form-item>
    <el-form-item label="Name" prop="name">
      <el-input v-model.number="ruleForm.name" />
    </el-form-item>
    <el-form-item label="Mobile" prop="mobile">
      <el-input v-model.number="ruleForm.mobile" />
    </el-form-item>

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
  mobile:''
})

const rules = reactive<FormRules<typeof ruleForm>>({
  pass: [{ validator: validatePass, trigger: 'blur' }],
  checkPass: [{ validator: validatePass2, trigger: 'blur' }],
  id: [{ validator: checkId, trigger: 'blur' }],
  name:[{ validator: checkName, trigger: 'blur' }]
})

const submitForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.validate((valid) => {
    if (valid) {
      console.log('submit!' + typeof ruleForm.id + " " + typeof ruleForm.pass)
      registUser(ruleForm.id,ruleForm.pass,ruleForm.name,ruleForm.mobile)
      onClickOutside()
    } else {
      console.log('error submit!')
    }
  })
}

const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}

const buttonRef = ref()
const popoverRef = ref()
const onClickOutside = () => {
  console.log('点了')
  unref(popoverRef).popperRef?.delayHide?.()
}

const registUser = async (newQQ,newPass,newName,newMobile) => {
  console.log("QQ = " + newQQ + " Password = " + newPass + " Name = " + newName)
  try {
    const response = await axios({
      method: 'post',
      url: 'http://localhost:5209/User/RegistUser',
      headers: {
        'Accept': 'text/plain',
        'Content-Type': 'application/json'
      },
      data: {
        qq: String(newQQ),
        mobile: String(newMobile),
        passWord: String(newPass),
        nickName: String(newName),
        userSex: "456789",
        validateKey: "423",
        validateCode: "75757"
      }
    });
    console.log(response); // 打印响应内容
  } catch (error) {
    console.error('Error:', error); // 打印错误信息
  }
};

</script>
