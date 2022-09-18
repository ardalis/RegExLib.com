using System.Text.Json;

namespace RegExLib.BlazorShared;
public class Result
{
  public bool IsSuccess { get; set; }
  public string Message { get; set; } = string.Empty;
  public object? Data { get; set; }
  public int Count { get; set; }
  private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };

  public Result() { }

  public static Result SuccessResult()
  {
    var result = new Result(true);

    return result;
  }

  public static Result DeserializeJson<T>(string jsonString)
  {
    var result = JsonSerializer.Deserialize<Result>(jsonString, _jsonOptions);

    var unknownObject = JsonDocument.Parse(jsonString);

    if (unknownObject.RootElement.TryGetProperty("data", out var dataJsonElement))
    {
      if (dataJsonElement.ValueKind != JsonValueKind.Null)
        result!.Data = dataJsonElement.Deserialize<T>(_jsonOptions)!;
    }

    return result;
  }

  public static Result DeserializeJsonWithoutData(string jsonString)
  {
    var result = JsonSerializer.Deserialize<Result>(jsonString, _jsonOptions);
    result!.Data = null;

    return result;
  }

  public static Result SuccessWithDataResult<T>(T data)
  {
    var result = new Result(true)
    {
      Data = data!,
      Count = 1
    };

    return result;
  }

  public static Result SuccessWithDataResult<T>(int count, T data)
  {
    var result = new Result(true)
    {
      Data = data!,
      Count = count
    };

    return result;
  }

  public static Result ErrorResult(string message)
  {
    var result = new Result(false, message);

    return result;
  }

  public T GetData<T>() where T : new()
  {
    return (T)Data;
  }

  private Result(bool isSuccess)
  {
    IsSuccess = isSuccess;
  }

  private Result(bool isSuccess, string message)
  {
    IsSuccess = isSuccess;
    Message = message;
  }
}
