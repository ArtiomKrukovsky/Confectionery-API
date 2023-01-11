using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionMappingsQuery : IQuery<List<ConfectionMappingViewModel>>
    {
    }

    public class GetConfectionMappingsQueryHandler : IRequestHandler<GetConfectionMappingsQuery, List<ConfectionMappingViewModel>>
    {
        private readonly IConfectionRepository _confectionRepository;
        private readonly IMapper _mapper;

        public GetConfectionMappingsQueryHandler(IConfectionRepository confectionRepository, IMapper mapper)
        {
            _confectionRepository = confectionRepository ?? throw new ArgumentNullException(nameof(confectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ConfectionMappingViewModel>> Handle(GetConfectionMappingsQuery request, CancellationToken cancellationToken)
        {
            var confections = await _confectionRepository.GetAsync();

            var groupedConfectionsByType = confections.GroupBy(confection => confection.Type);

            return groupedConfectionsByType
                .Select(group => new ConfectionMappingViewModel
                {
                    ConfectionType = group.Key,
                    Confections = _mapper.Map<List<ConfectionViewModel>>(group)
                })
                .ToList();
        }
    }
}
