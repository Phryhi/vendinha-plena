using vendinhaPlena.Models;
using Microsoft.EntityFrameworkCore;
using vendinhaPlena.Services;
using vendinhaPlena.Enums;

var service = new ClienteService();
var serviceDivida = new DividaService();

while (true)
{   
    Environment.SetEnvironmentVariable(
        "ConnectionStrings__DefaultConnection",
        "Server=localhost;Port=5432;User Id=postgres;Password=anacamis2468;Database=postgres");

    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("---------- Bem-vindo à Vendinha ----------");
    Console.WriteLine("---------- Escolha uma opção ----------");
    Console.WriteLine("1. Cadastro de cliente");
    Console.WriteLine("2. Listar clientes");
    Console.WriteLine("3. Atualização de cliente");
    Console.WriteLine("4. Exclusão de cliente");
    Console.WriteLine("5. Sessão de dívidas");

    try
    {
        int escolha = int.Parse(Console.ReadLine());

        if (escolha == 1)
        {
            Console.Clear();
            Console.WriteLine("------------- Cadastro de Clientes -------------");
            Console.WriteLine("Nome: ");
            var nome = Console.ReadLine();
            Console.WriteLine("Cpf (Sem pontos ou traços): ");
            var cpf = Console.ReadLine();
            Console.WriteLine("Data de Nascimento (ex: 2000-01-01): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Email: ");
            var email = Console.ReadLine();

            var cliente = new Cliente{nome = nome, cpf = cpf, DataNascimento = dataNascimento, Email = email};
            var sucesso = service.Criar(cliente);
            if (!sucesso)
            {
                Console.Clear();
                Console.WriteLine("Erro ao cadastrar cliente");
            }
            else
            {    
                Console.Clear();
                Console.WriteLine("Cliente cadastrado com sucesso!");
            }

        }else if(escolha == 2)
        {
            int pag = 1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("------------- Listar Clientes -------------");
                var clientes = service.Listar(pag);

                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"Cliente: {cliente.nome}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine($"Idade: {cliente.Idade}");
                    Console.WriteLine($"Email: {cliente.Email}");
                    Console.WriteLine("Dívidas:");
                    decimal total = 0;

                    foreach (var divida in cliente.dividas)
                    {
                        Console.WriteLine($"   Situação: {divida.situacao}");
                        Console.WriteLine($"   Valor: R$ {divida.valor}");
                        Console.WriteLine("--------------------------------");
                        total += divida.valor;
                    }

                    Console.WriteLine($"Total das dívidas: R$ {total}");
                    Console.WriteLine("====================================");
                }

                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("-------------------- Mais ações -------------------");
                Console.WriteLine("1. Buscar");
                Console.WriteLine("2. Avançar página");
                Console.WriteLine("3. Voltar página");

                int opcao_busca = int.Parse(Console.ReadLine());

                if (opcao_busca == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Buscar nome:");
                    var nomeCliente = Console.ReadLine();
                    var buscaResultado = service.Buscar(nomeCliente);

                    foreach (var item in buscaResultado)
                    {
                        if(item != null)
                        {
                            Console.WriteLine("--------------------------------");
                            Console.WriteLine($"ID: {item.id}");
                            Console.WriteLine($"Nome: {item.nome}");
                            Console.WriteLine($"CPF: {item.cpf}");
                            Console.WriteLine($"Idade: {item.Idade}");
                            Console.WriteLine("--------------------------------");
                            
                            decimal total = 0;
                            foreach (var divida in item.dividas)
                            {
                                Console.WriteLine($"   Situação: {divida.situacao}");
                                Console.WriteLine($"   Valor: R$ {divida.valor}");
                                Console.WriteLine("--------------------------------");

                                total += divida.valor;
                            }

                            Console.WriteLine($"Total das dívidas: R$ {total}");
                            Console.WriteLine("====================================");
                        }
                        else
                        {
                            Console.WriteLine("Não foi possivel encontrar o cliente!");
                        }    
                    }
                    Console.ReadKey();
                }else if(opcao_busca == 2)
                {
                    Console.Clear();
                    pag++;
                    Console.ReadKey();
                }else if(opcao_busca == 3)
                {
                    Console.Clear();
                    pag--;
                    Console.ReadKey();
                }   
            }
        }else if(escolha == 3)
        {
            Console.Clear();
            Console.WriteLine("------------- Atualizar Cliente -------------");
            Console.WriteLine("Digite o id do cliente a ser alterado: ");
            int id_atualizar = int.Parse(Console.ReadLine());

            Console.WriteLine("Novo nome: ");
            var nome = Console.ReadLine();
            Console.WriteLine("Novo cpf: ");
            var cpf = Console.ReadLine();
            Console.WriteLine("Nova data de Nascimento: ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Novo email: ");
            var email = Console.ReadLine();

            var clienteAtualizar = new Cliente{id = id_atualizar, nome = nome, cpf = cpf, DataNascimento = dataNascimento, Email = email};
            var sucesso = service.Editar(clienteAtualizar, out var erros);
            if (!sucesso)
            {
                Console.Clear();
                Console.WriteLine("Erro ao atualizar cliente");
            }
            else
            {    
                Console.Clear();
                Console.WriteLine("Cliente atualizado com sucesso!");
            }
        }else if(escolha == 4)
        {
            Console.WriteLine("------------- Excluir Cliente -------------");
            Console.Clear();
            Console.WriteLine("Digite o ID do cliente a ser excluído: ");
            int idExcluir = int.Parse(Console.ReadLine());

            var clienteExcluir = new Cliente{id = idExcluir};
            var sucesso = service.Excluir(clienteExcluir);
            if (!sucesso)
            {
                Console.Clear();
                Console.WriteLine("Erro ao excluir cliente");
            }
            else
            {    
                Console.Clear();
                Console.WriteLine("Cliente excluído com sucesso");
            }
        }else if(escolha == 5)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Atenção! Dívidas devem ser atribuídas a clientes já cadastrados no sistema!");
                Console.WriteLine("1. Cadastrar dívida");
                Console.WriteLine("2. Mostrar divida");
                Console.WriteLine("3. Pagar divida");
            
                int escolhaDivida = int.Parse(Console.ReadLine());
                if (escolhaDivida == 1)
                {
                    Console.Clear();
                    Console.WriteLine("------------- Cadastrar dívida -------------");

                    Console.WriteLine("ID do cliente devedor:");
                    int idCliente = int.Parse(Console.ReadLine());
                    Console.WriteLine("Valor da dívida: ");
                    decimal valor = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Situação: ");
                    Console.WriteLine("0 - pago | 1 - não pago");
                    SituacaoDivida situacao = (SituacaoDivida)int.Parse(Console.ReadLine());
                    Console.WriteLine("Data da criação: ");
                    DateTime dataCriacao = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Data Pagamento: ");
                    DateTime dataPagamento = DateTime.Parse(Console.ReadLine());

                    var divida = new Dividas{valor = valor, situacao = situacao, dataCriacao = dataCriacao, dataPagamento = dataPagamento, idCliente = idCliente};

                    var sucesso = serviceDivida.CriarDivida(divida);
                    if (!sucesso)
                    {
                        Console.Clear();
                        Console.WriteLine("Erro ao cadastrar dívida");
                    }
                    else
                    {    
                        Console.Clear();
                        Console.WriteLine("Dívida cadastrada com sucesso");   
                    }
                }else if(escolhaDivida == 2)
                {
                    Console.Clear();
                    Console.WriteLine("------------ Mostrar dívida ------------");
                    Console.WriteLine("Insira o ID do cliente para visualizar dívida:");
                    int id_cliente = int.Parse(Console.ReadLine());
                    var dividas = serviceDivida.ListarDivida(id_cliente);

                    foreach (var divida in dividas)
                    {
                        if(divida != null)
                        {
                            Console.WriteLine("Dívida: {0} - Data de vencimento: {1} - Valor: {2} - Situação: {3}", divida.id_divida, divida.dataPagamento, divida.valor, divida.situacao);
                        }
                        else
                        {
                            Console.WriteLine("Cliente não possui dívida.");
                        }
                    }
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("------------- Mais ações -------------");
                    Console.WriteLine($"1. Pagar dívida do cliente {id_cliente}");
                    int escolha_divida = int.Parse(Console.ReadLine());

                    if (escolha_divida == 1)
                        {
                        try
                        {
                            var sucesso = serviceDivida.Pagar(id_cliente);
                            Console.Clear();

                            if (!sucesso)
                            {
                                Console.WriteLine("Dívida não foi encontrada ou dívida já paga!");
                            }
                            else
                            {
                                Console.WriteLine("Dívida paga com sucesso");
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("Erro ao pagar dívida");
                        }
                    }
                }else if(escolhaDivida == 3)
                {
                    Console.Clear();
                    Console.WriteLine("------------- Pagar dívida ------------");
                    Console.WriteLine("Insira o id do cliente para pagar sua divida:");
                    int id_pagar = int.Parse(Console.ReadLine());

                    try
                    {
                        var sucesso = serviceDivida.Pagar(id_pagar);
                        Console.Clear();

                        if (!sucesso)
                        {
                            Console.WriteLine("Dívida não foi encontrada ou dívida já paga!");
                        }
                        else
                        {
                            Console.WriteLine("Dívida paga com sucesso");
                        }
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Erro ao pagar dívida");
                    }
                }
                else
                {
                    Console.WriteLine("Voltando");
                    break;
                }   
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Tchauzinho!");
            break;
        }

    }
    catch (Exception)
    {
        Console.WriteLine("OOps, parece que houve um erro, tente novamento");
    }
}