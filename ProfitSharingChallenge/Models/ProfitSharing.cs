using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfitSharingChallenge.Models
{
    public interface IProfitSharing
    {
        Task<ReturnItem> GetParticipation(string total_disponibilizado);
    }

    public class ProfitSharing : IProfitSharing
    {
        private readonly IEmployeesData _data;

        public ProfitSharing(IEmployeesData data)
        {
            _data = data;
        }

        /* 
         * Update profit sharing participation for database employees
         * 
         * Bonus Formula:
         *       (SB*PTA) + (SB*PAA)
         *       ___________________ * 12
         *             (SB*PFS)
         *
         */
        public ParticipationItem CalculateParticipation(EmployeeItem employee)
        {
            if (employee == null)
                return null;

            // Parameters
            bool estagiario = employee.cargo == "Estagiario";
            var sb = BrlStringToDouble(employee.salario_bruto);
            var pta = PTA(employee.data_de_admissao);
            var paa = PAA(employee.area);
            var pfs = PFS(employee.salario_bruto, estagiario);
            var participation_value = ((sb * pta) + (sb * paa)) / (sb * pfs) * 12;

            // New Participation
            ParticipationItem newParticipation = new ParticipationItem();
            newParticipation.nome = employee.nome;
            newParticipation.matricula = employee.matricula;
            newParticipation.valor_da_participacao = String.Format("R$ {0:0.0,0}", participation_value);
            return newParticipation;
        }

        /*
         * Return Participation for all employees
         */
        public async Task<ReturnItem> GetParticipation(string total_disponibilizado)
        {
            ReturnItem r = new ReturnItem();
            List<ParticipationItem> pl = new List<ParticipationItem>();
            EmployeeItem[] employees = await _data.GetEmployeesAsync();
            int total_de_funcionarios = 0;
            double total_distribuido = 0.0;

            foreach (EmployeeItem e in employees)
            {
                ParticipationItem newParticipation = CalculateParticipation(e);
                pl.Add(newParticipation);
                total_de_funcionarios += 1;
                total_distribuido += BrlStringToDouble(newParticipation.valor_da_participacao);
            }

            r.participacoes = pl;
            r.total_de_funcionarios = total_de_funcionarios.ToString();
            r.total_distribuido = String.Format("R$ {0:0.0,0}", total_distribuido);
            r.total_disponibilizado = String.Format("R$ {0:0.0,0}", BrlStringToDouble(total_disponibilizado));
            r.saldo_total_distponibilizado = String.Format("R$ {0:0.0,0}", BrlStringToDouble(total_disponibilizado) - total_distribuido);

            return r;
        }

        // Helper Methods

        /*
         * Calculate gross salary
         */
        private double BrlStringToDouble(string salario_bruto)
        {
            var a = Regex.Replace(salario_bruto, "[R$ .]", string.Empty);

            return double.Parse(Regex.Replace(salario_bruto, "[R$ .]", string.Empty),
                            System.Globalization.CultureInfo.InstalledUICulture);
        }

        /* 
         * Calculate weight due admission time
         */
        private int PTA(string data_de_adimissiao)
        {
            DateTime today = DateTime.Today;
            DateTime admission = DateTime.ParseExact(data_de_adimissiao, "yyyy-mm-dd",
                null);

            var admissionTime = (today - admission).Days / 365;

            if (admissionTime <= 1) return 1;
            if (admissionTime <= 3) return 2;
            if (admissionTime <= 8) return 3;
            return 5;
        }

        /*
         * Calculate weight due salary range
         */
        private int PFS(string salario_bruto, bool estagiario)
        {
            var minWage = 998.0; // Hard coded
            var salaryRange = BrlStringToDouble(salario_bruto) / minWage;

            if (estagiario || salaryRange <= 3) return 1;
            if (salaryRange <= 5) return 2;
            if (salaryRange <= 8) return 3;
            return 5;
        }

        /*
         * Calculate weight due area
         */
        private int PAA(string area)
        {
            switch (area)
            {
                case "Diretoria":
                    return 1;
                case "Contabilidade":
                case "Financeiro":
                case "Tecnologia":
                    return 2;
                case "Servicos Gerais":
                    return 3;
                case "Relacionamento com o Cliente":
                    return 5;
                default:
                    return 1;
            }
        }
    }
}
