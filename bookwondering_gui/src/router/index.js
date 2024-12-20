import { createRouter, createWebHistory } from 'vue-router'
import Menu from '../components/Menu.vue'
import Login from '../components/PageComponents/Login.vue'
import Register from '../components/PageComponents/Register.vue'
import BookDisplay from '../components/PageComponents/BookDisplay.vue'
import BookDetails from '../components/PageComponents/BookDetails.vue'
import BookUpLoad from '../components/BookUpLoad.vue'
import BookBorrowPage from '../components/PageComponents/BookBorrowPage.vue'
import UserDetails from '../components/PageComponents/UserDetails.vue'
import EmailBox from '../components/PageComponents/EmailBox.vue'
import EmailReply from '../components/PageComponents/EmailReply.vue'
import EmailDetail from '../components/PageComponents/EmailDetail.vue'
import BookReturnPage from '../components/PageComponents/BookReturnPage.vue'

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
    path:'/bookborrowpage/:id',
    name:'BookBorrowPage',
    component: BookBorrowPage,
    props: true,
  },
  {
    path: '/bookupload',
    name: 'BookUpLoad',
    component: BookUpLoad
  },
  {
    path: '/userdetails',
    name: 'UserDetails',
    component: UserDetails
  },
  {
    path: '/emailbox',
    name: 'EmailBox',
    component: EmailBox
  },
  {
    path: '/emailreply',
    name: 'EmailReply',
    component: EmailReply
  },
  {
    path: '/emaidetail',
    name: 'EmailDetail',
    component: EmailDetail
  },
  {
    path: '/bookreturnPage/:id',
    name: 'BookReturnPage',
    component: BookReturnPage,
    props: true,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;