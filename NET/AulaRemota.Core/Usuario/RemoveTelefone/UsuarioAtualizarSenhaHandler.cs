using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AulaRemota.Core.Usuario.RemoveTelefone
{
    public class RemoveTelefoneHandler : IRequestHandler<RemoveTelefoneInput, bool>
    {
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public RemoveTelefoneHandler(IRepository<TelefoneModel> telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        public async Task<bool> Handle(RemoveTelefoneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Busca Inválida");

            try
            {
                var result = await _telefoneRepository.Context.Set<TelefoneModel>()
                                        .Include(e => e.Edriving)
                                        .Include(e => e.Administrativo)
                                        .Include(e => e.Aluno)
                                        .Include(e => e.AutoEscola)
                                        .Include(e => e.Instrutor)
                                        .Include(e => e.Parceiro)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();
                if (result == null) throw new HttpClientCustomException("Não Encontrado");

                if(result.Edriving != null && result.Edriving.Telefones.Count == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");
                if(result.Administrativo != null && result.Telefone.Length == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");
                if(result.Aluno != null && result.Telefone.Length == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");
                if(result.AutoEscola != null && result.Telefone.Length == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");
                if(result.Instrutor != null && result.Telefone.Length == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");
                if(result.Parceiro != null && result.Telefone.Length == 1) 
                    throw new HttpClientCustomException("Remoção inválida, usuário não pode ficar sem telefone cadastrado");

                _telefoneRepository.Delete(result);
                await _telefoneRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
