using System;

namespace SearchService.Models;

public class SearchDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ItemsCollectionName { get; set; } = null!;
}
