import React, { Component, ReactElement, ReactHTMLElement } from 'react';
import axios from 'axios';
import { Redirect } from 'react-router-dom';
import * as Api from '../../api/models';
import ArtistMapper from './artistMapper';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import ReactTable from "react-table";
import ArtistViewModel from './artistViewModel';
import "react-table/react-table.css";
import { Form, Button, Input, Row, Col, Alert, Spin } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';

interface ArtistSearchComponentProps
{
     form:WrappedFormUtils;
	 history:any;
	 match:any;
}

interface ArtistSearchComponentState
{
    records:Array<ArtistViewModel>;
    filteredRecords:Array<ArtistViewModel>;
    loading:boolean;
    loaded:boolean;
    errorOccurred:boolean;
    errorMessage:string;
    searchValue:string;
    deleteSubmitted:boolean;
    deleteSuccess:boolean;
    deleteResponse:string;
}

export default class ArtistSearchComponent extends React.Component<ArtistSearchComponentProps, ArtistSearchComponentState> {

    state = ({deleteSubmitted:false, deleteSuccess:false, deleteResponse:'', records:new Array<ArtistViewModel>(), filteredRecords:new Array<ArtistViewModel>(), searchValue:'', loading:false, loaded:true, errorOccurred:false, errorMessage:''});
    
    componentDidMount () {
        this.loadRecords();
    }

    handleEditClick(e:any, row:ArtistViewModel) {
         this.props.history.push(ClientRoutes.Artists + '/edit/' + row.id);
    }

    handleDetailClick(e:any, row:ArtistViewModel) {
         this.props.history.push(ClientRoutes.Artists + '/' + row.id);
    }

    handleCreateClick(e:any) {
        this.props.history.push(ClientRoutes.Artists + '/create');
    }

    handleDeleteClick(e:any, row:Api.ArtistClientResponseModel) {
        axios.delete(Constants.ApiEndpoint + ApiRoutes.Artists + '/' + row.id,
        {
            headers: {
                'Content-Type': 'application/json',
            }
        })
        .then(resp => {
            this.setState({...this.state, deleteResponse:'Record deleted', deleteSuccess:true, deleteSubmitted:true});
            this.loadRecords(this.state.searchValue);
        }, error => {
            console.log(error);
            this.setState({...this.state, deleteResponse:'Error deleting record', deleteSuccess:false, deleteSubmitted:true});
        })
    }

   handleSearchChanged(e:React.FormEvent<HTMLInputElement>) {
		this.loadRecords(e.currentTarget.value);
   }
   
   loadRecords(query:string = '') {
	   this.setState({...this.state, searchValue:query});
	   let searchEndpoint = Constants.ApiEndpoint + ApiRoutes.Artists + '?limit=100';

	   if(query)
	   {
		   searchEndpoint += '&query=' +  query;
	   }

	   axios.get(searchEndpoint,
	   {
		   headers: {
			   'Content-Type': 'application/json',
		   }
	   })
	   .then(resp => {
		    let response = resp.data as Array<Api.ArtistClientResponseModel>;
		    let viewModels : Array<ArtistViewModel> = [];
			let mapper = new ArtistMapper();

			response.forEach(x =>
			{
				viewModels.push(mapper.mapApiResponseToViewModel(x));
			})

            this.setState({records:viewModels, filteredRecords:viewModels, loading:false, loaded:true, errorOccurred:false, errorMessage:''});

	   }, error => {
		   console.log(error);
		   this.setState({records:new Array<ArtistViewModel>(), filteredRecords:new Array<ArtistViewModel>(), loading:false, loaded:true, errorOccurred:true, errorMessage:'Error from API'});
	   })
    }

    filterGrid() {

    }
    
    render () {
        if(this.state.loading) {
            return <Spin size="large" />;
        } 
		else if(this.state.errorOccurred) {
            return <Alert message={this.state.errorMessage} type="error" />
        }
        else if(this.state.loaded) {

            let errorResponse:JSX.Element = <span></span>;

            if (this.state.deleteSubmitted) {
				if (this.state.deleteSuccess) {
				  errorResponse = (
					<Alert message={this.state.deleteResponse} type="success" style={{marginBottom:"25px"}} />
				  );
				} else {
				  errorResponse = (
					<Alert message={this.state.deleteResponse} type="error" style={{marginBottom:"25px"}} />
				  );
				}
			}
            
			return (
            <div>
            {errorResponse}
            <Row>
				<Col span={8}></Col>
				<Col span={8}>   
				   <Input 
					placeholder={"Search"} 
					id={"search"} 
					onChange={(e:any) => {
					  this.handleSearchChanged(e)
				   }}/>
				</Col>
				<Col span={8}>  
				  <Button 
				  style={{'float':'right'}}
				  type="primary" 
				  onClick={(e:any) => {
                        this.handleCreateClick(e)
						}}
				  >
				  +
				  </Button>
				</Col>
			</Row>
			<br />
			<br />
            <ReactTable 
                data={this.state.filteredRecords}
                columns={[{
                    Header: 'Artist',
                    columns: [
					  {
                      Header: 'Asp Net User',
                      accessor: 'aspNetUserId',
                      Cell: (props) => {
                      return <span>{String(props.original.aspNetUserId)}</span>;
                      }           
                    },  {
                      Header: 'Bio',
                      accessor: 'bio',
                      Cell: (props) => {
                      return <span>{String(props.original.bio)}</span>;
                      }           
                    },  {
                      Header: 'Facebook',
                      accessor: 'facebook',
                      Cell: (props) => {
                      return <span>{String(props.original.facebook)}</span>;
                      }           
                    },  {
                      Header: 'Name',
                      accessor: 'name',
                      Cell: (props) => {
                      return <span>{String(props.original.name)}</span>;
                      }           
                    },  {
                      Header: 'Sound Cloud',
                      accessor: 'soundCloud',
                      Cell: (props) => {
                      return <span>{String(props.original.soundCloud)}</span>;
                      }           
                    },  {
                      Header: 'Twitter',
                      accessor: 'twitter',
                      Cell: (props) => {
                      return <span>{String(props.original.twitter)}</span>;
                      }           
                    },  {
                      Header: 'Website',
                      accessor: 'website',
                      Cell: (props) => {
                      return <span>{String(props.original.website)}</span>;
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
                              row.original as ArtistViewModel
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
                              row.original as ArtistViewModel
                            );
                          }}
                        >
                          <i className="fas fa-edit" />
                        </Button>
                        &nbsp;
                        <Button
                          type="danger" 
                          onClick={(e:any) => {
                            this.handleDeleteClick(
                              e,
                              row.original as ArtistViewModel
                            );
                          }}
                        >
                          <i className="far fa-trash-alt" />
                        </Button>

                        </div>)
                    }],
                    
                  }]} />
                  </div>);
        } 
		else {
		  return null;
		}
    }
}

export const WrappedArtistSearchComponent = Form.create({ name: 'Artist Search' })(ArtistSearchComponent);

/*<Codenesium>
    <Hash>8f93308604ba2d053c77c8e26a62d169</Hash>
</Codenesium>*/