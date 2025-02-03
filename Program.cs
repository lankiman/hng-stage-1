
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Routing.Template;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddHttpClient();

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

var app = builder.Build();

app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET"));

static (bool, string?, int?) IsValidNumber(string number)
{
    if (int.TryParse(number, out int num))
    {
        return (true, "number", num);
    }

    // if (Regex.IsMatch(number, @"[a-zA-Z]") || Regex.IsMatch(number, @"[\W_]"))
    // {
    //     return (false, "alphabet");
    // }
    
    return (false, "alphabet", null);
};

static bool CheckArmstrong(int number)
{
    string numberString = number.ToString();
    int numberDigits = numberString.Length;
    int temp = number;
    int sum = 0;
    while (temp>0)
    {
        int digit = temp % 10;
        sum += (int)Math.Pow(digit, numberDigits);
        temp /= 10;
    }
    return number==sum;
}

static string IsOddOrEven(int number)
{
    if (number % 2 == 0)
    {
        return "even";
    }

    return "odd";
}

static bool IsPrime(int number)
{
    if (number<2) return false;
    for (int i = 2; i * i <= number; i++)
    {
        if (number % i == 0) return false;
    }

    return true;
}

static bool IsPerfect(int number)
{
    if (number<2) return false;
    int sumOfDivisors = 1;
    for(int i=2; i*i <=number; i++)
    {
        if (number % i == 0)
        {
            sumOfDivisors += i;  
            if (i != number / i)  
            {
                sumOfDivisors += number / i;  
            }
        }
        
    }
    return sumOfDivisors == number;
}

static int SumOfNumberDigits(int number)
{
    number = Math.Abs(number);
    int sum = 0;
    while (number>0)
    {
        sum += number % 10;  
        number /= 10;  
    }
    return sum;
}

static async Task<(bool, string?)> GetFunFactFromNumbersApi(HttpClient httpClient, int number)
{
    try
    {
        var apiUrl = $"http://numbersapi.com/{number}/math";
        var funFact = await httpClient.GetStringAsync(apiUrl);
        return (true, funFact);
    }
    catch (Exception ex)
    {
        return (false, $"An error occurred while fetching the fun fact: {ex.Message}");
    }
}


app.MapGet("/", () => "Health check passed");

app.MapGet("/api/classify-number", async (string? number, IHttpClientFactory httpClient) =>
{
    if (String.IsNullOrEmpty(number))
    {
        return Results.BadRequest(new
        {
            message = "required endpoint parameter number not provided, please provide one",
            error = true
        });
    }
    var (status, error, num) = IsValidNumber(number);
    if (!status)
    {
        return Results.BadRequest(new
        {
            number=error,
            error=status,
        });
    }

    var client = httpClient.CreateClient();
    var (funFactStatus, funFact) = await GetFunFactFromNumbersApi(client, num!.Value);

    if (!funFactStatus)
    {
        return Results.InternalServerError(new
        {
            message="An error occured",
            error=!funFactStatus,
        });
    }

    ;
    var properties = new List<string>(2);
    var armstrong = CheckArmstrong(num.Value);

    if (armstrong)
    {
        properties.Add("armstrong");
        properties.Add(IsOddOrEven(num.Value)); 
    }
    else
    {
        properties.Add(IsOddOrEven(num.Value)); 
    }
    var results = new
    {
        number = num.Value,
        is_prime = IsPrime(num.Value),
        is_perfect = IsPerfect(num.Value),
        properties= properties,
        digit_sum= SumOfNumberDigits(num.Value),
        fun_fact= funFact
    };

    return Results.Ok(results);

});

app.Run();


