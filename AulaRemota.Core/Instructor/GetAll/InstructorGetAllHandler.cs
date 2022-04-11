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

namespace AulaRemota.Core.Instructor.GetAll
{
    internal class InstructorGetAllHandler : IRequestHandler<InstructorGetAllInput, List<InstructorModel>>
    {
        private readonly IRepository<InstructorModel, int> _instructorRepository;

        public InstructorGetAllHandler(IRepository<InstructorModel, int> instructorRepository) =>
            _instructorRepository = instructorRepository;

        public async Task<List<InstructorModel>> Handle(InstructorGetAllInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request.Uf))
                    return _instructorRepository.All().ToList();

                return await _instructorRepository.Where(e => e.Address.Uf.Equals(request.Uf))
                        .ToListAsync();
            }
            catch (Exception e)
            {
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(InstructorGetAllHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NoContent
                });
            }
        }
    }
}
