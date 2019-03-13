import * as React from 'react';
import { Layout, Menu, Breadcrumb, Icon } from 'antd';
import MenuItem from '../../node_modules/antd/lib/menu/MenuItem';
import { Link, RouteComponentProps } from 'react-router-dom';
import { ClientRoutes, Constants } from '../constants';
const { Header, Content, Footer, Sider } = Layout;

const SubMenu = Menu.SubMenu;

interface WrapperHeaderProps {}

interface WrapperHeaderState {
  collapsed: boolean;
}
export const wrapperHeader = (Component: React.ComponentClass<any> | React.SFC<any>,
displayName:string) => {
  class WrapperHeaderComponent extends React.Component<WrapperHeaderProps & RouteComponentProps, WrapperHeaderState> {
    state = { collapsed: true };

    onCollapse = () => {
      this.setState({ ...this.state, collapsed: !this.state.collapsed });
    };
    render() {
      return (
        <Layout style={{ minHeight: '100vh' }}>
          <Sider
            collapsible
            collapsed={this.state.collapsed}
            onCollapse={this.onCollapse}
          >
            <div className="logo" />
            <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline">
               <MenuItem
                key="Home"
				onClick={() =>  {this.setState({...this.state, collapsed:true})}}
              >
                <Icon type="home" />
                <span>Home</span>
                <Link to={'/'}></Link>
              </MenuItem>

			   			   <MenuItem
                key="artist"
              >
			  <Icon type="pie-chart" />
              <span>Artist</span>
              <Link to={ClientRoutes.Artists}></Link>
              </MenuItem>

							   <MenuItem
                key="bankAccount"
              >
			  <Icon type="rise" />
              <span>Bank Account</span>
              <Link to={ClientRoutes.BankAccounts}></Link>
              </MenuItem>

							   <MenuItem
                key="transaction"
              >
			  <Icon type="bars" />
              <span>Transaction</span>
              <Link to={ClientRoutes.Transactions}></Link>
              </MenuItem>

							   <MenuItem
                key="email"
              >
			  <Icon type="cloud" />
              <span>Email</span>
              <Link to={ClientRoutes.Emails}></Link>
              </MenuItem>

				
            </Menu>
          </Sider>
          <Layout>
            <Content style={{ margin: '0 16px' }}>
            <h2>{displayName}</h2>
			  <div style={{ padding: 24, background: '#fff', minHeight: 360 }}>
                <Component {...this.props} />
              </div>
            </Content>
            <Footer style={{ textAlign: 'center' }}>Footer</Footer>
          </Layout>
        </Layout>
      );
    }
  }
  return WrapperHeaderComponent;
};

/*<Codenesium>
    <Hash>e902df3e9da2b39dffb32018f3c67c90</Hash>
</Codenesium>*/