using PlatformServcice.Dtos;

namespace PlatformServcice.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto publishedDto);
    }
}
