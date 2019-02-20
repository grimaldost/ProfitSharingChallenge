using System.Threading.Tasks;
using ProfitSharingChallenge.Models;

namespace TestProject
{
    class EmployeeDataFake : IEmployeesData
    {
        private readonly EmployeeItem[] _employees;

        public EmployeeDataFake()
        {
            // Fake Employees
            _employees = new EmployeeItem[4];
            EmployeeItem e0 = new EmployeeItem();
            e0.matricula = "0009968";
            e0.nome = "Victor Wilson";
            e0.area = "Diretoria";
            e0.cargo = "Diretor Financeiro";
            e0.salario_bruto = "R$ 12.696,20";
            e0.data_de_admissao = "2012-01-05";
            _employees[0] = e0;
            EmployeeItem e1 = new EmployeeItem();
            e1.matricula = "0005253";
            e1.nome = "Wong Austin";
            e1.area = "Financeiro";
            e1.cargo = "Economista Junior";
            e1.salario_bruto = "R$ 2.215,04";
            e1.data_de_admissao = "2016-08-27";
            _employees[1] = e1;
            EmployeeItem e2 = new EmployeeItem();
            e2.matricula = "0001843";
            e2.nome = "Daugherty Kramer";
            e2.area = "Servicos Gerais";
            e2.cargo = "Atendente de Almoxarifado";
            e2.salario_bruto = "R$ 2.120,08";
            e2.data_de_admissao = "2016-04-21";
            _employees[2] = e2;
            EmployeeItem e3 = new EmployeeItem();
            e3.matricula = "0002105";
            e3.nome = "Dorthy Lee";
            e3.area = "Financeiro";
            e3.cargo = "Estagiario";
            e3.salario_bruto = "R$ 1.491,45";
            e3.data_de_admissao = "2015-03-16";
            _employees[3] = e3;
        }


        public async Task<EmployeeItem[]> GetEmployeesAsync()
        {
            return _employees;
        }

        public async Task<EmployeeItem> GetEmployeeAsync(string matricula)
        {
            foreach(EmployeeItem e in _employees)
            {
                if (e.matricula == matricula)
                    return e;
            }

            return null;
        }
    }
}
