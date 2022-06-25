//dotnet add package Microsoft.Azure.CognitiveServices.Language.LUIS.Authoring --version 3.2.0-preview.3
//dotnet add package Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime --version 3.1.0-preview.1

using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json;

var key = "adfeb70d6b294bddbf8180ac5b29d0d8";
var predictionEndpoint = "https://hitw.cognitiveservices.azure.com/";
var appId = Guid.Parse("6fb6e131-2669-473a-b98d-d28cb71b0f7b");
var credentials = new Microsoft.Azure.CognitiveServices.Language.LUIS.Authoring.ApiKeyServiceClientCredentials(key);
var runtimeClient = new LUISRuntimeClient(credentials) { Endpoint = predictionEndpoint };

var phrases = new string[] {
    "Va-t-il pleuvoir demain à Mons ?",
    "Va-t-il pleuvoir toute cette semaine ?",
    "Va-t-il pleuvoir à cinq heures du matin demain ?"
};

foreach (var p in phrases)
{
    Console.WriteLine($"");
    Console.WriteLine($"==============================================");
    Console.WriteLine($"Starting prediction with ## {p} ##");
    var request = new PredictionRequest { Query = p };
    var prediction = await runtimeClient.Prediction.GetSlotPredictionAsync(appId, "Staging", request);
    Console.Write(JsonConvert.SerializeObject(prediction, Formatting.Indented));
    Console.ReadKey();
}
