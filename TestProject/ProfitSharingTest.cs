using Xunit;
using ProfitSharingChallenge.Models;

namespace TestProject
{
    public class ProfitSharingTest
    {
        IEmployeesData _dataFake;
        IProfitSharing _profitSharing;

        public ProfitSharingTest()
        {
            _dataFake = new EmployeeDataFake();
            _profitSharing = new ProfitSharing(_dataFake);
        }


        [Fact]
        public void TestCalculateParticipationValueof1000()
        {
            var result = _profitSharing.GetParticipation("1000");

            // Expected
            var e0_valor_da_participacao = "R$ 9,60";
            var e1_valor_da_participacao = "R$ 48,00";
            var e2_valor_da_participacao = "R$ 60,00";
            var e3_valor_da_participacao = "R$ 60,00";
            var total_de_funcionarios = "4";
            var total_disponibilizado = "R$ 1000,00";
            var total_distribuido = "R$ 177,60";
            var saldo_total_disponibilizado = "R$ 822,40";

            // Test
            Assert.Equal(e0_valor_da_participacao, result.Result.participacoes[0].valor_da_participacao);
            Assert.Equal(e1_valor_da_participacao, result.Result.participacoes[1].valor_da_participacao);
            Assert.Equal(e2_valor_da_participacao, result.Result.participacoes[2].valor_da_participacao);
            Assert.Equal(e3_valor_da_participacao, result.Result.participacoes[3].valor_da_participacao);
            Assert.Equal(total_de_funcionarios, result.Result.total_de_funcionarios);
            Assert.Equal(total_disponibilizado, result.Result.total_disponibilizado);
            Assert.Equal(total_distribuido, result.Result.total_distribuido);
            Assert.Equal(saldo_total_disponibilizado, result.Result.saldo_total_distponibilizado);
        }
    }
}
