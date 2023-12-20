using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.ValueObject;
using SpotifyLike.Domain.Notificacao.Aggregates;

namespace SpotifyLike.Domain.Transacao.Aggregates
{
    public class Cartao
    {
        private const int INTERVALO_TRANSACAO = -2;
        private const int REPETICAO_TRANSACAO_MERCHANT = 1;

        public Guid Id { get; set; }
        public Boolean Ativo { get; set; }
        public Monetario Limite { get; set; }
        public String Numero { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
        public DateTime DataVencimento { get; set; }
        public Usuario Usuario {  get; set; }
        public void CriarTransacao(Merchant merchant, Monetario valor, string Descricao = "")
        {
            //Verificar se o cartão está ativo
            this.IsCartaoAtivo();

            Transacao transacao = new Transacao();
            //transacao.UsuarioDestino = usuarioDestino;
            transacao.Merchant = merchant;
            transacao.Valor = valor;
            transacao.Descricao = Descricao;
            transacao.DtTransacao = DateTime.Now;

            //Verificar limite disponível
            this.VerificaLimite(transacao);

            //Verifica regras antifraude
            this.ValidarTransacao(transacao);

            //Cria numero de autorizacao
            transacao.Id = Guid.NewGuid();

            //Criar Notificação
            //Notificacao.Aggregates.Notificacao.Criar($"Notificação da transação {transacao.Id.ToString}", $"Notificação referente a transação {Descricao}", TipoNotificacao.Usuario, usuarioDestino, Usuario);
            //Notificacao.Aggregates.Notificacao notificacao = new Notificacao.Aggregates.Notificacao();
            Notificacao.Aggregates.Notificacao notificacao = new Notificacao.Aggregates.Notificacao();
            notificacao = Notificacao.Aggregates.Notificacao.Criar($"Notificação de transação realizada", $"Notificação referente a transação {Descricao}", TipoNotificacao.Sistema, this.Usuario);
            this.Usuario.Notificacoes.Add(notificacao);

            //Diminui o limite com o valor da transação
            this.Limite.Valor = this.Limite.Valor - transacao.Valor;

            this.Transacoes.Add(transacao);
        }

        public void AssociarUsuario(Usuario usuario)
        {
            this.Usuario = usuario;
        }

        private void ValidarTransacao(Transacao transacao)
        {
            var ultimasTransacoes = this.Transacoes.Where(x => x.DtTransacao >= DateTime.Now.AddMinutes(INTERVALO_TRANSACAO));

            if (ultimasTransacoes?.Count() >= 3)
                throw new Exception("Cartão utilizado muitas vezes em um período curto");

            var transacaoRepetidaPorMerchant = ultimasTransacoes?.Where(x => x.Merchant.Nome.ToUpper() == transacao.Merchant.Nome.ToUpper() && x.Valor == transacao.Valor).Count() == REPETICAO_TRANSACAO_MERCHANT;

            if (transacaoRepetidaPorMerchant)
                throw new Exception("Transação Duplicada para o mesmo cartão e o mesmo Comerciante");
        }

        private void VerificaLimite(Transacao transacao)
        {
            if (this.Limite < transacao.Valor)
                throw new Exception("Cartão não possui limite para esta transação");
        }

        private void IsCartaoAtivo()
        {
            if (this.Ativo == false)
            {
                throw new Exception("Cartão não está ativo");
            }
        }

    }
}
