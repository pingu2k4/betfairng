namespace Betfair.ESAClient.Cache
{
    public class Utils
    {
        public static double SelectPrice(bool isImage, ref double currentPrice, double? newPrice)
        {
            if (isImage)
            {
                currentPrice = newPrice ?? 0.0;
            }
            else
            {
                currentPrice = newPrice ?? currentPrice;
            }
            return currentPrice;
        }
    }
}