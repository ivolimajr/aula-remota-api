using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System;

namespace AulaRemota.Core.DrivingSchool.GetOne
{
    public class DrivingSchoolGetOneHandler : IRequestHandler<DrivingSchoolGetOneInput, DrivingSchoolModel>
    {
        private readonly IRepository<DrivingSchoolModel, int> _autoEscolaRepository;

        public DrivingSchoolGetOneHandler(IRepository<DrivingSchoolModel, int> autoEscolaRepository) => _autoEscolaRepository = autoEscolaRepository;

        public async Task<DrivingSchoolModel> Handle(DrivingSchoolGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                var result = await _autoEscolaRepository.Where(e => e.Id.Equals(request.Id))
                                    .Include(e => e.User)
                                    .Include(e => e.PhonesNumbers)
                                    .Include(e => e.Address)
                                    .Include(e => e.Files)
                                    .Include(e => e.Administratives)
                                    .Include(e => e.Administratives).ThenInclude(e => e.Address)
                                    .Include(e => e.Administratives).ThenInclude(e => e.PhonesNumbers)
                                    .FirstOrDefaultAsync();

                 Check.NotNull(result, "Não encontrado");

                return result;
            }
            catch (Exception e)
            {
                object result = new
                {
                    id = request.Id
                };
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = result
                });
            }
        }
    }
}
