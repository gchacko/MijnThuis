﻿using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MijnThuis.Integrations.Car;

public interface ICarService
{
    Task<CarOverview> GetOverview();
    Task<bool> Lock();
    Task<bool> Unlock();
    Task<bool> Honk();
    Task<bool> Fart();
    Task<bool> Preheat();
    Task<bool> OpenFrunk();
    Task<bool> ToggleTrunk();
}

public class CarService : BaseService, ICarService
{
    private readonly string _vinNumber;

    public CarService(IConfiguration configuration) : base(configuration)
    {
        _vinNumber = configuration.GetValue<string>("CAR_VIN_NUMBER");
    }

    public async Task<CarOverview> GetOverview()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<StateResponse>($"{_vinNumber}/state");

        return new CarOverview
        {
            State = "Parked",
            IsLocked = result.VehicleState.Locked,
            IsCharging = result.ChargeState.ChargingState == "Charging",
            BatteryLevel = result.ChargeState.BatteryLevel,
            RemainingRange = (int)result.ChargeState.BatteryRange,
            TemperatureInside = (int)result.ClimateState.InsideTemp,
            TemperatureOutside = (int)result.ClimateState.OutsideTemp,
            IsPreconditioning = result.ClimateState.IsPreconditioning,
            Location = "Home"
        };
    }

    public async Task<bool> Fart()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/remote_boombox");

        return result.Result;
    }

    public async Task<bool> Honk()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/remote_boombox");

        return result.Result;
    }

    public async Task<bool> Lock()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/lock");

        return result.Result;
    }

    public async Task<bool> Preheat()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/start_climate");

        return result.Result;
    }

    public async Task<bool> OpenFrunk()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/remote_boombox");

        return result.Result;
    }

    public async Task<bool> ToggleTrunk()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/lock");

        return result.Result;
    }

    public async Task<bool> Unlock()
    {
        using var client = InitializeHttpClient();
        var result = await client.GetFromJsonAsync<BaseResponse>($"{_vinNumber}/command/unlock");

        return result.Result;
    }
}

public class BaseService
{
    private readonly string _baseAddress;
    private readonly string _authToken;

    protected BaseService(IConfiguration configuration)
    {
        _baseAddress = configuration.GetValue<string>("CAR_API_BASE_ADDRESS");
        _authToken = configuration.GetValue<string>("CAR_API_AUTH_TOKEN");
    }

    protected HttpClient InitializeHttpClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_baseAddress);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

        return client;
    }
}

public class BaseResponse
{
    public bool Result { get; set; }
}

public class StateResponse
{
    [JsonPropertyName("charge_state")]
    public ChargeState ChargeState { get; set; }

    [JsonPropertyName("climate_state")]
    public ClimateState ClimateState { get; set; }

    [JsonPropertyName("vehicle_state")]
    public VehicleState VehicleState { get; set; }
}

public class ChargeState
{
    [JsonPropertyName("usable_battery_level")]
    public byte BatteryLevel { get; set; }

    [JsonPropertyName("battery_range")]
    public decimal BatteryRange { get; set; }

    [JsonPropertyName("charging_state")]
    public string ChargingState { get; set; }
}

public class ClimateState
{
    [JsonPropertyName("inside_temp")]
    public decimal InsideTemp { get; set; }

    [JsonPropertyName("outside_temp")]
    public decimal OutsideTemp { get; set; }

    [JsonPropertyName("is_preconditioning")]
    public bool IsPreconditioning { get; set; }
}

public class VehicleState
{
    [JsonPropertyName("locked")]
    public bool Locked { get; set; }
}