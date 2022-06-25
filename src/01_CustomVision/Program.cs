//dotnet add package Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training --version 2.0.0
//dotnet add package Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction --version 2.0.0


using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;

string predictionEndpoint = "https://hitwtalk-prediction.cognitiveservices.azure.com/";
string predictionKey = "aa25a5f8d5584520baa23b8d162547f9";
Guid projectId = Guid.Parse("a4e23698-6745-4e2f-a050-4a9da63eb921");

var publishedModels = new string[] { "HitwSample", "HitwSampleV2", "HitwSampleV3" };

foreach (var model in publishedModels)
{
    Console.WriteLine($"");
    Console.WriteLine($"");
    Console.WriteLine($"==============================================");
    Console.WriteLine($"Starting prediction with {model}");

    Console.WriteLine("Test : berger_allemand.jpg");
    TestIteration("Tests/berger_allemand.jpg", model);

    Console.WriteLine("Test : husky.jpg");
    TestIteration("Tests/husky.jpg", model);

    Console.WriteLine("Test : chat-persan.jpg");
    TestIteration("Tests/chat-persan.jpg", model);

    Console.WriteLine("Test : lapin.jpg");
    TestIteration("Tests/lapin.jpg", model);

    Console.ReadKey();
}


CustomVisionPredictionClient GetClient()
{
    CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(predictionKey))
    {
        Endpoint = predictionEndpoint
    };
    return predictionApi;
}

void TestIteration(string imagePath, string publishedModelName)
{
    Console.WriteLine("Making a prediction:");
    var client = GetClient();
    var testImage = new MemoryStream();
    var fileStream = File.Open(imagePath, FileMode.Open);
    fileStream.CopyTo(testImage);
    fileStream.Close();
    testImage.Seek(0, SeekOrigin.Begin);
    var result = client.ClassifyImage(projectId, publishedModelName, testImage);
    foreach (var c in result.Predictions)
    {
        Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
    }
}