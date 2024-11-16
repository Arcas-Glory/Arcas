import { createRouter, createWebHistory } from 'vue-router';
import Menu from '../components/Menu.vue';
import Login from '../components/Login.vue';
import Register from '../components/Register.vue';

const routes = [
  {
    path: '/',
    name: 'Menu',
    component: Menu,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;