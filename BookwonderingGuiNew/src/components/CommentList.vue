<template>
    <div class="comment-list">
      <el-scrollbar class="comment-scrollbar">
        <div class="comment-item" v-for="(comment, index) in comments" :key="index">
          <el-avatar :src="comment.avatar" size="medium" />
          <div class="comment-content">
            <strong>{{ comment.username }}</strong>
            <p>{{ comment.content }}</p>
          </div>
        </div>
      </el-scrollbar>
    </div>
  </template>
  
  <script lang="ts" setup>
  

  // 定义 Comment 类型
  interface Comment {
    username: string;
    avatar: string;
    content: string;
  }

  // 定义 props 类型
  const props = defineProps<{
  comments: Comment[];
  }>();
  
  </script>
  
  <style scoped>
  .comment-list {
    display: flex;
    flex-direction: column;
    max-height: 200px;
  }
  
  .comment-scrollbar {
    max-height: 180px;
  }
  
  .comment-item {
    display: flex;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #f0f0f0;
  }
  
  .comment-item:last-child {
    border-bottom: none;
  }
  
  .comment-content {
    margin-left: 10px;
  }
  
  strong {
    color: #333;
    margin-right: 8px;
  }
  </style>
  