<template>

  <div id="app">
    <div class="common-layout">
      <el-container>
        <el-header>
          <el-row type="flex" justify="end">
            <el-col :span="10">
              <Menu />
            </el-col>
          </el-row>
        </el-header>
        <br />
        <el-main><router-view></router-view></el-main>
      </el-container>
    </div>
  </div>
</template>

<script>
import Menu from './components/Menu.vue'
import ImgDisplay from './components/ImageDisplay.vue'


export default {
  name: 'App',
  components: {
    Menu,
    ImgDisplay,
  }
};


</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #2c3e50;
}
.el-main {
  border: 1px solid #dcdfe6; /* 灰色边框 */
  padding: 20px; /* 内边距 */
  margin-top: 20px; /* 外边距 */
}
</style>