using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.Domain.IRepositories;
using Confectionery.Infrastructure.QueryProcessing;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionsQuery : IQuery<List<ConfectionViewModel>>
    {
        public QueryParameters QueryParameters { get; set; }

        public GetConfectionsQuery(QueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
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
            var paginatedConfections = await _confectionRepository.GetConfectionsWithPicturesAsync(request.QueryParameters);

            return _mapper.Map<List<ConfectionViewModel>>(paginatedConfections.Items);
        }
    }
}
