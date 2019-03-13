import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from './artistMapper';
import ArtistViewModel from './artistViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { BankAccountTableComponent } from '../shared/bankAccountTable';
import { EmailTableComponent } from '../shared/emailTable';
import { TransactionTableComponent } from '../shared/transactionTable';

interface ArtistDetailComponentProps {
  form: WrappedFormUtils;
  history: any;
  match: any;
}

interface ArtistDetailComponentState {
  model?: ArtistViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
}

class ArtistDetailComponent extends React.Component<
  ArtistDetailComponentProps,
  ArtistDetailComponentState
> {
  state = {
    model: new ArtistViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
  };

  handleEditClick(e: any) {
    this.props.history.push(
      ClientRoutes.Artists + '/edit/' + this.state.model!.id
    );
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
            style={{ float: 'right' }}
            type="primary"
            onClick={(e: any) => {
              this.handleEditClick(e);
            }}
          >
            <i className="fas fa-edit" />
          </Button>
          <div>
            <div>
              <h3>Asp Net User</h3>
              <p>{String(this.state.model!.aspNetUserId)}</p>
            </div>
            <div>
              <h3>Bio</h3>
              <p>{String(this.state.model!.bio)}</p>
            </div>
            <div>
              <h3>Facebook</h3>
              <p>{String(this.state.model!.facebook)}</p>
            </div>
            <div>
              <h3>Name</h3>
              <p>{String(this.state.model!.name)}</p>
            </div>
            <div>
              <h3>Sound Cloud</h3>
              <p>{String(this.state.model!.soundCloud)}</p>
            </div>
            <div>
              <h3>Twitter</h3>
              <p>{String(this.state.model!.twitter)}</p>
            </div>
            <div>
              <h3>Website</h3>
              <p>{String(this.state.model!.website)}</p>
            </div>
          </div>
          {message}
          <div>
            <h3>BankAccounts</h3>
            <BankAccountTableComponent
              id={this.state.model!.id}
              history={this.props.history}
              match={this.props.match}
              apiRoute={
                Constants.ApiEndpoint +
                ApiRoutes.Artists +
                '/' +
                this.state.model!.id +
                '/' +
                ApiRoutes.BankAccounts
              }
            />
          </div>
          <div>
            <h3>Emails</h3>
            <EmailTableComponent
              id={this.state.model!.id}
              history={this.props.history}
              match={this.props.match}
              apiRoute={
                Constants.ApiEndpoint +
                ApiRoutes.Artists +
                '/' +
                this.state.model!.id +
                '/' +
                ApiRoutes.Emails
              }
            />
          </div>
          <div>
            <h3>Transactions</h3>
            <TransactionTableComponent
              id={this.state.model!.id}
              history={this.props.history}
              match={this.props.match}
              apiRoute={
                Constants.ApiEndpoint +
                ApiRoutes.Artists +
                '/' +
                this.state.model!.id +
                '/' +
                ApiRoutes.Transactions
              }
            />
          </div>
        </div>
      );
    } else {
      return null;
    }
  }
}

export const WrappedArtistDetailComponent = Form.create({
  name: 'Artist Detail',
})(ArtistDetailComponent);


/*<Codenesium>
    <Hash>7751142bb19ea80d3aa099eaa7070161</Hash>
</Codenesium>*/