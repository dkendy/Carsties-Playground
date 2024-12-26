using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;

public class CreateDocumentDto
{
    [Required]
    public string Name { get; set; }
 
}
