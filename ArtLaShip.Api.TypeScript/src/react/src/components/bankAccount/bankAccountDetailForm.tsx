import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import BankAccountMapper from './bankAccountMapper';
import BankAccountViewModel from './bankAccountViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';

interface BankAccountDetailComponentProps {
  form: WrappedFormUtils;
  history: any;
  match: any;
}

interface BankAccountDetailComponentState {
  model?: BankAccountViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
}

class BankAccountDetailComponent extends React.Component<
  BankAccountDetailComponentProps,
  BankAccountDetailComponentState
> {
  state = {
    model: new BankAccountViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
  };

  handleEditClick(e: any) {
    this.props.history.push(
      ClientRoutes.BankAccounts + '/edit/' + this.state.model!.id
    );
  }

  componentDidMount() {
    this.setState({ ...this.state, loading: true });

    axios
      .get(
        Constants.ApiEndpoint +
          ApiRoutes.BankAccounts +
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
          let response = resp.data as Api.BankAccountClientResponseModel;

          console.log(response);

          let mapper = new BankAccountMapper();

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
              <h3>Account Number</h3>
              <p>{String(this.state.model!.accountNumber)}</p>
            </div>
            <div style={{ marginBottom: '10px' }}>
              <h3>Artist</h3>
              <p>{String(this.state.model!.artistIdNavigation!.toDisplay())}</p>
            </div>
            <div>
              <h3>Routing Number</h3>
              <p>{String(this.state.model!.routingNumber)}</p>
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

export const WrappedBankAccountDetailComponent = Form.create({
  name: 'BankAccount Detail',
})(BankAccountDetailComponent);


/*<Codenesium>
    <Hash>d545d2e08a075b317a6bee6106ee5734</Hash>
</Codenesium>*/