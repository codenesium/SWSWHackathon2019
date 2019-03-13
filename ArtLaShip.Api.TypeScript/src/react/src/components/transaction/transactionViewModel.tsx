import moment from 'moment';
import ArtistViewModel from '../artist/artistViewModel';

export default class TransactionViewModel {
  amount: number;
  artistId: number;
  artistIdEntity: string;
  artistIdNavigation?: ArtistViewModel;
  dateCreated: any;
  id: number;
  stripeTransactionId: string;

  constructor() {
    this.amount = 0;
    this.artistId = 0;
    this.artistIdEntity = '';
    this.artistIdNavigation = new ArtistViewModel();
    this.dateCreated = undefined;
    this.id = 0;
    this.stripeTransactionId = '';
  }

  setProperties(
    amount: number,
    artistId: number,
    dateCreated: any,
    id: number,
    stripeTransactionId: string
  ): void {
    this.amount = amount;
    this.artistId = artistId;
    this.dateCreated = moment(dateCreated, 'YYYY-MM-DD');
    this.id = id;
    this.stripeTransactionId = stripeTransactionId;
  }

  toDisplay(): string {
    return String(this.amount);
  }
}


/*<Codenesium>
    <Hash>9ac27953305ba79b68938df199ea2104</Hash>
</Codenesium>*/