import moment from 'moment';

export default class ArtistViewModel {
  aspNetUserId: string;
  bio: string;
  externalId: any;
  facebook: string;
  id: number;
  name: string;
  soundCloud: string;
  twitter: string;
  venmo: string;
  website: string;

  constructor() {
    this.aspNetUserId = '';
    this.bio = '';
    this.externalId = undefined;
    this.facebook = '';
    this.id = 0;
    this.name = '';
    this.soundCloud = '';
    this.twitter = '';
    this.venmo = '';
    this.website = '';
  }

  setProperties(
    aspNetUserId: string,
    bio: string,
    externalId: any,
    facebook: string,
    id: number,
    name: string,
    soundCloud: string,
    twitter: string,
    venmo: string,
    website: string
  ): void {
    this.aspNetUserId = aspNetUserId;
    this.bio = bio;
    this.externalId = externalId;
    this.facebook = facebook;
    this.id = id;
    this.name = name;
    this.soundCloud = soundCloud;
    this.twitter = twitter;
    this.venmo = venmo;
    this.website = website;
  }

  toDisplay(): string {
    return String(this.name);
  }
}


/*<Codenesium>
    <Hash>746680099bbb91d1a62c93f552e1a2da</Hash>
</Codenesium>*/