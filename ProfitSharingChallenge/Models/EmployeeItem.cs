using System.ComponentModel.DataAnnotations;

namespace ProfitSharingChallenge.Models
{
    public class EmployeeItem
    {
        [Key]
        public string matricula { get; set; }
        public string nome { get; set; }
        public string area { get; set; }
        public string cargo { get; set; }
        public string salario_bruto { get; set; }
        public string data_de_admissao { get; set; }
    }
}
