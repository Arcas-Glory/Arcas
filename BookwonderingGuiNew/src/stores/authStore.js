import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || "Undefined token", 
    userID: localStorage.getItem('userID') || "Undefined userID",
  }),
  actions: {
    setUser(token,userID) {
      this.token = token
      this.userID = userID
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