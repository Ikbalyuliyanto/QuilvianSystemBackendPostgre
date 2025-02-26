
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuilvianSystemBackendDev.Areas.ManajemenKesehatan.MasterData.Models
{
    [Table("MstAgama", Schema = "public")]
    public class Agama
    {
        [Key]
        public Guid AgamaId { get; set; }
        public string KodeAgama { get; set; }
        public string NamaAgama { get; set; }
    }
}
