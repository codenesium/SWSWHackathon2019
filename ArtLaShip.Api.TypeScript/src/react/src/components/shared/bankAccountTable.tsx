import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import BankAccountMapper from '../bankAccount/bankAccountMapper';
import BankAccountViewModel from '../bankAccount/bankAccountViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import ReactTable from "react-table";

interface BankAccountTableComponentProps {
  id:number,
  apiRoute:string;
  history: any;
  match: any;
}

interface BankAccountTableComponentState {
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  filteredRecords : Array<BankAccountViewModel>;
}

export class  BankAccountTableComponent extends React.Component<
BankAccountTableComponentProps,
BankAccountTableComponentState
> {
  state = {
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
    filteredRecords:[]
  };

handleEditClick(e:any, row: BankAccountViewModel) {
  this.props.history.push(ClientRoutes.BankAccounts + '/edit/' + row.id);
}

 handleDetailClick(e:any, row: BankAccountViewModel) {
   this.props.history.push(ClientRoutes.BankAccounts + '/' + row.id);
 }

  componentDidMount() {
	this.loadRecords();
  }

  loadRecords() {
    this.setState({ ...this.state, loading: true });

    axios
      .get(this.props.apiRoute,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .then(
        resp => {
          let response = resp.data as Array<Api.BankAccountClientResponseModel>;

          console.log(response);

          let mapper = new BankAccountMapper();
          
          let bankAccounts:Array<BankAccountViewModel> = [];

          response.forEach(x =>
          {
              bankAccounts.push(mapper.mapApiResponseToViewModel(x));
          });
          this.setState({
            ...this.state,
            filteredRecords: bankAccounts,
            loading: false,
            loaded: true,
            errorOccurred: false,
            errorMessage: '',
          });
        },
        error => {
          console.log(error);
          this.setState({
            ...this.state,
            loading: false,
            loaded: false,
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
    }
	else if (this.state.errorOccurred) {
	  return <Alert message={this.state.errorMessage} type='error' />;
	}
	 else if (this.state.loaded) {
      return (
	  <div>
		{message}
         <ReactTable 
                data={this.state.filteredRecords}
				defaultPageSize={10}
                columns={[{
                    Header: 'BankAccounts',
                    columns: [
					  {
                      Header: 'Account Number',
                      accessor: 'accountNumber',
                      Cell: (props) => {
                      return <span>{String(props.original.accountNumber)}</span>;
                      }           
                    },  {
                      Header: 'Artist',
                      accessor: 'artistId',
                      Cell: (props) => {
                        return <a href='' onClick={(e) => { e.preventDefault(); this.props.history.push(ClientRoutes.Artists + '/' + props.original.artistId); }}>
                          {String(
                            props.original.artistIdNavigation.toDisplay()
                          )}
                        </a>
                      }           
                    },  {
                      Header: 'Routing Number',
                      accessor: 'routingNumber',
                      Cell: (props) => {
                      return <span>{String(props.original.routingNumber)}</span>;
                      }           
                    },
                    {
                        Header: 'Actions',
					    minWidth:150,
                        Cell: row => (<div>
					    <Button
                          type="primary" 
                          onClick={(e:any) => {
                            this.handleDetailClick(
                              e,
                              row.original as BankAccountViewModel
                            );
                          }}
                        >
                          <i className="fas fa-search" />
                        </Button>
                        &nbsp;
                        <Button
                          type="primary" 
                          onClick={(e:any) => {
                            this.handleEditClick(
                              e,
                              row.original as BankAccountViewModel
                            );
                          }}
                        >
                          <i className="fas fa-edit" />
                        </Button>
                        </div>)
                    }],
                    
                  }]} />
			</div>
      );
    } else {
      return null;
    }
  }
}

/*<Codenesium>
    <Hash>16a0eeed36cea384ac68f4a9961cef42</Hash>
</Codenesium>*/