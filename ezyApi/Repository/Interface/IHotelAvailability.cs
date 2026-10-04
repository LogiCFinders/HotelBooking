using ezyApi.Models;

namespace ezyApi.Repository.Interface
{
    public interface IHotelAvailability
    {
        AvailResponse getHotelAvailaibility(AvailRequest availRequest);
    }
}
