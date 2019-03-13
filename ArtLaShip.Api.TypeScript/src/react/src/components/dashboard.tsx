import * as React from 'react';
import axios from 'axios';
import { Constants } from '../constants';
import { Alert,Statistic,Col } from 'antd'

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
   <Col span={12}>
      <Statistic title="New Subscriptions" value={113} />
      <br />
      <Statistic title="New Tips" value={157} precision={2} />
    </Col>
      </div>;
  }
}