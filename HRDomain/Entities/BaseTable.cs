namespace HRDomain.Entities
{
    public class BaseTable
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}

// We need to search about the best practicies of doing these things
// 1] the self relationship
// 2] making the nationality as an Enum ?
// 3] how the EF ORM deal with Enums ?