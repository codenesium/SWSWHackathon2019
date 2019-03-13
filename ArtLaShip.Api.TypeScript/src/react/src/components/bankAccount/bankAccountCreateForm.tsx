import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import BankAccountMapper from './bankAccountMapper';
import BankAccountViewModel from './bankAccountViewModel';
import { Form, Input, Button, Switch, InputNumber, DatePicker, Spin, Alert, TimePicker } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ToLowerCaseFirstLetter } from '../../lib/stringUtilities';
import { ArtistSelectComponent } from '../shared/artistSelect'
	
interface BankAccountCreateComponentProps {
  form:WrappedFormUtils;
  history:any;
  match:any;
}

interface BankAccountCreateComponentState {
  model?: BankAccountViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  submitted:boolean;
  submitting:boolean;
}

class BankAccountCreateComponent extends React.Component<
  BankAccountCreateComponentProps,
  BankAccountCreateComponentState
> {
  state = {
    model: new BankAccountViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
	submitted:false,
	submitting:false
  };

 handleSubmit = (e:FormEvent<HTMLFormElement>) => {
     e.preventDefault();
	 this.setState({...this.state, submitting:true, submitted:false});
     this.props.form.validateFields((err:any, values:any) => {
      if (!err) {
        let model = values as BankAccountViewModel;
        console.log('Received values of form: ', model);
        this.submit(model);
      }
	  else {
	      this.setState({...this.state, submitting:false, submitted:false});
	  }
    });
  };

  submit = (model:BankAccountViewModel) =>
  {  
    let mapper = new BankAccountMapper();
     axios
      .post(
        Constants.ApiEndpoint + ApiRoutes.BankAccounts,
        mapper.mapViewModelToApiRequest(model),
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .then(
        resp => {
          let response = resp.data as CreateResponse<
            Api.BankAccountClientRequestModel
          >;
          this.setState({...this.state, submitted:true, submitting:false, model:mapper.mapApiResponseToViewModel(response.record!), errorOccurred:false, errorMessage:''});
          console.log(response);
        },
        error => {
          console.log(error);
          if(error.response.data)
          {
			  let errorResponse = error.response.data as ActionResponse; 

			  errorResponse.validationErrors.forEach(x =>
			  {
				this.props.form.setFields({
				 [ToLowerCaseFirstLetter(x.propertyName)]: {
				  value:this.props.form.getFieldValue(ToLowerCaseFirstLetter(x.propertyName)),
				  errors: [new Error(x.errorMessage)]
				},
				})
			  });
		  }
          this.setState({...this.state, submitted:true, submitting:false, errorOccurred:true, errorMessage:'Error from API'});
        }
      ); 
  }
  
  render() {

    const { getFieldDecorator, getFieldsError, getFieldError, isFieldTouched } = this.props.form;
        
    let message:JSX.Element = <div></div>;
    if(this.state.submitted)
    {
      if (this.state.errorOccurred) {
        message = <Alert message={this.state.errorMessage} type='error' />;
      }
      else
      {
        message = <Alert message='Submitted' type='success' />;
      }
    }

    if (this.state.loading) {
      return <Spin size="large" />;
    } 
    else if (this.state.loaded) {

        return ( 
         <Form onSubmit={this.handleSubmit}>
            			<Form.Item>
              <label htmlFor='accountNumber'>Account Number</label>
              <br />             
              {getFieldDecorator('accountNumber', {
              rules:[{ required: true, message: 'Required' },
{ max: 24, message: 'Exceeds max length of 24' },
],
              
              })
              ( <Input placeholder={"Account Number"} /> )}
              </Form.Item>

						
                        <Form.Item>
                        <label htmlFor='artistId'>Artist</label>
                        <br />   
                        <ArtistSelectComponent   
                          apiRoute={
                          Constants.ApiEndpoint +
                          ApiRoutes.Artists}
                          getFieldDecorator={this.props.form.getFieldDecorator}
                          propertyName="artistId"
                          required={true}
                          selectedValue={this.state.model!.artistId}
                         />
                        </Form.Item>

						<Form.Item>
              <label htmlFor='routingNumber'>Routing Number</label>
              <br />             
              {getFieldDecorator('routingNumber', {
              rules:[{ required: true, message: 'Required' },
{ max: 24, message: 'Exceeds max length of 24' },
],
              
              })
              ( <Input placeholder={"Routing Number"} /> )}
              </Form.Item>

			
           <Form.Item>
            <Button type="primary" htmlType="submit" loading={this.state.submitting} >
                {(this.state.submitting ? "Submitting..." : "Submit")}
              </Button>
            </Form.Item>
			{message}
        </Form>);
    } else {
      return null;
    }
  }
}

export const WrappedBankAccountCreateComponent = Form.create({ name: 'BankAccount Create' })(BankAccountCreateComponent);

/*<Codenesium>
    <Hash>d0406158f1bb9a31c70001e9196b5939</Hash>
</Codenesium>*/