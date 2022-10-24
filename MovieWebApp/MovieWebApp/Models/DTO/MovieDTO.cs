﻿
namespace MovieAPI.Models.DTO
{
    public class MovieDTO
    {
        public Guid MovieID { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Country { get; set; }
        public string Actor { get; set; }
        public string Director { get; set; }
        public string Language { get; set; }
        public string Subtitle { get; set; }
        public DateTime ReleaseTime { get; set; }
        public DateTime PublicationTime { get; set; }
        public string CoverImage { get; set; }
        public string Age { get; set; }
        public string MovieURL { get; set; }
        public float RunningTime { get; set; }
        public string Quality { get; set; }
        public float Rating { get; set; }
        public bool IsVisible { get; set; }
        //Relationship
        // Profile user (author)
        public string FirstNameAuthor { get; set; }
        public string LastNameAuthor { get; set; }
        // Classification Name
        public string ClassName { get; set; }
        // Movie Type
        public string MovieTypeName { get; set; }
        // Movie Genre
        public List<string> Genres { get; set; }
    }
}
