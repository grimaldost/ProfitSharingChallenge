using System.Collections.Generic;

namespace ProfitSharingChallenge.Models
{
    public class ReturnItem
    {
        public List<ParticipationItem> participacoes { get; set; }
        public string total_de_funcionarios { get; set; }
        public string total_distribuido { get; set; }
        public string total_disponibilizado { get; set; }
        public string saldo_total_distponibilizado { get; set; }
    }
}
