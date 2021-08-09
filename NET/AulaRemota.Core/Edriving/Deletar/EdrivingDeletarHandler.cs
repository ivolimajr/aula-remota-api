using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;
using System.Linq;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarHandler : IRequestHandler<EdrivingDeletarInput, bool>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public EdrivingDeletarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<TelefoneModel> telefoneRepository)
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
                var edriving = await _edrivingRepository.GetByIdAsync(request.Id);
                if (edriving == null) throw new HttpClientCustomException("Não encontrado");

                var usuario = await _usuarioRepository.GetByIdAsync(edriving.UsuarioId);
                var telefones = _telefoneRepository.GetWhere(e => e.Edriving.Id == request.Id).ToList();

                edriving.Telefones = telefones;
                edriving.Usuario = usuario;
                //REMOVE O OBJETO
                _edrivingRepository.Delete(edriving);

                _usuarioRepository.Delete(usuario);

                foreach (var item in telefones)
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
