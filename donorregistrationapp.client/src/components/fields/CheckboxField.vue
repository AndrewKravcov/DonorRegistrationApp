<template>
  <label>{{fieldDescription}}</label>
  <div class="checkbox-group">
    <label v-for="(option, index) in options" :key="index" class="checkbox-item">
      <input type="checkbox"
            :value="option.value"
            v-model="selectedOptions"
            @change="validateCheckboxes"
            :class="{ error: !checkboxFieldIsValid }"/>
            <span class="checkbox-label">{{ option.label }}</span>
    </label>
  </div>
  <p v-if="errorMessage" style="color: red;">{{ errorMessage }}</p>
</template>

<script>
  export default {
    props: {
      fieldDescription: String,
      fieldValue: Array
    },
    data() {
      return {
        options: [
          { value: 'blood-group-0', label: '0' },
          { value: 'blood-group-A', label: 'A' },
          { value: 'blood-group-B', label: 'B' },
          { value: 'blood-group-AB', label: 'AB' },
        ],
        selectedOptions: [],
        checkboxFieldIsValid: true,
        errorMessage: ''
      };
    },
    watch: {
      fieldValue(newVal) {
        this.selectedOptions = newVal;
        if(newVal.length == 0) this.checkboxFieldIsValid = true;
        if(newVal.length > 0) this.validateCheckboxes();
      }
    },
    methods: {
      validateCheckboxes() {
        this.errorMessage = '';
        this.checkboxFieldIsValid = false;

        if (this.selectedOptions.length === 0) {
          this.errorMessage = 'Choose one!';
          this.checkboxFieldIsValid = false;
          return;
        }

        if(this.errorMessage === '')
          this.checkboxFieldIsValid = true;

        return {
          'isValid' : this.checkboxFieldIsValid,
          'outValue' : this.selectedOptions};
      }
    }
  };
</script>
