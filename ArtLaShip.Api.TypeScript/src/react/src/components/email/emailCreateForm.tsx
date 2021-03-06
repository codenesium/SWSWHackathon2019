import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import EmailMapper from './emailMapper';
import EmailViewModel from './emailViewModel';
import { Form, Input, Button, Switch, InputNumber, DatePicker, Spin, Alert, TimePicker } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ToLowerCaseFirstLetter } from '../../lib/stringUtilities';
import { ArtistSelectComponent } from '../shared/artistSelect'
	
interface EmailCreateComponentProps {
  form:WrappedFormUtils;
  history:any;
  match:any;
}

interface EmailCreateComponentState {
  model?: EmailViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  submitted:boolean;
  submitting:boolean;
}

class EmailCreateComponent extends React.Component<
  EmailCreateComponentProps,
  EmailCreateComponentState
> {
  state = {
    model: new EmailViewModel(),
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
        let model = values as EmailViewModel;
        console.log('Received values of form: ', model);
        this.submit(model);
      }
	  else {
	      this.setState({...this.state, submitting:false, submitted:false});
	  }
    });
  };

  submit = (model:EmailViewModel) =>
  {  
    let mapper = new EmailMapper();
     axios
      .post(
        Constants.ApiEndpoint + ApiRoutes.Emails,
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
            Api.EmailClientRequestModel
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
              <label htmlFor='dateCreated'>Date Created</label>
              <br />             
              {getFieldDecorator('dateCreated', {
              rules:[],
              
              })
              ( <DatePicker format={'YYYY-MM-DD'} placeholder={"Date Created"} /> )}
              </Form.Item>

						<Form.Item>
              <label htmlFor='emailValue'>Email Value</label>
              <br />             
              {getFieldDecorator('emailValue', {
              rules:[{ required: true, message: 'Required' },
{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Email Value"} /> )}
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

export const WrappedEmailCreateComponent = Form.create({ name: 'Email Create' })(EmailCreateComponent);

/*<Codenesium>
    <Hash>768a3e7d98d1627b9b7e3dcddb7c1614</Hash>
</Codenesium>*/