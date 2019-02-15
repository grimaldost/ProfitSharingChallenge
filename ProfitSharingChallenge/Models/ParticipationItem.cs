using System.ComponentModel.DataAnnotations;

namespace ProfitSharingChallenge.Models
{
    public class ParticipationItem
    {
        [Key]
        public string matricula { get; set; }
        public string nome { get; set; }
        public string valor_da_participacao { get; set; }
    }
}
