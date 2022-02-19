using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.DrivingSchool.GetOne
{
    public class DrivingSchoolGetOneHandler : IRequestHandler<DrivingSchoolGetOneInput, DrivingSchoolGetOneResponse>
    {
        private readonly IRepository<DrivingSchoolModel> _autoEscolaRepository;

        public DrivingSchoolGetOneHandler(IRepository<DrivingSchoolModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<DrivingSchoolGetOneResponse> Handle(DrivingSchoolGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = await _autoEscolaRepository.Context.Set<DrivingSchoolModel>()
                    .Include(e => e.Telefones)
                    .Include(e => e.Endereco)
                    .Include(e => e.Arquivos)
                    .Include(e => e.Usuario)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (result == null) throw new CustomException("Não encontrado");


                return new DrivingSchoolGetOneResponse
                {
                    Id = result.Id,
                    RazaoSocial = result.RazaoSocial,
                    NomeFantasia = result.NomeFantasia,
                    InscricaoEstadual = result.InscricaoEstadual,
                    DataFundacao = result.DataFundacao,
                    Email = result.Email,
                    Descricao = result.Descricao,
                    Site = result.Site,
                    Cnpj = result.Cnpj,
                    EnderecoId = result.EnderecoId,
                    Endereco = result.Endereco,
                    UsuarioId = result.UsuarioId,
                    Usuario = result.Usuario,
                    Arquivos = result.Arquivos,
                    Telefones = result.Telefones
                };
            }
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
        }
    }
}
