using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace SearchService.Models;

[Collection("Documents")] 
public class DocumentEfCore
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set;}
    public string Name { get; set; }

}


public class Document:Entity
{
      
    public string Name { get; set; }

}