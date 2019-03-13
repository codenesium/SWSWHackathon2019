import React, { Component, ReactElement, ReactHTMLElement } from 'react';
import axios from 'axios';
import { Redirect } from 'react-router-dom';
import * as Api from '../../api/models';
import ArtistMapper from '../artist/artistMapper';
import { Constants, ApiRoutes, ClientRoutes } from '../../constants';
import ReactTable from "react-table";
import ArtistViewModel from '../artist/artistViewModel';
import "react-table/react-table.css";
import { Form, Select, Button, Input, Row, Col, Alert, Spin } from 'antd';
import { WrappedFormUtils } from 'antd/es/form/Form';
import Item from '../../../node_modules/antd/lib/list/Item';
import { SelectValue } from '../../../node_modules/antd/lib/select';
import logo from './logo.png'; 

interface SearchComponentProps
{
   form:WrappedFormUtils;
	 history:any;
	 match:any;
}

interface SearchComponentState
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
    placeHolder:string;
}

export default class SearchComponent extends React.Component<SearchComponentProps, SearchComponentState> {

    state = ({placeHolder:'Enter an artist...', deleteSubmitted:false, deleteSuccess:false, deleteResponse:'', records:new Array<ArtistViewModel>(), filteredRecords:new Array<ArtistViewModel>(), searchValue:'', loading:false, loaded:true, errorOccurred:false, errorMessage:''});
    
    componentDidMount () {
 
    }

    handleChange(id:SelectValue) {
      this.props.history.push('/artist/' + id.valueOf());
     }

   handleSearchChanged(e:string) {
		this.loadRecords(e);
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
      
        const options = this.state.records.map((item,i) => { return <Select.Option key={String(item.id)}>{item.name}</Select.Option>});
            
			return (
     <div>
       <br />
       <br />
       <br />
            <Row>
              <Col span={12} offset={6}>  
              <div style={{'textAlign':'center','marginBottom':'35px'}}>
                <img src={logo} alt="Logo" /> 
                </div>
              <Select
              showSearch
              placeholder="Artist name...Try Jake."
              defaultActiveFirstOption={false}
              showArrow={false}
              filterOption={false}
              onSearch={(value) => this.handleSearchChanged(value)}
              onChange={(value) => this.handleChange(value)}
              notFoundContent="No artists found..."
              style={{'width':'100%'}}
              >
                {options}
              </Select>
              </Col>
			</Row>
			<br />
			<br />
         

                  </div>);
        } 
}

export const WrappedSearchComponent = Form.create({ name: 'Artist Search' })(SearchComponent);

/*<Codenesium>
    <Hash>ddc710b4a2594761c38255363b7b8f9a</Hash>
</Codenesium>*/