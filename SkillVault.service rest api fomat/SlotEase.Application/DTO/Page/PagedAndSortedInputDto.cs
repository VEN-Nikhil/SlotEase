using System;
using System.ComponentModel.DataAnnotations;
using SlotEase.Domain.Constants;

namespace SlotEase.Application.DTO.Page;

public class PagedAndSortedInputDto
{
    public string Sorting { get; set; }
    [Range(1, ClientspaceConstants.MaxPageSize)]
    public int MaxResultCount { get; set; }

    [Range(0, int.MaxValue)]
    public int SkipCount { get; set; }
    public PagedAndSortedInputDto()
    {
        MaxResultCount = ClientspaceConstants.DefaultPageSize;
    }
}
