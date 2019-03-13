import moment from 'moment';
import ArtistViewModel from '../artist/artistViewModel';

export default class EmailViewModel {
  artistId: number;
  artistIdEntity: string;
  artistIdNavigation?: ArtistViewModel;
  dateCreated: any;
  emailValue: string;
  id: number;

  constructor() {
    this.artistId = 0;
    this.artistIdEntity = '';
    this.artistIdNavigation = new ArtistViewModel();
    this.dateCreated = undefined;
    this.emailValue = '';
    this.id = 0;
  }

  setProperties(
    artistId: number,
    dateCreated: any,
    emailValue: string,
    id: number
  ): void {
    this.artistId = artistId;
    this.dateCreated = moment(dateCreated, 'YYYY-MM-DD');
    this.emailValue = emailValue;
    this.id = id;
  }

  toDisplay(): string {
    return String(this.emailValue);
  }
}


/*<Codenesium>
    <Hash>6fac13dab72daada73196c4380e5c1bc</Hash>
</Codenesium>*/