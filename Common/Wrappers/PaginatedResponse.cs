﻿using Microsoft.EntityFrameworkCore;

namespace Movies.Common.Wrappers;

public class PaginatedResponse<T>
{
    private PaginatedResponse(int pageIndex, int pageSize, int count, IEnumerable<T> items)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public IEnumerable<T> Items { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    
    public static async Task<PaginatedResponse<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<T>(pageIndex, pageSize, count, items);
    }
}
