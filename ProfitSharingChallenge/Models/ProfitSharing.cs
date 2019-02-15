using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ProfitSharingChallenge.Models
{
    public interface IProfitSharing
    {
        void UpdateParticipation(string matricula);
        ReturnItem GetParticipation();
        ReturnItem GetParticipation(string matricula);
    }

    public class ProfitSharing : IProfitSharing
    {
        private readonly EmployeeContext _context;
        private readonly IReturnData _returnData;

        public ProfitSharing(EmployeeContext context, IReturnData returnData)
        {
            _context = context;
            _returnData = returnData;
        }

        /* 
         * Update profit sharing participation for new employee
         * 
         * Bonus Formula:
         *       (SB*PTA) + (SB*PAA)
         *       ___________________ * 12
         *             (SB*PFS)
         * 
         *  Participation only depends on each employee data, so it's
         *  necessary update just when a new employee is inserted.
         */
        public void UpdateParticipation(string matricula)
        {
            EmployeeItem employee = _context.employees.Find(matricula);

            if (employee == null)
                return;

            // Parameters
            bool estagiario = employee.cargo == "Estagiário";
            var sb = SB(employee.salario_bruto);
            var pta = PTA(employee.data_de_admissao);
            var paa = PAA(employee.area);
            var pfs = PFS(employee.salario_bruto, estagiario);
            var participation_value = ((sb * pta) + (sb * paa)) / (sb * pfs) * 12;

            // New Participation
            ParticipationItem newParticipation = new ParticipationItem();
            newParticipation.nome = employee.nome;
            newParticipation.matricula = employee.matricula;
            newParticipation.valor_da_participacao = String.Format("R$ {0:0.0,0}", participation_value);
            _context.participation.Add(newParticipation);
            _context.SaveChanges();

            // Update general parameters
            _returnData.SetTFunc(_returnData.GetTFunc() + 1);
            _returnData.SetTDist(_returnData.GetTDist() + participation_value);
        }

        /*
         * Return Participation for all employees
         */
        public ReturnItem GetParticipation()
        {
            ReturnItem r = new ReturnItem();
            List<ParticipationItem> pl = new List<ParticipationItem>();

            foreach(ParticipationItem p in _context.participation)
            {
                pl.Add(p);
            }

            r.participacoes = pl;
            r.total_de_funcionarios = _returnData.GetTFunc().ToString();
            r.total_distribuido = String.Format("R$ {0:0.0,0}", _returnData.GetTDist());
            r.total_disponibilizado = String.Format("R$ {0:0.0,0}", _returnData.GetTDisp());
            r.saldo_total_distponibilizado = String.Format("R$ {0:0.0,0}", _returnData.GetTDisp() - _returnData.GetTDist());

            return r;
        }

        /*
         * Return participation for a specific employee defined by its ID
         */
        public ReturnItem GetParticipation(string matricula)
        {
            ReturnItem r = new ReturnItem();
            List<ParticipationItem> pl = new List<ParticipationItem>();
            ParticipationItem p = _context.participation.Find(matricula);
            if (p != null)
                pl.Add(p);

            r.participacoes = pl;
            r.total_de_funcionarios = _returnData.GetTFunc().ToString();
            r.total_distribuido = String.Format("R$ {0:0.0,0}", _returnData.GetTDist());
            r.total_disponibilizado = String.Format("R$ {0:0.0,0}", _returnData.GetTDisp());
            r.saldo_total_distponibilizado = String.Format("R$ {0:0.0,0}", _returnData.GetTDisp() - _returnData.GetTDist());

            return r;
        }

        // Helper Methods

        /*
         * Calculate gross salary
         */
        private double SB(string salario_bruto)
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
            var salaryRange = SB(salario_bruto) / minWage;

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
                case "Serviços Gerais":
                    return 3;
                case "Relacionamento com o Cliente":
                    return 5;
                default:
                    return 1;
            }
        }
    }
}
