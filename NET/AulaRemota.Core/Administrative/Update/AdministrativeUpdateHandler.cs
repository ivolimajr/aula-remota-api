using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.Update
{
    internal class AdministrativeUpdateHandler : IRequestHandler<AdministrativeUpdateInput, AdministrativeUpdateResponse>
    {
        private readonly IRepository<DrivingSchoolModel, int> _drivingSchoolRepository;
        private readonly IRepository<UserModel, int> _userRepository;
        private readonly IRepository<PhoneModel, int> _phoneRepository;
        private readonly IRepository<EdrivingModel, int> _edrivingRepository;
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;

        public AdministrativeUpdateHandler(
            IRepository<DrivingSchoolModel, int> drivingSchoolRepository, 
            IRepository<UserModel, int> userRepository, 
            IRepository<PhoneModel, int> phoneRepository, 
            IRepository<EdrivingModel, int> edrivingRepository, 
            IRepository<AdministrativeModel, int> administrativeRepository
            )
        {
            _drivingSchoolRepository = drivingSchoolRepository;
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
            _edrivingRepository = edrivingRepository;
            _administrativeRepository = administrativeRepository;
        }

        public async Task<AdministrativeUpdateResponse> Handle(AdministrativeUpdateInput request, CancellationToken cancellationToken)
        {

            try
            {
                var user = await _administrativeRepository
                    .Where(e => e.Id.Equals(request.Id))
                    .Include(e => e.PhonesNumbers)
                    .Include(e => e.Address)
                    .Include(e => e.User)
                    .FirstOrDefaultAsync();

                Check.NotNull(user, "Não encontrado");

                if(!string.IsNullOrEmpty(request.Cpf) && request.Cpf != user.Cpf)
                    if (_edrivingRepository.Exists(e => e.Cpf.Equals(request.Cpf)))
                        throw new CustomException("Cpf já em uso");

                if(request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
