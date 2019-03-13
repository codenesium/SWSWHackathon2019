import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from '../artist/artistMapper';
import ArtistViewModel from '../artist/artistViewModel';
import { Col,Row, Form, Input, Button, Spin, Alert, Icon } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import EmailMapper from '../email/emailMapper';
import EmailViewModel from '../email/emailViewModel';
	



interface ArtistComponentProps {
  form: WrappedFormUtils;
  history: any;
  match: any;
}

interface ArtistComponentState {
  model?: ArtistViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  submitting:boolean;
  submitted:boolean;
}

class ArtistComponent extends React.Component<
ArtistComponentProps,
ArtistComponentState
> {
  state = {
    model: new ArtistViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
    submitting:false,
    submitted:false
  };

  handleEditClick(e:any) {
    this.props.history.push(ClientRoutes.Artists + '/edit/' + this.state.model!.id);
  }
  
  handleSubmit = (value:string) => {

    this.setState({ ...this.state, submitting: true, submitted: false });
    this.props.form.validateFields((err: any, values: any) => {
      if (!err) {
        let model = values as EmailViewModel;
        model.artistId = this.state.model!.id;
        model.emailValue = value;
        model.dateCreated
        console.log('Received values of form: ', model);
        this.submit(model);
      } else {
        this.setState({ ...this.state, submitting: false, submitted: false });
      }
    });
  };

  submit = (model: EmailViewModel) => {
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
          this.setState({
            ...this.state,
            submitted: true,
            submitting: false,
            errorOccurred: false,
            errorMessage: ''
          });
          console.log(response);
        },
        error => {
          console.log(error);
          if (error.response.data) {
            let errorResponse = error.response.data as ActionResponse;

          }
          this.setState({
            ...this.state,
            submitted: true,
            submitting: false,
            errorOccurred: true,
            errorMessage: 'Error from API',
          });
        }
      );
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
        },
        error => {
          console.log(error);
          this.setState({
            model: undefined,
            loading: false,
            loaded: true,
            errorOccurred: true,
            errorMessage: 'Error from API',
          });
        }
      );
  }

  render() {
    
    const {
      getFieldDecorator,
      getFieldsError,
      getFieldError,
      isFieldTouched,
    } = this.props.form;


    if (this.state.loading) {
      return <Spin size="large" />;
    } else if (this.state.loaded) {
      return (
    <div>
		  <div>

        		<div style={{'textAlign':'center', 'marginBottom':'30px'}}>
							<h1>{String(this.state.model!.name)}</h1>
						 </div>

     <Row style={{'marginBottom':'30px'}}>
     <Col span={12} offset={6}>  
						 <div style={{'textAlign':'left'}}>
							<p>{String(this.state.model!.bio || '')}</p>
						 </div>
             </Col>
    </Row>
 
     <Row style={{'marginBottom':'30px'}}>
     <Col span={12} offset={6}>   
    <Input.Search
      placeholder="Subscribe to the mailing list..."
      enterButton={this.state.submitted ? "Submitted" : "Submit"}
      size="large"
      type="email"
      disabled={this.state.submitted}
      onSearch={(value:string) => {this.handleSubmit(value)}}
    />
    </Col>
    </Row>

            
            <Row>
     <Col span={12} offset={6}>  
    
             <div>
             <a href={"https://facebook.com/" + String(this.state.model!.facebook)} target="_blank"><Icon type="facebook"/>&nbsp;Facebook</a>
						 </div>

					   <div>
             <a href={"https://soundCloud.com/" +String(this.state.model!.soundCloud)} target="_blank"><Icon type="sound"/>&nbsp;Sound Cloud</a>
						 </div>

					   <div>
             <a href={"https://twitter.com/" +String(this.state.model!.twitter)} target="_blank"><Icon type="twitter"/>&nbsp;Twitter</a>
						 </div>
					   <div>
             <a href={String(this.state.model!.website)} target="_blank"><Icon type="link"/>&nbsp;Website</a>
						 </div>

                          </Col>
              </Row>
					  </div>
         </div>
      );
    } else {
      return null;
    }
  }
}

export const WrappedArtistComponent = Form.create({ name: 'Artist' })(
  ArtistComponent
);

