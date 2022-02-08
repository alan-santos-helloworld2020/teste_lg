using System;
using cadastro.models;
using cadastro.database;
namespace Cadastro
{
    public class Cadastro
    {
        public static void Main(string[] args)
        {

              DbAction db = new DbAction();
              Cliente cl = new Cliente();
              Contato ct = new Contato();
              Console.WriteLine("informa uma opção\ndigite 1 para cadastrar o cliente\ndigite 2 para cadastrar o contato");
              int opcao = Convert.ToInt32(Console.ReadLine());
              switch (opcao)
              {
                  case 1:

                    cl.nome = "cliente";
                    cl.telefone = "(21)00000-0000";
                    cl.email = "fulano@gmail.com";
                    cl.cep = "00000-000";
                    db.salvarCliente(cl);
                    
                  break;

                  case 2:
                    
                    ct.nome="contato";
                    ct.telefone="(21)00000-0000";
                    ct.grau_parentesco="pai";
                    ct.idCliente=1;
                    if(db.validacao(ct)){
                       db.salvarContato(ct);
                    }else{
                        Console.WriteLine("Contato  não cadastrado :-(");
                    }

                  break;
                  
              }


        }
        
    }
    
}
