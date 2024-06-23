namespace ConcertAll.Dto.Request
{
    public class PaginationDto
    {
        private readonly int maxRecordsPerpage = 50;
        public int Page { get; set; } = 1;
        private int recordsPerPage = 10;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = value > maxRecordsPerpage ? maxRecordsPerpage : value; }
        }

    }
}
