<template>
  <div  class="forms-column">
    <div class="form-group">
      <TextField :fieldDescription="textFieldDescription"
                 :fieldValue="localtem.fullName"
                 ref="textFieldRef"/>
    </div>
    <div class="form-group">
      <CheckboxField :fieldDescription="checkboxFieldDescription"
                     :fieldValue="localtem.bloodGroup"
                     ref="checkboxFieldRef"/>
    </div>
    <div class="form-group">
      <DateField :fieldDescription="dateFieldDescription"
                 :fieldValue="localtem.dateDonate"
                 ref="dateFieldRef"/>
    </div>
    <div class="form-group">
      <DropDownField :fieldDescription="dropdownFieldDescription"
                     :fieldValue="localtem.hospital"
                     ref="dropdownFieldRef"/>
    </div>
    <div class="form-group">
      <RadioField :fieldDescription="radioFieldDescription"
                  :fieldValue="localtem.firstTime"
                  ref="radioFieldRef"/>
    </div>
    <button @click="submitItem(localtem)">Submit</button>
  </div>
</template>
<script>
  import TextField from './fields/TextField.vue'
  import CheckboxField from './fields/CheckboxField.vue'
  import DateField from './fields/DateField.vue'
  import DropDownField from './fields/DropDownField.vue'
  import RadioField from './fields/RadioField.vue'

  export default {
    created() {
        this.textFieldDescription = 'Full Name'
        this.checkboxFieldDescription = 'Blood type compatibility'
        this.dateFieldDescription = 'Date to donate'
        this.dropdownFieldDescription = 'Select a hospital'
        this.radioFieldDescription = 'It\'s your first time?'
    },
    components: {
      TextField,
      CheckboxField,
      DateField,
      DropDownField,
      RadioField
    },
    data() {
      return this.initialState();
    },
    methods: {
      initialState() {
        return {
          formIsValid: false,
          localtem:{
            id: null,
            dateCreated: null,
            fullName: '',
            bloodGroup:[],
            dateDonate:null,
            hospital:'',
            firstTime:''
          },
        }
      },
      reset() {
        this.formIsValid = this.initialState().formIsValid,
        this.localtem = this.initialState().localtem
      },

      //validation
      fieldsValidation() {
        let textFieldData = this.$refs.textFieldRef.validateInput();
        let checkboxFieldData = this.$refs.checkboxFieldRef.validateCheckboxes();
        let dateFieldData = this.$refs.dateFieldRef.validateDate();
        let fropdownFieldData = this.$refs.dropdownFieldRef.validateSelect();
        let radioFieldData = this.$refs.radioFieldRef.validateRadio();

        if( textFieldData.isValid
            && checkboxFieldData.isValid
            && dateFieldData.isValid
            && fropdownFieldData.isValid
            && radioFieldData.isValid)
        {
          this.localtem.fullName = textFieldData.outValue;
          this.localtem.bloodGroup = checkboxFieldData.outValue
          this.localtem.dateDonate = dateFieldData.outValue
          this.localtem.hospital = fropdownFieldData.outValue
          this.localtem.firstTime = radioFieldData.outValue
          return true;
        }
        return false;
      },

      async getItem(itemId){
        try {
          const response = await fetch(`api/elements/getitem/${itemId}`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json'
            }
          });
          this.localtem = this.initialState().localtem;
          this.localtem = await response.json();
          console.log('item get:', this.localtem);
        } catch (error) {
          console.error('get item error log:', error);
        }
      },

    async submitItem() {
      if(this.fieldsValidation())
        {
        try {
          if(this.localtem.id !== null)
          {
            const response = await fetch(`api/elements/${this.localtem.id}`, {
              method: 'PUT',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(this.localtem)
            });
            const data = await response.json();
            console.log('item edit:', data);
          }
          else
          {
            const response = await fetch('api/elements', {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(this.localtem)
            });
            const data = await response.json();
            console.log('item add:', data);
          }
          this.$emit('update-table');
          this.reset();
        } catch (error) {
          console.log(this.localtem);
          console.error('submit item error log:', error);
        }
      }
    },
  }
}
</script>
