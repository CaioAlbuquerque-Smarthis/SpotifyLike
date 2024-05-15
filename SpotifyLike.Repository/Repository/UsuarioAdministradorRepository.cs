using SpotifyLike.Domain.Admin.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class UsuarioAdministradorRepository : RepositoryBase<UsuarioAdmin>
    {
        public UsuarioAdministradorRepository(SpotifyLikeAdminContext context) : base(context)
        {
        }
    }
}
