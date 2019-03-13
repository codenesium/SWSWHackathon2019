export class Constants {
  static readonly BaseEndpoint = process.env.REACT_APP_API_URL;
  static readonly ApiEndpoint = Constants.BaseEndpoint + 'api/';
  static readonly ApiHealthEndpoint = Constants.ApiEndpoint + 'apiHealth';
  static readonly SwaggerEndpoint = Constants.BaseEndpoint + 'swagger';
  static readonly HostedSubDirectory = process.env.REACT_APP_HOST_SUBDIRECTORY;
}

export class ClientRoutes {
  static readonly Artists = '/artists';
  static readonly BankAccounts = '/bankaccounts';
  static readonly Transactions = '/transactions';
  static readonly Emails = '/emails';
}

export class ApiRoutes {
  static readonly Artists = 'artists';
  static readonly BankAccounts = 'bankaccounts';
  static readonly Transactions = 'transactions';
  static readonly Emails = 'emails';
}


/*<Codenesium>
    <Hash>7cb29b040acce2a81ea9926c3be73ec3</Hash>
</Codenesium>*/