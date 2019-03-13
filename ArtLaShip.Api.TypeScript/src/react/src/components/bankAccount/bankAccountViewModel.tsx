import moment from 'moment';
import ArtistViewModel from '../artist/artistViewModel';

export default class BankAccountViewModel {
  accountNumber: string;
  artistId: number;
  artistIdEntity: string;
  artistIdNavigation?: ArtistViewModel;
  id: number;
  routingNumber: string;

  constructor() {
    this.accountNumber = '';
    this.artistId = 0;
    this.artistIdEntity = '';
    this.artistIdNavigation = new ArtistViewModel();
    this.id = 0;
    this.routingNumber = '';
  }

  setProperties(
    accountNumber: string,
    artistId: number,
    id: number,
    routingNumber: string
  ): void {
    this.accountNumber = accountNumber;
    this.artistId = artistId;
    this.id = id;
    this.routingNumber = routingNumber;
  }

  toDisplay(): string {
    return String(this.accountNumber);
  }
}


/*<Codenesium>
    <Hash>3a2cbd68917a80cd05a915174b4efb1e</Hash>
</Codenesium>*/