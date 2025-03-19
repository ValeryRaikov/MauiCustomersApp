using SQLite;

namespace CustomerApp.Models
{
    [Table("customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [NotNull, MaxLength(50), Column("customer_name")]
        public string CustomerName { get; set; }

        [NotNull, MaxLength(50), Column("mobile")]
        public string Mobile { get; set; }

        [NotNull, MaxLength(100), Column("email")]
        public string Email { get; set; }
    }
}
