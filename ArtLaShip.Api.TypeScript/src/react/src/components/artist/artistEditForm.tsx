import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from './artistMapper';
import ArtistViewModel from './artistViewModel';
import { Form, Input, Button, Switch, InputNumber, DatePicker, Spin, Alert, TimePicker } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ToLowerCaseFirstLetter } from '../../lib/stringUtilities';
interface ArtistEditComponentProps {
  form:WrappedFormUtils;
  history:any;
  match:any;
}

interface ArtistEditComponentState {
  model?: ArtistViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  submitted:boolean;
  submitting:boolean;
}

class ArtistEditComponent extends React.Component<
  ArtistEditComponentProps,
  ArtistEditComponentState
> {
  state = {
    model: new ArtistViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
	submitted:false,
	submitting:false
  };

    componentDidMount() {
    this.setState({ ...this.state, loading: true });

    axios
      .get(
        Constants.ApiEndpoint +
          ApiRoutes.Artists +
          '/' +
          this.props.match.params.id,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .then(
        resp => {
          let response = resp.data as Api.ArtistClientResponseModel;

          console.log(response);

          let mapper = new ArtistMapper();

          this.setState({
            model: mapper.mapApiResponseToViewModel(response),
            loading: false,
            loaded: true,
            errorOccurred: false,
            errorMessage: '',
          });

		  this.props.form.setFieldsValue(mapper.mapApiResponseToViewModel(response));
        },
        error => {
          console.log(error);
          this.setState({
            model: undefined,
            loading: false,
            loaded: false,
            errorOccurred: true,
            errorMessage: 'Error from API',
          });
        }
      );
 }
 
 handleSubmit = (e:FormEvent<HTMLFormElement>) => {
     e.preventDefault();
	 this.setState({...this.state, submitting:true, submitted:false});
     this.props.form.validateFields((err:any, values:any) => {
     if (!err) {
        let model = values as ArtistViewModel;
        console.log('Received values of form: ', model);
        this.submit(model);
      } 
	  else {
		  this.setState({...this.state, submitting:false, submitted:false});
	  }
    });
  };

  submit = (model:ArtistViewModel) =>
  {  
    let mapper = new ArtistMapper();
     axios
      .put(
        Constants.ApiEndpoint + ApiRoutes.Artists + '/' + this.state.model!.id,
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
            Api.ArtistClientRequestModel
          >;
          this.setState({...this.state, submitted:true, submitting:false, model:mapper.mapApiResponseToViewModel(response.record!), errorOccurred:false, errorMessage:''});
          console.log(response);
        },
        error => {
          console.log(error);
		  let errorResponse = error.response.data as ActionResponse; 
		  if(error.response.data)
          {
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
              <label htmlFor='name'>Name</label>
              <br />             
              {getFieldDecorator('name', {
              rules:[{ required: true, message: 'Required' },
{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Name"} /> )}
              </Form.Item>


						<Form.Item>
              <label htmlFor='bio'>Bio</label>
              <br />             
              {getFieldDecorator('bio', {
              rules:[{ max: 8000, message: 'Exceeds max length of 8000' },
],
              
              })
              ( <Input placeholder={"Bio"} /> )}
              </Form.Item>

				<Form.Item>
              <label htmlFor='venmo'>Venmo</label>
              <br />             
              {getFieldDecorator('venmo', {
              rules:[{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Venmo"} addonBefore="@" /> )}
              </Form.Item>


						<Form.Item>
              <label htmlFor='facebook'>Facebook</label>
              <br />             
              {getFieldDecorator('facebook', {
              rules:[{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Facebook"} addonBefore="https://facebook.com/" /> )}
              </Form.Item>


						<Form.Item>
              <label htmlFor='soundCloud'>Sound Cloud</label>
              <br />             
              {getFieldDecorator('soundCloud', {
              rules:[{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Sound Cloud"}   addonBefore="https://soundcloud.com/"/> )}
              </Form.Item>

						<Form.Item>
              <label htmlFor='twitter'>Twitter</label>
              <br />             
              {getFieldDecorator('twitter', {
              rules:[{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Twitter"} addonBefore="https://twitter.com/" /> )}
              </Form.Item>

						<Form.Item>
              <label htmlFor='website'>Website</label>
              <br />             
              {getFieldDecorator('website', {
              rules:[{ max: 128, message: 'Exceeds max length of 128' },
],
              
              })
              ( <Input placeholder={"Website"}  /> )}
              </Form.Item>

		      	<br />
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

export const WrappedArtistEditComponent = Form.create({ name: 'Artist Edit' })(ArtistEditComponent);

/*<Codenesium>
    <Hash>3cb9923757584fa2132315079041d361</Hash>
</Codenesium>*/