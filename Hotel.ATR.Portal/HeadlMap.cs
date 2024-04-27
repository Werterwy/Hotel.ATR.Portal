namespace Hotel.ATR.Portal
{
    public class HeadlMap
    {
        void HeandlMapOpen(IApplicationBuilder appMap)
        {
            appMap.Run(async context =>
            {
                await context.Response.WriteAsync("hello");
            });
        }
    }
}
