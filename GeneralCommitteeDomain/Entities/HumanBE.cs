﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneralCommittee.Domain.Constants;

// Written By Marcelino , Reviewed by Mohab
// Reviewed No Edits
namespace GeneralCommittee.Domain.Entities;

public class HumanBe
{
    [MaxLength(Global.MaxNameLength)]
    [Required]
    public string FName { get; set; }

    [Required]
    [MaxLength(Global.MaxNameLength)]
    public string LName { get; set; }

    // nav property 
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public User User { get; set; }
}