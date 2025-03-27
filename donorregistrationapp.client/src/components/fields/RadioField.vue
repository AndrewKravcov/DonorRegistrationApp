<template>
  <label>{{fieldDescription}} </label>
  <div class="radio-group">
    <label v-for="(option, index) in options" :key="index" class="radio-item">
      <input type="radio"
            :value="option.value"
            v-model="selectedValue"
            @change="validateRadio"
            :class="{ error: !radioFieldIsValid }"/>
            <span class="radio-label">{{ option.label }}</span>
    </label>
  </div>
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
          { value: 'first-time-yes', label: 'Yes' },
          { value: 'first-time-no', label: 'No' }
        ],
        selectedValue: '',
        errorMessage: '',
        radioFieldIsValid: true
      };
    },
    watch: {
      fieldValue(newVal) {
        this.selectedValue = newVal;
        if(newVal === '') this.radioFieldIsValid = true;
        if(newVal !== '') this.validateRadio();
      }
    },
    methods: {
      validateRadio() {
        this.errorMessage = '';
        this.radioFieldIsValid = false;

        if (!this.selectedValue) {
          this.errorMessage = 'Choose one!';
          this.radioFieldIsValid = false;
        }

        if(this.errorMessage === '')
          this.radioFieldIsValid = true;

        return {
          'isValid' : this.radioFieldIsValid,
          'outValue' : this.selectedValue};
      }
    }
  };
</script>
