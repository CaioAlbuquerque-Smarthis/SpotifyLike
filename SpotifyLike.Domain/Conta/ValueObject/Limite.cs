using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.ValueObject
{
    public class Limite
    {
        public Decimal Valor { get; set; }
        public Limite(int valor)
        {
            if (valor < 0)
                throw new ArgumentException("Limite não pode ser negativo");
            this.Valor = valor;

        }
        public String Formatado()
        {
            return $"R$ {this.Valor.ToString("N2")}";
        }

    }
}
