using Confectionery.API.Application.Interfaces;
using Confectionery.API.Application.ViewModels;
using Confectionery.API.Application.ViewModels.Common;
using Confectionery.Domain.IRepositories;
using Confectionery.Infrastructure.QueryProcessing;
using MapsterMapper;
using MediatR;
using System.Runtime.Serialization;

namespace Confectionery.API.Application.Queries.Confection
{
    [DataContract]
    public class GetConfectionsQuery : IQuery<PagedListViewModel<ConfectionViewModel>>
    {
        public QueryParameters QueryParameters { get; set; }

        public GetConfectionsQuery(QueryParameters queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class GetConfectionsQueryHandler : IRequestHandler<GetConfectionsQuery, PagedListViewModel<ConfectionViewModel>>
    {
        private readonly IConfectionRepository _confectionRepository;
        private readonly IMapper _mapper;

        public GetConfectionsQueryHandler(IConfectionRepository confectionRepository, IMapper mapper)
        {
            _confectionRepository = confectionRepository ?? throw new ArgumentNullException(nameof(confectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedListViewModel<ConfectionViewModel>> Handle(GetConfectionsQuery request, CancellationToken cancellationToken)
        {
            var paginatedConfections = await _confectionRepository.GetConfectionsWithPicturesAsync(request.QueryParameters);

            return _mapper.Map<PagedListViewModel<ConfectionViewModel>>(paginatedConfections);
        }
    }
}
