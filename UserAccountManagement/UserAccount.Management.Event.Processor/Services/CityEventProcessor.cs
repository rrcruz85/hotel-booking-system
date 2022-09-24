﻿using Hotel.Booking.Common.Constant;
using UserAccount.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;
using UserAccount.Management.Event.Processor.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.Event.Processor.Services
{
    [ExcludeFromCodeCoverage]
    public class CityEventProcessor : ICityEventProcessor
    {
        private readonly ICityService _cityService;

        public CityEventProcessor(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task ProcessCityEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)CityEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.City>(eventPayload);
                        await _cityService.CreateCityAsync(@event);
                    }
                    break;
                case (int)CityEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.City>(eventPayload);
                        await _cityService.UpdateCityAsync(@event);
                    }
                    break;
                case (int)CityEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _cityService.DeleteCityAsync(@event);
                    }
                    break;
            }
        }
    }
}
