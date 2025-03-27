<template>
    <div class="header">
      <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />
    </div>
    <div class="container">
      <ElementForm ref="elementFormRef" @update-table="handleUpdateTable" />
      <ElementsTable ref="elementsTableRef" @edit-item="handleUpdateItem" />
    </div>
</template>

<script>
  import ElementsTable from './components/ElementsTable.vue'
  import ElementForm from './components/ElementForm.vue'

  export default {
    components: {
      ElementsTable,
      ElementForm,
    },
    data() {
      return {
        sharedItemId:null,
        updateTable:false
      };
    },
    methods: {
      handleUpdateItem(itemId) {
        this.$refs.elementFormRef.getItem(itemId).then(console.log('call get item'));
      },
      handleUpdateTable() {
        this.$refs.elementsTableRef.fetchData().then(console.log('call fetch data'))
      }
    }
}
</script>
