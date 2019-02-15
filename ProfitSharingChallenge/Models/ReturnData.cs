using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitSharingChallenge.Models
{
    public interface IReturnData
    {
        void SetTFunc(int tf);
        void SetTDist(double td);
        void SetTDisp(double td);

        int GetTFunc();
        double GetTDist();
        double GetTDisp();
    }

    public class ReturnData : IReturnData
    {
        private int total_de_funcionarios;
        private double total_distribuido;
        private double total_disponibilizado;

        public ReturnData()
        {
            total_de_funcionarios = 0;
            total_distribuido = 0.0;
            total_disponibilizado = 0.0;
        }

        public void SetTFunc(int tf) { total_de_funcionarios = tf; }
        public void SetTDist(double td) { total_distribuido = td; }
        public void SetTDisp(double td) { total_disponibilizado = td; }

        public int GetTFunc() { return total_de_funcionarios; }
        public double GetTDist() { return total_distribuido; }
        public double GetTDisp() { return total_disponibilizado; }
    }
}
