import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from '../artist/artistMapper';
import ArtistViewModel from '../artist/artistViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import {BankAccountTableComponent} from '../shared/bankAccountTable'
import {EmailTableComponent} from '../shared/emailTable'
import {TransactionTableComponent} from '../shared/transactionTable'
	



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
    errorMessage: ''
  };

  handleEditClick(e:any) {
    this.props.history.push(ClientRoutes.Artists + '/edit/' + this.state.model!.id);
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
    
    if (this.state.loading) {
      return <Spin size="large" />;
    } else if (this.state.loaded) {
      return (
    <div>
		  <div>

        		<div>
							<h1>{String(this.state.model!.name)}</h1>
						 </div>

						 <div>
							<p>{String(this.state.model!.bio)}</p>
						 </div>
		         
             <div>
             <a href={String(this.state.model!.facebook)}>{String(this.state.model!.facebook)}</a>
						 </div>

					   <div>
             <a href={String(this.state.model!.soundCloud)}>{String(this.state.model!.soundCloud)}</a>
						 </div>

					   <div>
             <a href={String(this.state.model!.twitter)}>{String(this.state.model!.twitter)}</a>
						 </div>
					   <div>
             <a href={String(this.state.model!.website)}>{String(this.state.model!.website)}</a>
						 </div>
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

