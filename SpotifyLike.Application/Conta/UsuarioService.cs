using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.Extension;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;
using SpotifyLike.Repository.Repository;


namespace SpotifyLike.Application.Conta
{
    public class UsuarioService
    {
        private IMapper Mapper { get; set; }
        private UsuarioRepository UsuarioRepository { get; set; }
        private PlanoRepository PlanoRepository { get; set; }


        public UsuarioService(IMapper mapper, UsuarioRepository usuarioRepository, PlanoRepository planoRepository)
        {
            Mapper = mapper;
            UsuarioRepository = usuarioRepository;
            PlanoRepository = planoRepository;
        }

        public UsuarioDto Criar(UsuarioDto dto)
        {
            if (this.UsuarioRepository.Exists(x => x.Email == dto.Email))
                throw new Exception("Usuario já existente na base");


            Plano plano = this.PlanoRepository.GetById(dto.PlanoId);

            if (plano == null)
                throw new Exception("Plano não existente ou não encontrado");

            Cartao cartao = this.Mapper.Map<Cartao>(dto.Cartao);

            Usuario usuario = new Usuario();
            usuario.CriarConta(dto.Nome, dto.Email, dto.Senha, dto.DtNascimento, plano, cartao);

            //TODO: GRAVAR MA BASE DE DADOS
            this.UsuarioRepository.Save(usuario);
            var result = this.Mapper.Map<UsuarioDto>(usuario);

            return result;

        }

        public UsuarioDto Obter(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            var result = this.Mapper.Map<UsuarioDto>(usuario);
            return result;
        }

        public UsuarioDto Autenticar(String email, String senha)
        {
            var usuario = this.UsuarioRepository.Find(x => x.Email == email && x.Senha == senha.HashSHA256()).FirstOrDefault();
            var result = this.Mapper.Map<UsuarioDto>(usuario);
            return result;
        }

        private Musica MusicaDtoParaMusica(MusicaDto musicaDto)
        {
            Musica musica = new Musica();
            musica.Id = musicaDto.Id;
            musica.Nome = musicaDto.Nome;
            musica.Duracao = musicaDto.Duracao;

            return musica;
        }

        private MusicaDto MusicaParaMusicaDto(Musica musica)
        {
            MusicaDto musicaDto = new MusicaDto();
            musicaDto.Id = musica.Id;
            musicaDto.Nome = musica.Nome;
            musicaDto.Duracao = musica.Duracao;

            return musicaDto;
        }

        public UsuarioDto Favoritar(MusicaDto musicaDto, Guid idUsuario)
        {
            var usuario = this.UsuarioRepository.GetById(idUsuario);
            var musica = MusicaDtoParaMusica(musicaDto);
            //Criar a transformação de musicaDto pra Musica aí declarar a musica e usar ela no adicionar musica
            if (usuario.Playlists.Any(playlist => playlist.Favorita
                    && playlist.Musicas.Any(m => m.Id == musica.Id)))
            {
                return this.Mapper.Map<UsuarioDto>(usuario);
            }
            else
            {
                usuario.Playlists.FirstOrDefault(playlist => playlist.Favorita)
                    .AdicionarMusica(musica);
                this.UsuarioRepository.Update(usuario);
                return this.Mapper.Map<UsuarioDto>(usuario);
            };
        }
        public IEnumerable<MusicaDto> ObterFavoritas(Guid id)
        {
            var usuario = this.UsuarioRepository.GetById(id);
            var musicas = usuario.Playlists.FirstOrDefault(playlist => playlist.Favorita).Musicas;
            return musicas.Select(musica => MusicaParaMusicaDto(musica));
        }

        public IEnumerable<UsuarioDto> Obter()
        {
            var usuario = this.UsuarioRepository.GetAll();
            return this.Mapper.Map<IEnumerable<UsuarioDto>>(usuario);
        }

        public IEnumerable<MusicaRelatorioDto> GerarRelatorioMusicasFavoritas()
        {
            // Obter todos os usuários com suas playlists favoritas
            var usuarios = this.UsuarioRepository.GetAll();

            // Criar um dicionário para contar as ocorrências das músicas
            var musicaContagem = new Dictionary<Guid, MusicaRelatorioDto>();

            foreach (var usuario in usuarios)
            {
                var playlistFavoritaDto = this.ObterFavoritas(usuario.Id);

                if (playlistFavoritaDto != null)
                {
                    foreach (var musica in playlistFavoritaDto)
                    {
                        if (musicaContagem.ContainsKey(musica.Id))
                        {
                            musicaContagem[musica.Id].QuantidadeCurtidas++;
                        }
                        else
                        {
                            musicaContagem[musica.Id] = new MusicaRelatorioDto
                            {
                                MusicaId = musica.Id,
                                NomeMusica = musica.Nome,
                                QuantidadeCurtidas = 1
                            };
                        }
                    }
                }
            }

            return musicaContagem.Values.OrderByDescending(m => m.QuantidadeCurtidas);
        }
    }
}
