import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from '../artist/artistMapper';
import ArtistViewModel from '../artist/artistViewModel';
import { Col,Row, Form, Input, Button, Spin, Alert, Icon, Select } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import StripeCheckout from 'react-stripe-checkout';
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
  emailSubmitting:boolean;
  emailSubmitted:boolean;
  stripeSubmitting:boolean;
  stripeSubmitted:boolean;
  tipAmountInDollars:number;
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
    emailSubmitting:false,
    emailSubmitted:false,
    stripeSubmitting:false,
    stripeSubmitted:false,
    tipAmountInDollars:5.00
  };

  handleEditClick(e:any) {
    this.props.history.push(ClientRoutes.Artists + '/edit/' + this.state.model!.id);
  }
  
  handleSubmit = (value:string) => {

    this.setState({ ...this.state, emailSubmitting: true, emailSubmitted: false });
    this.props.form.validateFields((err: any, values: any) => {
      if (!err) {
        let model = values as EmailViewModel;
        model.artistId = this.state.model!.id;
        model.emailValue = value;
        model.dateCreated
        console.log('Received values of form: ', model);
        this.submit(model);
      } else {
        this.setState({ ...this.state, emailSubmitting: false, emailSubmitted: false });
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
            emailSubmitted: true,
            emailSubmitting: false,
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
            emailSubmitted: true,
            emailSubmitting: false,
            errorOccurred: true,
            errorMessage: 'Error from API',
          });
        }
      );
  };


  onToken = (token:any) => {

   token.amountInCEnts = this.state.tipAmountInDollars * 100;

    this.setState({...this.state,stripeSubmitted:false,stripeSubmitting:false})
    fetch(Constants.ApiEndpoint + 'payments/process', {
      method: 'POST',
      body: JSON.stringify(token),
      headers: {
        'Content-Type': 'application/json',
      },
    }).then(response => {
        this.setState({...this.state,stripeSubmitted:true,stripeSubmitting:false,errorMessage:'',errorOccurred:false})
      }, error =>
      {
        this.setState({...this.state,stripeSubmitted:false,stripeSubmitting:true,errorMessage:'Error from Stripe',errorOccurred:true})
    });
  }

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


    let checkout = <span></span>;
    let email = <span></span>;

    if(this.state.emailSubmitted)
    {
      email = <Alert type={"success"} message={"You have been subscribed!"} />;
    }
    else
    {
      email = <Input.Search
        placeholder="Subscribe to the mailing list..."
        enterButton={this.state.emailSubmitted ? "Submitted" : "Submit"}
        size="large"
        type="email"
        disabled={this.state.emailSubmitted}
        onSearch={(value:string) => {this.handleSubmit(value)}}
      />;
    }

    if(this.state.stripeSubmitted)
    {
      checkout = <Alert type={"success"} message={"Thanks for the tip!"} />;
    }
    else
    {
      checkout = <div>
       <Select style={{'width':'90px'}} value={this.state.tipAmountInDollars} onChange={(value) => this.setState({...this.state,tipAmountInDollars:value})}>
          <Select.Option value={3.00}>3.00</Select.Option>
          <Select.Option value={5.00} >5.00</Select.Option>
          <Select.Option value={10.00}>10.00</Select.Option>
          <Select.Option value={20.00}>20.00</Select.Option>
          <Select.Option value={50.00}>50.00</Select.Option>
          <Select.Option value={100.00}>100.00</Select.Option>
          <Select.Option value={250.00}>250.00</Select.Option>
          <Select.Option value={500.00}>500.00</Select.Option>
        </Select> &nbsp;  &nbsp;
         <StripeCheckout
            token={this.onToken}
            stripeKey="pk_test_LDwOSVOOext5rVFgnsX0b4Zy"
            name="Tip the Artist"
            label="Tip the Artist"
            description={"You are awesome!"}
            panelLabel="Tip the Artist"
            amount={this.state.tipAmountInDollars * 100} // cents
            currency="USD"
          />
      </div>;
  }

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


     {checkout}

        </Col>
    </Row>

<br />
     <Row style={{'marginBottom':'30px'}}>
     <Col span={12} offset={6}>   
    {email}
    </Col>
    </Row>

            
      <Row>
     <Col span={12} offset={6}>  
    
             <div>
             <a href={"https://facebook.com/" + String(this.state.model!.facebook)} target="_blank"><Icon type="facebook"/>&nbsp;Facebook</a>
             &nbsp;&nbsp;&nbsp;
             <a href={"https://soundCloud.com/" +String(this.state.model!.soundCloud)} target="_blank"><Icon type="sound"/>&nbsp;Sound Cloud</a>
             &nbsp;&nbsp;&nbsp;
             <a href={"https://twitter.com/" +String(this.state.model!.twitter)} target="_blank"><Icon type="twitter"/>&nbsp;Twitter</a>
             &nbsp;&nbsp;&nbsp;
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

