import { createRouter, createWebHistory } from 'vue-router';
import Menu from '../components/Menu.vue';
import Login from '../components/PageComponents/Login.vue';
import Register from '../components/PageComponents/Register.vue';
import BookDisplay from '../components/PageComponents/BookDisplay.vue'

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
  {
    path: '/bookdisplay',
    name: 'BookDisplay',
    component: BookDisplay,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;