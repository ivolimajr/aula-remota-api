using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity.Auto_Escola;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AulaRemota.Core.Parceiro.Deletar
{
    public class ParceiroDeletarHandler : IRequestHandler<ParceiroDeletarInput, bool>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EnderecoModel> _enderecoRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public ParceiroDeletarHandler(
            IRepository<ParceiroModel> parceiroRepository, 
            IRepository<UsuarioModel> usuarioRepository, 
            IRepository<EnderecoModel> enderecoRepository,
            IRepository<TelefoneModel> telefoneRepository
            )
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
        } 

        public async Task<bool> Handle(ParceiroDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            try
            {
                _parceiroRepository.CreateTransaction();
                var parceiro = await _parceiroRepository.Context
                                        .Set<ParceiroModel>()
                                        .Include(e => e.Usuario)
                                        .Include(e => e.Endereco)
                                        .Include(e => e.Telefones)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();

                if (parceiro == null) throw new CustomException("Não encontrado");

                _parceiroRepository.Delete(parceiro);
                _usuarioRepository.Delete(parceiro.Usuario);
                _enderecoRepository.Delete(parceiro.Endereco);
                foreach (var item in parceiro.Telefones)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }

                _parceiroRepository.Save();
                _parceiroRepository.Commit();
                return true;
            }
            catch (Exception e)
            {
                _parceiroRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _parceiroRepository.Context.Dispose();
            }
        }
    }
}
