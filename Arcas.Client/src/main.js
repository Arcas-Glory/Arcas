import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'


const app = createApp(App)

app.use(ElementPlus)
app.use(router);
app.use(createPinia())

app.config.globalProperties.$axios = axios; // 使 Axios 在所有组件中可用

app.mount('#app');