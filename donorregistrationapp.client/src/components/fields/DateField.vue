<template>
  <label>{{fieldDescription}} </label>
  <input type="date" v-model="selectedDate" @input="validateDate"  :class="{ error: !dateFieldIsValid }"/>
  <p v-if="errorMessage" style="color: red;">{{ errorMessage }}</p>
</template>

<script>
  export default {
    props: {
      fieldDescription: String,
      fieldValue: Date
    },
    data() {
      return {
        selectedDate: null,
        errorMessage: '',
        dateFieldIsValid: true
      };
    },
    watch: {
      fieldValue(newVal) {
        this.selectedDate = newVal;
        if(newVal === null) this.dateFieldIsValid = true;
        if(newVal !== null) this.validateDate();
      }
    },
    methods: {
      validateDate() {
        this.errorMessage = '';
        this.dateFieldIsValid = false;

        if (!this.selectedDate) {
          this.errorMessage = 'Date not choose!';
          this.dateFieldIsValid = false;
        }

        if (new Date(this.selectedDate) <  Date.now()) {
          this.errorMessage = 'Can\'t choose previous or current days!';
          this.dateFieldIsValid = false;
        }

        if(this.errorMessage === '')
          this.dateFieldIsValid = true;

        return {
          'isValid' : this.dateFieldIsValid,
          'outValue' : this.selectedDate};
      }
    }
  };
</script>
