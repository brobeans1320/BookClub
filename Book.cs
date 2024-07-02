namespace BookCubAPI
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        //public int YearPublished { get; set; }
        public DateTime PublicationDate {  get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string CoverImageUrl { get; set; }
        public List<string> Tags { get; set; }
        public bool IsRead { get; set; }
    }
}
