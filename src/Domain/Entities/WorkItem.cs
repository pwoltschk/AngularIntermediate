﻿using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class WorkItem : AggregateRoot
{
    public int? ProjectId { get; set; }
    public Project? Project { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Iteration { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public Priority Priority { get; set; } = Priority.Low;
    public Stage Stage { get; set; } = Stage.Planned;
}
