using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// This interface abstracts connection & cache implementation.
    /// </summary>
    public interface IChangeMessageHandler
    {
        void OnErrorStatusNotification(StatusMessage message);

        void OnMarketChange(ChangeMessage<MarketChange> change);

        void OnOrderChange(ChangeMessage<OrderMarketChange> change);
    }
}