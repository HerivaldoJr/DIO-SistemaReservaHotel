using System;

namespace SistemaReservaHotel
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite? Suite { get; set; }  // Adicionado ? para permitir null
        public int DiasReservados { get; set; }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
            Hospedes = new List<Pessoa>();
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (Suite == null)
                throw new InvalidOperationException("Suíte não foi definida para a reserva.");

            if (hospedes.Count > Suite.Capacidade)
                throw new ArgumentException($"A suíte {Suite.TipoSuite} suporta apenas {Suite.Capacidade} hóspedes.");

            Hospedes = hospedes;
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite ?? throw new ArgumentNullException(nameof(suite));
        }

        public int ObterQuantidadeHospedes()
        {
            return Hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            if (Suite == null)
                throw new InvalidOperationException("Suíte não foi definida para cálculo da diária.");

            decimal valor = DiasReservados * Suite.ValorDiaria;
            return DiasReservados >= 10 ? valor * 0.9m : valor;
        }
    }
}