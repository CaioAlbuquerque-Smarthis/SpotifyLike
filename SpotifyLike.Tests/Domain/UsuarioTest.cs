using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain
{
    public class UsuarioTest
    {
        [Fact]
        public void DeveCriarUsuarioComSucesso()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            //Assert
            Assert.NotNull(usuario.Email);
            Assert.NotNull(usuario.Nome);
            Assert.True(usuario.Email == email);
            Assert.True(usuario.Nome == nome);
            Assert.True(usuario.Senha != senha);

            Assert.True(usuario.Assinaturas.Count > 0);
            Assert.Same(usuario.Assinaturas[0].Plano, plano);

            Assert.True(usuario.Cartoes.Count > 0);
            Assert.Same(usuario.Cartoes[0], cartao);

            Assert.True(usuario.Playlists.Count > 0);
            Assert.True(usuario.Playlists[0].Nome == "Favoritas");
            Assert.False(usuario.Playlists[0].Publica);
        }

        [Fact]
        public void NaoDeveCriarUsuarioComCartaoSemLimite()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 10M,
                Numero = "6465465466",
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            //Act
            Assert.Throws<Exception>(() =>
            {
                Usuario usuario = new Usuario();
                usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);
            });

        }

        [Fact]
        public void DeveNotificarUsuario()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 20M,
                Numero = "6465465466",
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            Assert.True(usuario.Notificacoes.Count() > 0);
        }
        [Fact]
        public void DeveAdicionarBandaFavorita()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 20M,
                Numero = "6465465466",
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            Banda banda = new Banda()
            {
                Id = Guid.NewGuid(),
                Nome = "Dummy Banda",
                Descricao = "Descrição Banda",
                Backdrop = "Backdrop Banda"

            };

            usuario.AdicionarBandaFavorita(banda);

            Assert.True(usuario.BandasFavoritas.Count() > 0);
        }

        [Fact]
        public void DeveAdicionarMusicaEmPlaylist()
        {
            Musica musica = new Musica()
            {
                Id = Guid.NewGuid(),
                Nome = "Musica boa",
                Duracao = 3
            };

            Playlist playlist = new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = "Playlist de Musica",
                Publica = true,
                DtCriacao = DateTime.Now,
                Favorita = false
            };
            
            playlist.AdicionarMusica(musica);
        }

        [Fact]
        public void DeveAdicionarPlaylistParaUsuario()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 20M,
                Numero = "6465465466",
            };

            string nome = "Dummy Usuario";
            string email = "teste@teste.com";
            string senha = "123456";

            Usuario usuario = new Usuario();
            usuario.CriarConta(nome, email, senha, DateTime.Now, plano, cartao);

            Musica musica = new Musica()
            {
                Id = Guid.NewGuid(),
                Nome = "Musica boa",
                Duracao = 3
            };

            Playlist playlist = new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = "Playlist de Musica",
                Publica = true,
                DtCriacao = DateTime.Now,
                Favorita = false
            };

            playlist.AdicionarMusica(musica);

            usuario.AdicionarPlaylist(playlist);

            Assert.True(usuario.Playlists.Count() > 0);
        }
    }
}
