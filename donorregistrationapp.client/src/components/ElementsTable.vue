<template>
  <div v-if="loading">
      Loading... Please refresh once the ASP.NET backend has started.
  </div>

  <div v-if="data" class="table-column">
      <div class="table-search">
        <input type="text" id="searchInput" v-model.lazy="searchText" placeholder="Search..." class="search-input">
        <span class="search-icon">⌕</span>
      </div>
      <table>
          <thead>
              <tr>
                  <th>Id</th>
                  <th>Date created</th>
                  <th colspan="5">Donor data</th>
              </tr>
          </thead>
          <tbody>
            <tr v-for="item in data" :key="item.id">
              <td>{{ item.id }}</td>
              <td>{{ item.dateCreated }}</td>
              <td>{{ item.fullName }}</td>
              <td>{{ item.bloodGroup }}</td>
              <td>{{ item.dateDonate }}</td>
              <td>{{ item.hospital }}</td>
              <td>{{ item.firstTime }}</td>
              <td>
                <button @click="editItem(item.id)">Edit</button>
              </td>
              <td>
                <button @click="deleteItem(item.id)">Delete</button>
              </td>
            </tr>
          </tbody>
      </table>
  </div>
</template>

<script>
    import { defineComponent } from 'vue';

    export default defineComponent({
      props: {
        updateTable: {
            type: Boolean,
        }
      },
      emits:['edit-item'],
      data() {
          return {
              loading: false,
              data: null,
              searchText: '',
          };
      },
      created() {
          // fetch the data when the view is created and the data is
          // already being observed
          this.fetchData();
      },
      watch: {
          // call again the method if the route changes
          '$route': 'fetchData',
          'updateTable': 'fetchData',
          'searchText': 'fetchData',
      },
      methods: {
        async fetchData() {
          this.data = null;
          this.loading = true;
          try{
            const response = await fetch(`api/elements/${this.searchText.toString()}`, {
              headers: { 'Content-Type': 'application/json' }
            });
            this.data = await response.json();
            this.loading = false;
            console.log('item get list:', this.data);}
          catch(error){
            console.error('item get list error log:', error);
          };
        },

        editItem(itemId) {
          this.$emit('edit-item', itemId);
        },

        async deleteItem(id) {
          if (confirm(`You want to delete ${id} ?`)) {
            try{
              const response = await fetch(`api/elements/${id}`, {
              method: 'DELETE',
              headers: {'Content-Type': 'application/json'},
              body: JSON.stringify(id)
            });

            const data = await response.json();
            console.log('item delete:', data);

            await this.fetchData();

            }
            catch(error){
              console.error('error log:', error);
          };
        }
      }
    },
  });
</script>
