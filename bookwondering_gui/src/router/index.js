import { createRouter, createWebHistory } from 'vue-router'
import Menu from '../components/Menu.vue'
import Login from '../components/PageComponents/Login.vue'
import Register from '../components/PageComponents/Register.vue'
import BookDisplay from '../components/PageComponents/BookDisplay.vue'
import BookDetails from '../components/PageComponents/BookDetails.vue'
import BookUpLoad from '../components/BookUpLoad.vue'

const routes = [
  {
    path: '/menu',
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
    path: '/',
    name: 'BookDisplay',
    component: BookDisplay,
  },
  {
    path:'/bookdetails/:id',
    name:'BookDetails',
    component: BookDetails,
    props: true,
  },
  {
    path: '/bookupload',
    name: 'BookUpLoad',
    component: BookUpLoad
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;