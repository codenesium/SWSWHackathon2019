import * as Api from '../../api/models';
import ArtistViewModel from  './artistViewModel';
export default class ArtistMapper {
    
	mapApiResponseToViewModel(dto: Api.ArtistClientResponseModel) : ArtistViewModel 
	{
		let response = new ArtistViewModel();
		response.setProperties(dto.aspNetUserId,dto.bio,dto.externalId,dto.facebook,dto.id,dto.name,dto.soundCloud,dto.twitter,dto.venmo,dto.website);
		
				

		
		
		return response;
	}

	mapViewModelToApiRequest(model: ArtistViewModel) : Api.ArtistClientRequestModel
	{
		let response = new Api.ArtistClientRequestModel();
		response.setProperties(model.aspNetUserId,model.bio,model.externalId,model.facebook,model.id,model.name,model.soundCloud,model.twitter,model.venmo,model.website);
		return response;
	}
};

/*<Codenesium>
    <Hash>ef090add950ac075b3b3585a68aa7571</Hash>
</Codenesium>*/