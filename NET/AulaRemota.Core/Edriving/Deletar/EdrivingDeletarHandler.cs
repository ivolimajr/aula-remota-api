using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarHandler : IRequestHandler<EdrivingDeletarInput, bool>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public EdrivingDeletarHandler(
            IRepository<EdrivingModel> edrivingRepository, 
            IRepository<UsuarioModel> usuarioRepository, 
            IRepository<TelefoneModel> telefoneRepository
            )
        {
            _edrivingRepository = edrivingRepository;
            _telefoneRepository = telefoneRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(EdrivingDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");
            try
            {
                _edrivingRepository.CreateTransaction();
                var edriving = await _edrivingRepository.Context
                                        .Set<EdrivingModel>()
                                        .Include(e => e.Usuario)
                                        .Include(e => e.Telefones)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();
                if (edriving == null) throw new HttpClientCustomException("Não encontrado");

                _edrivingRepository.Delete(edriving);
                _usuarioRepository.Delete(edriving.Usuario);

                foreach (var item in edriving.Telefones)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }

                _edrivingRepository.Save();
                _edrivingRepository.Commit();
                return true;
            }
            catch (Exception e)
            {
                _edrivingRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}
