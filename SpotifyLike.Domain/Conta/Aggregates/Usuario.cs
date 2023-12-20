using System;
using SpotifyLike.Domain.Transacao.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Streaming.Aggregates;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Usuario
    {

        private const string NOME_PLAYLIST = "Favoritas";

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DtNascimento { get; set; }
        public List<Cartao> Cartoes { get; set; } = new List<Cartao>();
        public List<Assinatura> Assinaturas { get; set;} = new List<Assinatura>();

        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
        public List<Banda> BandasFavoritas { get; set; }

        public List<Notificacao.Aggregates.Notificacao> Notificacoes { get; set; } = new List<Notificacao.Aggregates.Notificacao>();

        //public Notificacao.Aggregates.Notificacao notificacao { get; set; } = new Notificacao.Aggregates.Notificacao();

        public void CriarConta(string nome, string email, string senha, DateTime dtNascimento, Plano plano, Cartao cartao)
        {
            this.Nome = nome;
            this.Email = email;

            //Todo: Transformar a senha em hash
            this.Senha = this.HashSenha(senha);

            this.DtNascimento = dtNascimento;

            //Assinar um plano
            this.AssinarPlano(plano, cartao);

            //Adicionar cartão na conta do usuário
            this.AdicionarCartao(cartao);

            //Criar a playlist padrão do usuário
            this.CriarPlaylist(nome: NOME_PLAYLIST, publica: false);
        }

        private void DesativarCartao(Cartao cartao)
        {
            cartao.Ativo = false;
        }

        private void AtivarCartao(Cartao cartao)
        {
            cartao.Ativo = true;
        }

        private void AdicionarBandaFavorita(Banda banda)
        {
            this.BandasFavoritas.Add(banda);
        }

        public void CriarPlaylist(string nome, bool publica = true, bool favorita = false)
        {
            this.Playlists.Add(new Playlist()
            {
                Nome = nome,
                Publica = false,
                DtCriacao = DateTime.Now,
                Usuario = this,
                Favorita = favorita
            });
        }

        private void AdicionarCartao(Cartao cartao)
            => this.Cartoes.Add(cartao);

        private void AssinarPlano(Plano plano, Cartao cartao)
        {
            //Debitar o valor do plano no cartao

            cartao.CriarTransacao(new Transacao.ValueObject.Merchant() { Nome = plano.Nome }, new Core.ValueObject.Monetario(plano.Valor), plano.Descricao);

            //Desativo caso tenha alguma assinatura ativa
            DesativarAssinaturaAtiva();

            //Adiciona nova assinatura
            this.Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                Plano = plano,
                DtAtivacao = DateTime.Now,

            });
        }

        private void DesativarAssinaturaAtiva()
        {
            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(x => x.Ativo))
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }
        }

        private String CriptografarSenha(string senhaAberta)
        {
            //Bcript seria a melhor biblioteca pra armazenar senha

            SHA256 criptoProvider = SHA256.Create();

            byte[] btexto = Encoding.UTF8.GetBytes(senhaAberta);

            var criptoResult = criptoProvider.ComputeHash(btexto);

            return Convert.ToHexString(criptoResult);
        }
        private String HashSenha(string senhaAberta)
        {
            //Bcript seria a melhor biblioteca pra armazenar senha
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);

            string hash = BCrypt.Net.BCrypt.HashPassword(senhaAberta, salt);

            return hash;
        }
    }
}
