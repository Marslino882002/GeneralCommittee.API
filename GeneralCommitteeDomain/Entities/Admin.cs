﻿namespace GeneralCommittee.Domain.Entities;
// Written By Marcelino , Reviewed by Mohab
// Reviewed No Edits
public class Admin : HumanBe
{
    public int AdminId { get; set; }
    #region Navigation property => Many

    public List<Article> Articles { get; set; } = new();
    public List<Podcast> Podcasts { get; set; } = new();

    public List<Meditation> Meditations { get; set; } = new();


    public List<CourseMateriel> CourseMateriels { get; set; } = new();

    #endregion
}