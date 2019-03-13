import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { ActionResponse, CreateResponse } from '../../api/apiObjects';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import TransactionMapper from './transactionMapper';
import TransactionViewModel from './transactionViewModel';
import {
  Form,
  Input,
  Button,
  Switch,
  InputNumber,
  DatePicker,
  Spin,
  Alert,
  TimePicker,
} from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import { ToLowerCaseFirstLetter } from '../../lib/stringUtilities';
import { ArtistSelectComponent } from '../shared/artistSelect';

interface TransactionCreateComponentProps {
  form: WrappedFormUtils;
  history: any;
  match: any;
}

interface TransactionCreateComponentState {
  model?: TransactionViewModel;
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  submitted: boolean;
  submitting: boolean;
}

class TransactionCreateComponent extends React.Component<
  TransactionCreateComponentProps,
  TransactionCreateComponentState
> {
  state = {
    model: new TransactionViewModel(),
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
    submitted: false,
    submitting: false,
  };

  handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    this.setState({ ...this.state, submitting: true, submitted: false });
    this.props.form.validateFields((err: any, values: any) => {
      if (!err) {
        let model = values as TransactionViewModel;
        console.log('Received values of form: ', model);
        this.submit(model);
      } else {
        this.setState({ ...this.state, submitting: false, submitted: false });
      }
    });
  };

  submit = (model: TransactionViewModel) => {
    let mapper = new TransactionMapper();
    axios
      .post(
        Constants.ApiEndpoint + ApiRoutes.Transactions,
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
            Api.TransactionClientRequestModel
          >;
          this.setState({
            ...this.state,
            submitted: true,
            submitting: false,
            model: mapper.mapApiResponseToViewModel(response.record!),
            errorOccurred: false,
            errorMessage: '',
          });
          console.log(response);
        },
        error => {
          console.log(error);
          if (error.response.data) {
            let errorResponse = error.response.data as ActionResponse;

            errorResponse.validationErrors.forEach(x => {
              this.props.form.setFields({
                [ToLowerCaseFirstLetter(x.propertyName)]: {
                  value: this.props.form.getFieldValue(
                    ToLowerCaseFirstLetter(x.propertyName)
                  ),
                  errors: [new Error(x.errorMessage)],
                },
              });
            });
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

  render() {
    const {
      getFieldDecorator,
      getFieldsError,
      getFieldError,
      isFieldTouched,
    } = this.props.form;

    let message: JSX.Element = <div />;
    if (this.state.submitted) {
      if (this.state.errorOccurred) {
        message = <Alert message={this.state.errorMessage} type="error" />;
      } else {
        message = <Alert message="Submitted" type="success" />;
      }
    }

    if (this.state.loading) {
      return <Spin size="large" />;
    } else if (this.state.loaded) {
      return (
        <Form onSubmit={this.handleSubmit}>
          <Form.Item>
            <label htmlFor="amount">Amount</label>
            <br />
            {getFieldDecorator('amount', {
              rules: [{ required: true, message: 'Required' }],
            })(<InputNumber placeholder={'Amount'} />)}
          </Form.Item>

          <Form.Item>
            <label htmlFor="artistId">Artist</label>
            <br />
            <ArtistSelectComponent
              apiRoute={Constants.ApiEndpoint + ApiRoutes.Artists}
              getFieldDecorator={this.props.form.getFieldDecorator}
              propertyName="artistId"
              required={true}
              selectedValue={this.state.model!.artistId}
            />
          </Form.Item>

          <Form.Item>
            <label htmlFor="dateCreated">Date Created</label>
            <br />
            {getFieldDecorator('dateCreated', {
              rules: [{ required: true, message: 'Required' }],
            })(
              <DatePicker format={'YYYY-MM-DD'} placeholder={'Date Created'} />
            )}
          </Form.Item>

          <Form.Item>
            <label htmlFor="stripeTransactionId">Stripe Transaction</label>
            <br />
            {getFieldDecorator('stripeTransactionId', {
              rules: [
                { required: true, message: 'Required' },
                { max: 128, message: 'Exceeds max length of 128' },
              ],
            })(<Input placeholder={'Stripe Transaction'} />)}
          </Form.Item>

          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              loading={this.state.submitting}
            >
              {this.state.submitting ? 'Submitting...' : 'Submit'}
            </Button>
          </Form.Item>
          {message}
        </Form>
      );
    } else {
      return null;
    }
  }
}

export const WrappedTransactionCreateComponent = Form.create({
  name: 'Transaction Create',
})(TransactionCreateComponent);


/*<Codenesium>
    <Hash>bda17b5bf13988d4b19e73f549bbe533</Hash>
</Codenesium>*/