using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain
{
    public class CartaoTests
    {
        [Fact]
        public void DeveCriarTransacaoComSucesso()
        {


            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "646326472364238"
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Nome = "Dummy"
            };

            cartao.CriarTransacao(merchant, 19M, "Dummy Transacao");
            Assert.True(cartao.Transacoes.Count > 0);
            Assert.True(cartao.Limite == 961.10M);
        }

        [Fact]
        public void NaoDeveAssinarPlanoComCartaoInativo()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = false,
                Limite = 1000M,
                Numero = "6465465466",
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            Assert.Throws<System.Exception>(
                () => usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao));

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Nome = "Dummy"
            };

        }

        [Fact]
        public void NaoDeveAssinarPlanoComCartaoSemLimite()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 10M,
                Numero = "6465465466",
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            Assert.Throws<System.Exception>(
                () => usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao));


        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoSemLimite()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 20M,
                Numero = "6465465466",
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Nome = "Dummy"
            };

            Assert.Throws<System.Exception>(
            () => cartao.CriarTransacao(merchant, 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoValorDuplicado()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao()
            {
                DtTransacao = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Nome = "Dummy"
            };

            Assert.Throws<System.Exception>(
                () => cartao.CriarTransacao(merchant, 19M, "Dummy Transacao"));

        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoAltoFrequencia()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao()
            {
                DtTransacao = DateTime.Now.AddMinutes(-1),
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao()
            {
                DtTransacao = DateTime.Now.AddMinutes(-0.5),
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Aggregates.Transacao()
            {
                DtTransacao = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });


            var merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
            {
                Nome = "Dummy"
            };

            Assert.Throws<System.Exception>(
                () => cartao.CriarTransacao(merchant, 19M, "Dummy Transacao"));
        }
    }
}
