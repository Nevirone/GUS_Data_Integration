using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("Data")]
    public class Data
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("Length")]
        public int? Length { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Province_Name")]
        public string Province { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("First_Value_Name")]
        public string FirstValueName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Second_Value_Name")]
        public string SecondValueName { get; set; }


        [Column(TypeName = "json")]
        public string FirstValues { get; set; }

        [Column(TypeName = "json")]
        public string SecondValues { get; set; }

        [Column(TypeName = "json")]
        public string Years { get; set; }

        [Required]
        [Column("Modified_At", TypeName = "datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModificationDate { get; set; }
    }
}
