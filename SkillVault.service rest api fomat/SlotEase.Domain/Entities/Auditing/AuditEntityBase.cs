﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using SlotEase.Domain.Interfaces;

namespace SlotEase.Domain.Entities.Auditing;

/// <summary>
/// A shortcut of <see cref="FullAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
/// </summary>
[Serializable]
public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity
{

}

/// <summary>
/// Implements <see cref="IFullAudited"/> to be a base class for full-audited entities.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited, IDelete
{
    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual long? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime { get; set; }
}

/// <summary>
/// Implements <see cref="IFullAudited{TUser}"/> to be a base class for full-audited entities.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited<TUser>, IDelete
    where TUser : IEntity<long>
{
    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Reference to the deleter user of this entity.
    /// </summary>
    [ForeignKey("DeleterUserId")]
    public virtual TUser DeleterUser { get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual long? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime { get; set; }
}
