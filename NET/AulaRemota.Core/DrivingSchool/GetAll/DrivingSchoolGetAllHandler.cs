using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.DrivingSchool.GetAll
{
    public class DrivingSchoolGetAllHandler : IRequestHandler<DrivingSchoolGetAllInput, List<DrivingSchoolModel>>
    {
        private readonly IRepository<DrivingSchoolModel> _autoEscolaRepository;

        public DrivingSchoolGetAllHandler(IRepository<DrivingSchoolModel> autoEscolaRepository)
        {
            _autoEscolaRepository = autoEscolaRepository;
        }

        public async Task<List<DrivingSchoolModel>> Handle(DrivingSchoolGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(request.Uf))
                {
                    return await _autoEscolaRepository.Context.Set<DrivingSchoolModel>()
                            .Where(e => e.Address.Uf.Equals(request.Uf))
                            .ToListAsync();
                }
                
                return _autoEscolaRepository.GetAll().ToList();
            }
            catch (CustomException e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolModel),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NoContent
                });
            }
        }
    }
}
