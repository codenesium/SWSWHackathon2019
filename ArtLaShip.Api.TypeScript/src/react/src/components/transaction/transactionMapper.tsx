import * as Api from '../../api/models';
import TransactionViewModel from './transactionViewModel';
import ArtistViewModel from '../artist/artistViewModel';
export default class TransactionMapper {
  mapApiResponseToViewModel(
    dto: Api.TransactionClientResponseModel
  ): TransactionViewModel {
    let response = new TransactionViewModel();
    response.setProperties(
      dto.amount,
      dto.artistId,
      dto.dateCreated,
      dto.id,
      dto.stripeTransactionId
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
    model: TransactionViewModel
  ): Api.TransactionClientRequestModel {
    let response = new Api.TransactionClientRequestModel();
    response.setProperties(
      model.amount,
      model.artistId,
      model.dateCreated,
      model.id,
      model.stripeTransactionId
    );
    return response;
  }
}


/*<Codenesium>
    <Hash>cc185f1ec6c5b6e33183e4522ead7921</Hash>
</Codenesium>*/