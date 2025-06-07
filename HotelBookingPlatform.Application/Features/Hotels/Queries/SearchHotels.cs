using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Queries
{
    public class SearchHotels : IRequest<List<Hotel>>
    {
        public string Query { get; set; }
    }

    public class SearchHotelsHandler : IRequestHandler<SearchHotels, List<Hotel>>
    {
        private readonly IHotelRepository _hotelRepository;

        public SearchHotelsHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<List<Hotel>> Handle(SearchHotels request, CancellationToken cancellationToken)
        {
            return await _hotelRepository.SearchHotelsAsync(request.Query);
        }
    }
}