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
                //return _autoEscolaRepository.Context.Set<AutoEscolaModel>()
                //            .Include(e => e.Files)
                //            .Include(e => e.PhonesNumbers)
                //            .Include(e => e.Address)
                //            .Include(e => e.User)
                //            .ToList();
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
