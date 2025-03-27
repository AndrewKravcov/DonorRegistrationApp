<template>
  <label>{{fieldDescription}}</label>
  <input type="text" id="uname" name="name" v-model="inputValue" @input="validateInput" placeholder="Input text" :class="{ error: !textFieldIsValid }"/>
  <p v-if="errorMessage" style="color: red;">{{ errorMessage }}</p>
</template>

<script>
  export default {
    props: {
      fieldDescription: String,
      fieldValue: String
    },
    data() {
      return {
        inputValue: this.fieldValue,
        errorMessage: '',
        textFieldIsValid: true
      };
    },
    watch: {
      fieldValue(newVal) {
        this.inputValue = newVal;
        if(newVal == '') this.textFieldIsValid = true;
        if(newVal !== '') this.validateInput();
      }
    },
    methods: {
      validateInput() {
        this.textFieldIsValid = false;
        this.errorMessage = '';

        if (this.inputValue.trim() === '') {
          this.errorMessage = 'Not empty!';
          this.textFieldIsValid = false;
        }

        if (this.inputValue.length < 3) {
          this.errorMessage = 'Min 3 symbols!';
          this.textFieldIsValid = false;
        }

        if(this.errorMessage === '')
          this.textFieldIsValid = true;

        return {
          'isValid' : this.textFieldIsValid,
          'outValue' : this.inputValue};
      }
    }
  };
</script>

<style scoped>

.wrapperField {
  display: grid;
  grid-gap: 10px;
  grid-template-columns: 1fr 1fr 1fr;
  background-color: #fff;
  color: #444;
}

.boxField {
  background-color: #444;
  color: #fff;
  border-radius: 5px;
  padding: 20px;
  font-size: 150%;
}

</style>
