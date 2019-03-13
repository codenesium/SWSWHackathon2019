import * as React from 'react';
import { Route, Switch, match, BrowserRouter } from 'react-router-dom';
import Dashboard from './components/dashboard';
import { Security, ImplicitCallback, SecureRoute } from '@okta/okta-react';
import { wrapperHeader } from './components/header';
import { ClientRoutes, Constants } from './constants';
import { WrappedArtistCreateComponent } from './components/artist/artistCreateForm';
import { WrappedArtistDetailComponent } from './components/artist/artistDetailForm';
import { WrappedArtistEditComponent } from './components/artist/artistEditForm';
import { WrappedArtistSearchComponent } from './components/artist/artistSearchForm';
import { WrappedBankAccountCreateComponent } from './components/bankAccount/bankAccountCreateForm';
import { WrappedBankAccountDetailComponent } from './components/bankAccount/bankAccountDetailForm';
import { WrappedBankAccountEditComponent } from './components/bankAccount/bankAccountEditForm';
import { WrappedBankAccountSearchComponent } from './components/bankAccount/bankAccountSearchForm';
import { WrappedTransactionCreateComponent } from './components/transaction/transactionCreateForm';
import { WrappedTransactionDetailComponent } from './components/transaction/transactionDetailForm';
import { WrappedTransactionEditComponent } from './components/transaction/transactionEditForm';
import { WrappedTransactionSearchComponent } from './components/transaction/transactionSearchForm';
import { WrappedEmailCreateComponent } from './components/email/emailCreateForm';
import { WrappedEmailDetailComponent } from './components/email/emailDetailForm';
import { WrappedEmailEditComponent } from './components/email/emailEditForm';
import { WrappedEmailSearchComponent } from './components/email/emailSearchForm';

import { WrappedArtistComponent } from './components/public/artistForm';
import { WrappedSearchComponent } from './components/public/searchForm';
import { wrapperPublicMenu } from './components/public/menu';
const config = {
  oidc: {
    clientId: '<okta_client_id>',
    issuer: 'https://<okta_application_url>/oauth2/default',
    redirectUri: 'https://<your_public_webserver>/implicit/callback',
    scope: 'openid profile email',
  },
};

export const AppRouter: React.StatelessComponent<{}> = () => {
  return (
    <BrowserRouter basename={Constants.HostedSubDirectory}>
      <Security
        issuer={config.oidc.issuer}
        client_id={config.oidc.clientId}
        redirect_uri={config.oidc.redirectUri}
      >
        <SecureRoute
          path="/protected"
          component={() => '<div>secure route</div>'}
        />
        <Switch>
          <Route
            exact
            path="/"
            component={wrapperPublicMenu(WrappedSearchComponent, 'Public Search')}
          />

              <Route
            exact
            path="/dashboard"
            component={wrapperHeader(Dashboard, 'Dashboard')}
          />

          <Route
            exact
            path="/artist/:id"
            component={wrapperPublicMenu(WrappedArtistComponent, 'Public Artist Display')}
          />
          <Route
            path={ClientRoutes.Artists + '/create'}
            component={wrapperHeader(
              WrappedArtistCreateComponent,
              'Artist Create'
            )}
          />
          <Route
            path={ClientRoutes.Artists + '/edit/:id'}
            component={wrapperHeader(WrappedArtistEditComponent, 'Artist Edit')}
          />
          <Route
            path={ClientRoutes.Artists + '/:id'}
            component={wrapperHeader(
              WrappedArtistDetailComponent,
              'Artist Detail'
            )}
          />
          <Route
            path={ClientRoutes.Artists}
            component={wrapperHeader(
              WrappedArtistSearchComponent,
              'Artist Search'
            )}
          />
          <Route
            path={ClientRoutes.BankAccounts + '/create'}
            component={wrapperHeader(
              WrappedBankAccountCreateComponent,
              'Bank Account Create'
            )}
          />
          <Route
            path={ClientRoutes.BankAccounts + '/edit/:id'}
            component={wrapperHeader(
              WrappedBankAccountEditComponent,
              'Bank Account Edit'
            )}
          />
          <Route
            path={ClientRoutes.BankAccounts + '/:id'}
            component={wrapperHeader(
              WrappedBankAccountDetailComponent,
              'Bank Account Detail'
            )}
          />
          <Route
            path={ClientRoutes.BankAccounts}
            component={wrapperHeader(
              WrappedBankAccountSearchComponent,
              'Bank Account Search'
            )}
          />
          <Route
            path={ClientRoutes.Transactions + '/create'}
            component={wrapperHeader(
              WrappedTransactionCreateComponent,
              'Transaction Create'
            )}
          />
          <Route
            path={ClientRoutes.Transactions + '/edit/:id'}
            component={wrapperHeader(
              WrappedTransactionEditComponent,
              'Transaction Edit'
            )}
          />
          <Route
            path={ClientRoutes.Transactions + '/:id'}
            component={wrapperHeader(
              WrappedTransactionDetailComponent,
              'Transaction Detail'
            )}
          />
          <Route
            path={ClientRoutes.Transactions}
            component={wrapperHeader(
              WrappedTransactionSearchComponent,
              'Transaction Search'
            )}
          />
          <Route
            path={ClientRoutes.Emails + '/create'}
            component={wrapperHeader(
              WrappedEmailCreateComponent,
              'Email Create'
            )}
          />
          <Route
            path={ClientRoutes.Emails + '/edit/:id'}
            component={wrapperHeader(WrappedEmailEditComponent, 'Email Edit')}
          />
          <Route
            path={ClientRoutes.Emails + '/:id'}
            component={wrapperHeader(
              WrappedEmailDetailComponent,
              'Email Detail'
            )}
          />
          <Route
            path={ClientRoutes.Emails}
            component={wrapperHeader(
              WrappedEmailSearchComponent,
              'Emails'
            )}
          />
        </Switch>
      </Security>
    </BrowserRouter>
  );
};


/*<Codenesium>
    <Hash>ad4400cfcd313d3db6441433f85cc6d5</Hash>
</Codenesium>*/