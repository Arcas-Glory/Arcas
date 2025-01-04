import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || "Undefined token", 
    userID: localStorage.getItem('userID') || "Undefined userID",
    username:localStorage.getItem('username') || "Undefined username",
  }),
  actions: {
    setUser(token,userID,username) {
      this.token = token
      this.userID = userID
      this.username = username
      localStorage.setItem('token', token)
      localStorage.setItem('userID', userID)
    },
    clearToken() {
      this.token = null
      this.userID = null
      localStorage.removeItem('token') 
      localStorage.removeItem('userID')
    },
  },
});