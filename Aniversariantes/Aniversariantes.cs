using System;

namespace AniversarioModel
{
    public class Aniversariantes
    {
        public Aniversariantes()
        {

        }
        public string primeiroNome { get; set; }
        public string segundoNome { get; set; }
        public DateTime dataAniversario { get; set; }
        public string nomeCompleto { get { return $"{primeiroNome} {segundoNome}"; } }

        public Aniversariantes(string primeironome, string segundonome, DateTime dataaniversario)
        {
            primeiroNome = primeironome;
            segundoNome = segundonome;
            dataAniversario = dataaniversario;

            
        }
    }
}
   
