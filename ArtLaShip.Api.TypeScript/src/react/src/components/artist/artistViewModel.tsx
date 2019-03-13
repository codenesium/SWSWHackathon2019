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
    this.website = website;
  }

  toDisplay(): string {
    return String(this.name);
  }
}


/*<Codenesium>
    <Hash>3f2e749b795766343e3008de553cb2a4</Hash>
</Codenesium>*/