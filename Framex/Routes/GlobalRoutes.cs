namespace Framex.Routes
{
    public static class GlobalRoutes
    {
        public static void MapGlobalRoutes(this WebApplication app)
        {
            app.UseRouting();
            app.MapGameRoutes();
            var webSocketRoutes = app.Services.GetRequiredService<WebSocketRoutes>();
            webSocketRoutes.MapWebSocketRoutes(app);
        
        }
    }
}