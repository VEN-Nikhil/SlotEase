﻿using System;

namespace SlotEase.Domain.DTO;

public class RoleDto<TKey> where TKey : IEquatable<TKey>
{
    //
    // Summary:
    //     Gets or sets the primary key for this role.
    public TKey Id { get; set; }
    //
    // Summary:
    //     Gets or sets the name for this role.
    public string Name { get; set; }
    //
    // Summary:
    //     Gets or sets the normalized name for this role.
    public string NormalizedName { get; set; }
    //
    // Summary:
    //     A random value that should change whenever a role is persisted to the store
    public string ConcurrencyStamp { get; set; }

}
