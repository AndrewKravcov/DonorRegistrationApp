<template>
  <label>{{fieldDescription}} </label>
  <select v-model="selectedValue" @change="validateSelect" :class="{ error: !dropdownFieldIsValid }">
    <option value="">Select...</option>
    <option v-for="(option, index) in options" :key="index" :value="option.value">
      {{ option.label }}
    </option>
  </select>
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
        options: [
          { value: 'hospital1', label: 'Hospital 1' },
          { value: 'hospital2', label: 'Hospital 2' },
          { value: 'hospital3', label: 'Hospital 3' }
        ],
        selectedValue: '',
        errorMessage: '',
        dropdownFieldIsValid: true
      };
    },
    watch: {
      fieldValue(newVal) {
        this.selectedValue = newVal;
        if(newVal === '') this.dropdownFieldIsValid = true;
        if(newVal !== '') this.validateSelect();
      }
    },
    methods: {
      validateSelect() {
        this.errorMessage = '';
        this.dropdownFieldIsValid = false;

        if (!this.selectedValue) {
          this.errorMessage = 'Choose one!';
          this.dropdownFieldIsValid = false;
        }

        if(this.errorMessage === '')
          this.dropdownFieldIsValid = true;

        return {
          'isValid' : this.dropdownFieldIsValid,
          'outValue' : this.selectedValue};
      }
    }
  };
</script>
