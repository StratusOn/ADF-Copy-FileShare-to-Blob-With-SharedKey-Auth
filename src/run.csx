#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Security.Cryptography;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation($"HmacSha256Hash function has been invoked at {DateTime.Now}.");

    string message = req.Query["message"];
    string secret = req.Query["secret"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    message = message ?? data?.message;
    secret = secret ?? data?.secret;

    return (!string.IsNullOrWhiteSpace(message) && !string.IsNullOrWhiteSpace(secret))
        ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(new HmacSha256Hash() { base64EncodedHash = HmacSha256HashBase64Encode(message, secret) }))
        : new BadRequestObjectResult("Please pass a message and secret on the query string or in the request body");
}

private static string HmacSha256HashBase64Encode(string message, string secret)
{
    secret = secret ?? "";
    var encoding = new System.Text.UTF8Encoding();
    //byte[] keyByte = encoding.GetBytes(secret);
    byte[] keyByte = Convert.FromBase64String(secret);
    byte[] messageBytes = encoding.GetBytes(message);
    using (var hmacsha256 = new HMACSHA256(keyByte))
    {
    byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
    return Convert.ToBase64String(hashmessage);
    }
}

class HmacSha256Hash
{
    public string base64EncodedHash { get; set; }
}
