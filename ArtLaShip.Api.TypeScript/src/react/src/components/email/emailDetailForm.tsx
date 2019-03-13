import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import EmailMapper from './emailMapper';
import EmailViewModel from './emailViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';




interface EmailDetailComponentProps {
  form: WrappedFormUtils;
  history: any;
  match: any;
}

interface EmailDetailComponentState {
  model?: EmailViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
}

class EmailDetailComponent extends React.Component<
EmailDetailComponentProps,
EmailDetailComponentState
> {
  state = {
    model: new EmailViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: ''
  };

  handleEditClick(e:any) {
    this.props.history.push(ClientRoutes.Emails + '/edit/' + this.state.model!.id);
  }
  
  componentDidMount() {
    this.setState({ ...this.state, loading: true });

    axios
      .get(
        Constants.ApiEndpoint +
          ApiRoutes.Emails +
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
          let response = resp.data as Api.EmailClientResponseModel;

          console.log(response);

          let mapper = new EmailMapper();

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
    
    let message: JSX.Element = <div />;
    if (this.state.errorOccurred) {
      message = <Alert message={this.state.errorMessage} type="error" />;
    } 
  
    if (this.state.loading) {
      return <Spin size="large" />;
    } else if (this.state.loaded) {
      return (
        <div>
		<Button 
			style={{'float':'right'}}
			type="primary" 
			onClick={(e:any) => {
				this.handleEditClick(e)
				}}
			>
             <i className="fas fa-edit" />
		  </Button>
		  <div>
									 <div style={{"marginBottom":"10px"}}>
							<h3>Artist</h3>
							<p>{String(this.state.model!.artistIdNavigation!.toDisplay())}</p>
						 </div>
					   						 <div>
							<h3>Date Created</h3>
							<p>{String(this.state.model!.dateCreated)}</p>
						 </div>
					   						 <div>
							<h3>Email</h3>
							<p>{String(this.state.model!.emailValue)}</p>
						 </div>
					   		  </div>
          {message}


        </div>
      );
    } else {
      return null;
    }
  }
}

export const WrappedEmailDetailComponent = Form.create({ name: 'Email Detail' })(
  EmailDetailComponent
);

/*<Codenesium>
    <Hash>2a135de89419c7258720fdc95176fdbb</Hash>
</Codenesium>*/