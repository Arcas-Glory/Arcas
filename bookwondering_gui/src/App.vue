<template>
  <div id="app">
    <Menu />
    <router-view></router-view>
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
</style>