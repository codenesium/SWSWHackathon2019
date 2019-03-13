import React, { Component, FormEvent } from 'react';
import axios from 'axios';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import * as Api from '../../api/models';
import ArtistMapper from '../artist/artistMapper';
import ArtistViewModel from '../artist/artistViewModel';
import { Form, Input, Button, Spin, Alert } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import ReactTable from 'react-table';

interface ArtistTableComponentProps {
  id: number;
  apiRoute: string;
  history: any;
  match: any;
}

interface ArtistTableComponentState {
  loading: boolean;
  loaded: boolean;
  errorOccurred: boolean;
  errorMessage: string;
  filteredRecords: Array<ArtistViewModel>;
}

export class ArtistTableComponent extends React.Component<
  ArtistTableComponentProps,
  ArtistTableComponentState
> {
  state = {
    loading: false,
    loaded: true,
    errorOccurred: false,
    errorMessage: '',
    filteredRecords: [],
  };

  handleEditClick(e: any, row: ArtistViewModel) {
    this.props.history.push(ClientRoutes.Artists + '/edit/' + row.id);
  }

  handleDetailClick(e: any, row: ArtistViewModel) {
    this.props.history.push(ClientRoutes.Artists + '/' + row.id);
  }

  componentDidMount() {
    this.loadRecords();
  }

  loadRecords() {
    this.setState({ ...this.state, loading: true });

    axios
      .get(this.props.apiRoute, {
        headers: {
          'Content-Type': 'application/json',
        },
      })
      .then(
        resp => {
          let response = resp.data as Array<Api.ArtistClientResponseModel>;

          console.log(response);

          let mapper = new ArtistMapper();

          let artists: Array<ArtistViewModel> = [];

          response.forEach(x => {
            artists.push(mapper.mapApiResponseToViewModel(x));
          });
          this.setState({
            ...this.state,
            filteredRecords: artists,
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
    } else if (this.state.errorOccurred) {
      return <Alert message={this.state.errorMessage} type="error" />;
    } else if (this.state.loaded) {
      return (
        <div>
          {message}
          <ReactTable
            data={this.state.filteredRecords}
            defaultPageSize={10}
            columns={[
              {
                Header: 'Artists',
                columns: [
                  {
                    Header: 'Asp Net User',
                    accessor: 'aspNetUserId',
                    Cell: props => {
                      return <span>{String(props.original.aspNetUserId)}</span>;
                    },
                  },
                  {
                    Header: 'Bio',
                    accessor: 'bio',
                    Cell: props => {
                      return <span>{String(props.original.bio)}</span>;
                    },
                  },
                  {
                    Header: 'Facebook',
                    accessor: 'facebook',
                    Cell: props => {
                      return <span>{String(props.original.facebook)}</span>;
                    },
                  },
                  {
                    Header: 'Name',
                    accessor: 'name',
                    Cell: props => {
                      return <span>{String(props.original.name)}</span>;
                    },
                  },
                  {
                    Header: 'Sound Cloud',
                    accessor: 'soundCloud',
                    Cell: props => {
                      return <span>{String(props.original.soundCloud)}</span>;
                    },
                  },
                  {
                    Header: 'Twitter',
                    accessor: 'twitter',
                    Cell: props => {
                      return <span>{String(props.original.twitter)}</span>;
                    },
                  },
                  {
                    Header: 'Website',
                    accessor: 'website',
                    Cell: props => {
                      return <span>{String(props.original.website)}</span>;
                    },
                  },
                  {
                    Header: 'Actions',
                    minWidth: 150,
                    Cell: row => (
                      <div>
                        <Button
                          type="primary"
                          onClick={(e: any) => {
                            this.handleDetailClick(
                              e,
                              row.original as ArtistViewModel
                            );
                          }}
                        >
                          <i className="fas fa-search" />
                        </Button>
                        &nbsp;
                        <Button
                          type="primary"
                          onClick={(e: any) => {
                            this.handleEditClick(
                              e,
                              row.original as ArtistViewModel
                            );
                          }}
                        >
                          <i className="fas fa-edit" />
                        </Button>
                      </div>
                    ),
                  },
                ],
              },
            ]}
          />
        </div>
      );
    } else {
      return null;
    }
  }
}


/*<Codenesium>
    <Hash>2ee2a06ba20ee5d5eedc15c2707906cd</Hash>
</Codenesium>*/