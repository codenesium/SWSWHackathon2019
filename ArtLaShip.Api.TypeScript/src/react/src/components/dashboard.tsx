import * as React from 'react';
import axios from 'axios';
import { Constants } from '../constants';
import { Alert } from 'antd'

interface DashboardComponentProps{
}

interface DashboardComponentState{
  connected:boolean;
  connecting:boolean;
}

export default class Dashboard extends React.Component<DashboardComponentProps, DashboardComponentState>  {
  
  state = ({connected:false, connecting:false});

  testConnection() {
    this.setState({...this.state,connecting:true});

    axios.get(Constants.ApiHealthEndpoint, {
      headers: {
        'Content-Type': 'application/json',
      },
    })
    .then(
      resp => {
        console.log(resp);
        this.setState({connecting:false, connected:true});

      },
      error => {
        console.log(error);
        this.setState({connecting:false, connected:false});
      }
    );
  }

  componentDidMount()
  {
     this.testConnection();
  }

  render() {
 
      return <div>
        Welcome to your dashboard!
      </div>;
  }
}