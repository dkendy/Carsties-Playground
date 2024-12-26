using System;
using System.Reflection.Metadata;
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controller;

[ApiController]
[Route("api/documents")]
public class DocumentController: ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public DocumentController(AuctionDbContext context, IMapper mapper,
        IPublishEndpoint publishEndpoint){
        this._context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<DocumentDto>>> GetAllDocuments(){
        var documents = await _context.Documents
            .ToListAsync();

        return _mapper.Map<List<DocumentDto>>(documents);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDto>> GetDocumentById(Guid Id){
        
        var document = await _context.Documents 
            .FirstOrDefaultAsync(x=>x.Id == Id);

            if(document == null) return NotFound();

        return _mapper.Map<DocumentDto>(document); 
    }

    [HttpPost]
    public async Task<ActionResult<DocumentDto>> CreateDocument(CreateDocumentDto documentDto)
    {
        var document = _mapper.Map<Entities.Document>(documentDto);
        _context.Documents.Add(document);

        var newDocument = _mapper.Map<DocumentDto>(document); 

        await _publishEndpoint.Publish(_mapper.Map<DocumentCreated>(newDocument));

        var result = await _context.SaveChangesAsync() >0;
        
        if(!result) return BadRequest("Could not save");

        return CreatedAtAction(nameof(GetDocumentById),new { document.Id}, _mapper.Map<DocumentDto>(document));

    }
 
}
