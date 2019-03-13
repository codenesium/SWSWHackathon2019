import * as Api from '../../api/models';
import BankAccountViewModel from './bankAccountViewModel';
import ArtistViewModel from '../artist/artistViewModel';
export default class BankAccountMapper {
  mapApiResponseToViewModel(
    dto: Api.BankAccountClientResponseModel
  ): BankAccountViewModel {
    let response = new BankAccountViewModel();
    response.setProperties(
      dto.accountNumber,
      dto.artistId,
      dto.id,
      dto.routingNumber
    );

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
        dto.artistIdNavigation.venmo,
        dto.artistIdNavigation.website
      );
    }

    return response;
  }

  mapViewModelToApiRequest(
    model: BankAccountViewModel
  ): Api.BankAccountClientRequestModel {
    let response = new Api.BankAccountClientRequestModel();
    response.setProperties(
      model.accountNumber,
      model.artistId,
      model.id,
      model.routingNumber
    );
    return response;
  }
}


/*<Codenesium>
    <Hash>b494600505daa3694c78dfd546912da9</Hash>
</Codenesium>*/