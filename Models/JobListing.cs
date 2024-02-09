namespace CareerSharp.Models
{
    public class JobListing
    {
        public long ID { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? PostingURL { get; set; }
        public ulong? Salary { get; set; }
        public string? Description { get; set; }

        public JobListing(long iD, string? title, string? location, string? postingURL, ulong? salary, string? description)
        {
            ID = iD;
            Title = title;
            Location = location;
            PostingURL = postingURL;
            Salary = salary;
            Description = description;
        }
    }
}
