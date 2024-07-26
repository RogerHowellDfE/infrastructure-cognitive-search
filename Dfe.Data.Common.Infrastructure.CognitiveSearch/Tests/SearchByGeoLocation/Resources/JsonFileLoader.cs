namespace Dfe.Data.Common.Infrastructure.CognitiveSearch.Tests.SearchByGeoLocation.Resources
{
    public static class JsonFileLoader
    {
        public static async Task<string> LoadJsonFile(string filePath)
        {
            string? rawJson = null;

            using (StreamReader streamReader = new(Path.GetFullPath(filePath)))
            {
                rawJson = await streamReader.ReadToEndAsync();
            }

            return rawJson;
        }
    }
}
