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
    <Hash>094abc0e7b68c77a977756e9ac248777</Hash>
</Codenesium>*/