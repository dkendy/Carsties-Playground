using System;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class DocumentCreatedConsumer : IConsumer<DocumentCreated>
{
    private  readonly IMapper _mapper;

    public DocumentCreatedConsumer(IMapper mapper)
    {
        _mapper=mapper;
        
    }
    public async Task Consume(ConsumeContext<DocumentCreated> context)
    {
        Console.WriteLine("--> Consuming auction created: " + context.Message.Id);

        var item = _mapper.Map<Document>(context.Message);

        await item.SaveAsync();

    }
}
