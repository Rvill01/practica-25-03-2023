using System.ComponentModel.DataAnnotations;

namespace practica_25_03_2023.Models
{
    public class facultades
    {
        [Key]
        public int facultad_id { get; set; }
        public string? nombre_facultad { get; set; }
    }
}
