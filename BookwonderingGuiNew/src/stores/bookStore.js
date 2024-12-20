import { defineStore } from "pinia";

export const useBookStore = defineStore('books',{
    state: () => ({
      books:[],
      isSet:false
    }),
    actions:{
      setBooks(books) {
        this.book = books
      },
      refreshBooks() {
        //定义后端交互逻辑，来刷新本地存储数据
        this.setBooks(/*新的书籍数组*/)
      },
      releaseBooks() {
        this.book = []
      }
    }
  });