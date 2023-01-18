using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionPictureQuery : IQuery<ConfectionPictureViewModel>
    {
        [DataMember]
        public Guid ConfectionId { get; set; }

        public GetConfectionPictureQuery(Guid confectionId)
        {
            ConfectionId = confectionId;
        }
    }

    public class GetConfectionPictureQueryHandler : IRequestHandler<GetConfectionPictureQuery, ConfectionPictureViewModel>
    {
        private readonly IConfectionPictureRepository _confectionPictureRepository;
        private readonly IMapper _mapper;

        public GetConfectionPictureQueryHandler(IConfectionPictureRepository confectionPictureRepository, IMapper mapper)
        {
            _confectionPictureRepository = confectionPictureRepository ?? throw new ArgumentNullException(nameof(confectionPictureRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ConfectionPictureViewModel> Handle(GetConfectionPictureQuery request, CancellationToken cancellationToken)
        {
            var confectionPicture = await _confectionPictureRepository.GetByConfectionIdAsync(request.ConfectionId);

            return _mapper.Map<ConfectionPictureViewModel>(confectionPicture);
        }
    }
}   
