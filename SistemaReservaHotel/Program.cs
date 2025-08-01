using System;
using System.Collections.Generic;

namespace SistemaReservaHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sistema de Reserva de Hotel\n");

            // Criar suítes disponíveis
            var suitePremium = new Suite("Premium", 4, 250.00m);
            var suiteLuxo = new Suite("Luxo", 2, 150.00m);
            var suiteEconomica = new Suite("Econômica", 2, 100.00m);

            // Lista de suítes para exibição
            var suites = new List<Suite> { suitePremium, suiteLuxo, suiteEconomica };

            // Exibir suítes disponíveis
            Console.WriteLine("Suítes disponíveis:");
            foreach (var suite in suites)
            {
                Console.WriteLine($"- {suite.TipoSuite}: Capacidade {suite.Capacidade}, Diária R${suite.ValorDiaria}");
            }

            // Criar hóspedes
            var hospedes = new List<Pessoa>();
            Console.WriteLine("\nCadastro de Hóspedes:");

            while (true)
            {
                Console.Write("Nome do hóspede (ou 'sair' para finalizar): ");
                string? nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome)) continue;
                if (nome.ToLower() == "sair") break;

                Console.Write("Sobrenome: ");
                string? sobrenome = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(sobrenome))
                {
                    hospedes.Add(new Pessoa(nome, sobrenome));
                }
                else
                {
                    Console.WriteLine("Sobrenome não pode ser vazio. Tente novamente.");
                }
            }

            if (hospedes.Count == 0)
            {
                Console.WriteLine("Nenhum hóspede cadastrado. Encerrando...");
                return;
            }

            // Selecionar suíte
            Suite? suiteSelecionada = null;
            while (suiteSelecionada == null)
            {
                Console.Write("\nDigite o tipo da suíte desejada: ");
                string? tipoSuite = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(tipoSuite))
                {
                    suiteSelecionada = suites.Find(s => 
                        s.TipoSuite.Equals(tipoSuite, StringComparison.OrdinalIgnoreCase));
                }

                if (suiteSelecionada == null)
                {
                    Console.WriteLine("Suíte não encontrada. Tente novamente.");
                }
            }

            // Informar dias de reserva
            int diasReservados = 0;
            while (diasReservados <= 0)
            {
                Console.Write("Quantidade de dias reservados: ");
                string? inputDias = Console.ReadLine();
                if (!int.TryParse(inputDias, out diasReservados) || diasReservados <= 0)
                {
                    Console.WriteLine("Valor inválido. Digite um número positivo.");
                    diasReservados = 0;
                }
            }

            // Criar reserva
            try
            {
                var reserva = new Reserva(diasReservados);
                reserva.CadastrarSuite(suiteSelecionada);
                reserva.CadastrarHospedes(hospedes);

                // Exibir resumo da reserva
                Console.WriteLine("\nResumo da Reserva:");
                if (reserva.Suite != null)
                {
                    Console.WriteLine($"Suíte: {reserva.Suite.TipoSuite}");
                    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
                    Console.WriteLine($"Dias reservados: {reserva.DiasReservados}");
                    Console.WriteLine($"Valor total: R${reserva.CalcularValorDiaria():F2}");

                    if (reserva.DiasReservados >= 10)
                    {
                        Console.WriteLine("(Desconto de 10% aplicado para reservas com 10 ou mais dias)");
                    }
                }
                else
                {
                    Console.WriteLine("Erro: Suíte não foi definida corretamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar reserva: {ex.Message}");
            }
        }
    }
}