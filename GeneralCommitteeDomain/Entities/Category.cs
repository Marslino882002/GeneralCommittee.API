﻿using System.ComponentModel.DataAnnotations;
using GeneralCommittee.Domain;
using GeneralCommittee.Domain.Constants;

namespace GeneralCommittee.Domain.Entities;
// Written By Marcelino , Reviewed by Mohab
// Reviewed No Edits
public class Category
{
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(Global.MaxNameLength)]
    public string Name { get; set; } = default!;
    //todo add max length
    public string? Description { get; set; }


    public ICollection<Course> Courses { get; set; }
        = new HashSet<Course>();
}