using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Exceptions
{
    public class UnregisteredGameException : Exception
    {
        public UnregisteredGameException()
            : base("Este jogo não está cadastrado")
        { }
    }
}
