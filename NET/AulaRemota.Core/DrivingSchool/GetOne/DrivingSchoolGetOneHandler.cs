using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

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
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.Files)
                    .Include(e => e.User)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (result == null) throw new CustomException("Não encontrado", HttpStatusCode.NotFound);


                return new DrivingSchoolGetOneResponse
                {
                    Id = result.Id,
                    CorporateName = result.CorporateName,
                    FantasyName = result.FantasyName,
                    StateRegistration = result.StateRegistration,
                    FoundingDate = result.FoundingDate,
                    Email = result.Email,
                    Description = result.Description,
                    Site = result.Site,
                    Cnpj = result.Cnpj,
                    AddressId = result.AddressId,
                    Address = result.Address,
                    UserId = result.UserId,
                    User = result.User,
                    Files = result.Files,
                    PhonesNumbers = result.PhonesNumbers
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
