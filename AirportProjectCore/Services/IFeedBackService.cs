using AirportProjectCore.Models;

namespace AirportProjectCore.Services
{
    public interface IFeedBackService
    {
        void Add(FeedBack Info);
        List<FeedBack> Get();
    }
}
