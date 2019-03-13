import * as React from 'react';
import { Layout, Menu, Breadcrumb, Icon } from 'antd';
import MenuItem from '../../../node_modules/antd/lib/menu/MenuItem';
import { Link, RouteComponentProps } from 'react-router-dom';
import { ClientRoutes, Constants } from '../../constants';
const { Header, Content, Footer, Sider } = Layout;

const SubMenu = Menu.SubMenu;

interface WrapperPublicMenuProps {}

interface WrapperPublicMenuState {
  collapsed: boolean;
}
export const wrapperPublicMenu = (
  Component: React.ComponentClass<any> | React.SFC<any>,
  displayName: string
) => {
  class WrapperPublicMenuComponent extends React.Component<
    WrapperPublicMenuProps & RouteComponentProps,
    WrapperPublicMenuState
  > {
    state = { collapsed: true };

    onCollapse = () => {
      this.setState({ ...this.state, collapsed: !this.state.collapsed });
    };
    render() {
      return (
        <Layout style={{ minHeight: '100vh' ,'background':'#FFF' }}>

            <div className="logo" />
            <Menu theme="dark" defaultSelectedKeys={['1']} mode="horizontal">
              <MenuItem
                key="Home"
                onClick={() => {
                  this.setState({ ...this.state, collapsed: true });
                }}
              >
                <Icon type="home" />
                <span>Home</span>
                <Link to={'/'} />
              </MenuItem>
             
              <MenuItem
                key="admin"
              >
			  <Icon type="rise" />
              <span>Login</span>
              <Link to={'/dashboard'}></Link>
              </MenuItem>
            </Menu>

          <Layout>
            <Content style={{ margin: '0 0' }}>
              <div style={{ padding: 24, minHeight: 360 }}>
                <Component {...this.props} />
              </div>
            </Content>
            <Footer style={{ textAlign: 'center' }}></Footer>
          </Layout>
        </Layout>
      );
    }
  }
  return WrapperPublicMenuComponent;
};


/*<Codenesium>
    <Hash>926a30d94cca0046af2e1f95fbc89147</Hash>
</Codenesium>*/