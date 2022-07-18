using System.Text.Json;
using System.Text.Json.Nodes;

public static class RouteMiddlewareExtensions
{
    public static WebApplication UseExtraRoutes(this WebApplication app)
    {
        var writableDoc = JsonNode.Parse(File.ReadAllText("mock.json"));

        foreach (var elem in writableDoc?.Root.AsObject().AsEnumerable())
        {
            var arr = elem.Value.AsArray();
            app.MapGet(string.Format("/{0}", elem.Key), () => elem.Value.ToString());
        }

        return app;
    }
}