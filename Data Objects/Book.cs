namespace BookClubAPI
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTime PublicationDate {  get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string CoverImageUrl { get; set; }
        public List<string> Tags { get; set; }
        public bool IsRead { get; set; }
    }
}
