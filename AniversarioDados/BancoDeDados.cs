using System;
using System.Collections.Generic;
using AniversarioModel;

namespace AniversarioDados
{
    public abstract class BancoDeDados
    {
        public abstract void Salvar(Aniversariantes aniversariante);
        public abstract IEnumerable<Aniversariantes> BuscarTodosOsAniversarios();
        public abstract IEnumerable<Aniversariantes> BuscarTodosOsAniversarios(string nomeVarBusca);
        public abstract IEnumerable<Aniversariantes> BuscarTodosOsAniversariantesPeloAniversario(DateTime dataAniversarioBusca);
        public abstract IEnumerable<Aniversariantes> Remover(string nomeVarBusca);


    }
}
