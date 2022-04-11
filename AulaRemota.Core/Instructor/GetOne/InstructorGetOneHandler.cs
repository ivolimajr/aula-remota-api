using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using AulaRemota.Shared.Helpers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Instructor.GetOne
{
    public class InstructorGetOneHandler : IRequestHandler<InstructorGetOneInput, InstructorModel>
    {
        private readonly IRepository<InstructorModel, int> _instructorRepository;

        public InstructorGetOneHandler(IRepository<InstructorModel, int> instructorRepository) =>
            _instructorRepository = instructorRepository;

        public async Task<InstructorModel> Handle(InstructorGetOneInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                InstructorModel result;
                if (request.Uf.IsNull())
                {
                    result = await _instructorRepository
                                .Where(e => e.Id.Equals(request.Id))
                                .Include(e => e.User)
                                .Include(e => e.PhonesNumbers)
                                .Include(e => e.Address)
                                .Include(e => e.Files)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                }
                else
                {
                    result = await _instructorRepository
                                    .Where(e => e.Id.Equals(request.Id) && e.Address.Uf.Equals(request.Uf))
                                    .Include(e => e.User)
                                    .Include(e => e.PhonesNumbers)
                                    .Include(e => e.Address)
                                    .Include(e => e.Files)
                                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                }

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
                    ModelName = nameof(InstructorGetOneHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = HttpStatusCode.NotFound,
                    Data = result
                });
            }
        }
    }
}
