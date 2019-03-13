import moment from 'moment';
import ArtistViewModel from '../artist/artistViewModel';

export default class EmailViewModel {
  artistId: number;
  artistIdEntity: string;
  artistIdNavigation?: ArtistViewModel;
  dateCreated: any;
  email1: string;
  id: number;

  constructor() {
    this.artistId = 0;
    this.artistIdEntity = '';
    this.artistIdNavigation = new ArtistViewModel();
    this.dateCreated = undefined;
    this.email1 = '';
    this.id = 0;
  }

  setProperties(
    artistId: number,
    dateCreated: any,
    email1: string,
    id: number
  ): void {
    this.artistId = artistId;
    this.dateCreated = moment(dateCreated, 'YYYY-MM-DD');
    this.email1 = email1;
    this.id = id;
  }

  toDisplay(): string {
    return String(this.artistId);
  }
}


/*<Codenesium>
    <Hash>235b69cb731bfa8ee9789e2a533d9988</Hash>
</Codenesium>*/