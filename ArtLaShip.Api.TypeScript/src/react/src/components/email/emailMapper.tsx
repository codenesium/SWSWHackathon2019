import * as Api from '../../api/models';
import EmailViewModel from './emailViewModel';
import ArtistViewModel from '../artist/artistViewModel';
export default class EmailMapper {
  mapApiResponseToViewModel(dto: Api.EmailClientResponseModel): EmailViewModel {
    let response = new EmailViewModel();
    response.setProperties(dto.artistId, dto.dateCreated, dto.email1, dto.id);

    if (dto.artistIdNavigation != null) {
      response.artistIdNavigation = new ArtistViewModel();
      response.artistIdNavigation.setProperties(
        dto.artistIdNavigation.aspNetUserId,
        dto.artistIdNavigation.bio,
        dto.artistIdNavigation.externalId,
        dto.artistIdNavigation.facebook,
        dto.artistIdNavigation.id,
        dto.artistIdNavigation.name,
        dto.artistIdNavigation.soundCloud,
        dto.artistIdNavigation.twitter,
        dto.artistIdNavigation.website
      );
    }

    return response;
  }

  mapViewModelToApiRequest(model: EmailViewModel): Api.EmailClientRequestModel {
    let response = new Api.EmailClientRequestModel();
    response.setProperties(
      model.artistId,
      model.dateCreated,
      model.email1,
      model.id
    );
    return response;
  }
}


/*<Codenesium>
    <Hash>29405d2c8edbf99512204ea48618deec</Hash>
</Codenesium>*/