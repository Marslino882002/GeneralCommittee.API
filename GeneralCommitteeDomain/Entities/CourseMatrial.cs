using System.ComponentModel.DataAnnotations;
using GeneralCommittee.Domain.Constants;
// written By Mohab , No Reviews Yet
namespace GeneralCommittee.Domain.Entities;

public class CourseMateriel
{
    public int CourseMaterielId { get; set; }

    //
    public int CourseId { get; set; }

    public Course Course { get; set; } = default!;

    //
    public int AdminId { get; set; }

    public Admin Admin { get; set; } = default!;

    //
    [MaxLength(Global.TitleMaxLength)] 
    public string? Title { set; get; }

    //todo add max length to description 
    public string? Description { set; get; }

    //
    public int ItemOrder { get; set; }

    [MaxLength(Global.UrlMaxLength)] public string Url { set; get; } = default!;

    

    public bool IsVideo { set; get; }
    //
}