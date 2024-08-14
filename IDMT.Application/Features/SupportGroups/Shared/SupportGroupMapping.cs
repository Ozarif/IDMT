using IDMT.Domain.SupportGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Application.Features.SupportGroups.Shared;

internal static class SupportGroupMapping
{
    public static SupportGroupResponse ToResponse(this SupportGroup supportGroup)
    {
        return new()
        {
            Id = supportGroup.Id,
            Name = supportGroup.Name.Value,
            Email = supportGroup.Email.Value,
            IsActive = supportGroup.IsActive.Value
        };
    }


    public static IQueryable<SupportGroupResponse> ToIQueryableResponse(this IQueryable<SupportGroup> source)
    {
        return source.Select(sg => new SupportGroupResponse
        {
            Id = sg.Id,
            Name = sg.Name.Value,
            Email = sg.Email.Value,
            IsActive = sg.IsActive.Value
        });
    }
}
