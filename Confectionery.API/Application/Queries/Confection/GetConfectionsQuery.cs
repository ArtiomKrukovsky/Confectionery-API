using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionsQuery : IQuery<List<ConfectionViewModel>>
    {
        public GetConfectionsQuery()
        {
        }
    }

    public class GetConfectionsQueryHandler : IRequestHandler<GetConfectionsQuery, List<ConfectionViewModel>>
    {
        private readonly IConfectionRepository _confectionRepository;
        private readonly IMapper _mapper;

        public GetConfectionsQueryHandler(IConfectionRepository confectionRepository, IMapper mapper)
        {
            _confectionRepository = confectionRepository ?? throw new ArgumentNullException(nameof(confectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ConfectionViewModel>> Handle(GetConfectionsQuery request, CancellationToken cancellationToken)
        {
            var confections = await _confectionRepository.GetConfectionsWithPicturesAsync();

            return _mapper.Map<List<ConfectionViewModel>>(confections);
        }
    }
}
