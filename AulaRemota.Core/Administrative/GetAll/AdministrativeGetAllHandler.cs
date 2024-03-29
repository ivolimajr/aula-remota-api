﻿using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Administrative.GetAll
{
    public class AdministrativeGetAllHandler : IRequestHandler<AdministrativeGetAllInput, List<AdministrativeModel>>
    {
        private readonly IRepository<AdministrativeModel, int> _administrativeRepository;

        public AdministrativeGetAllHandler(IRepository<AdministrativeModel, int> administrativeRepository) =>
            _administrativeRepository = administrativeRepository;

        public async Task<List<AdministrativeModel>> Handle(AdministrativeGetAllInput request, CancellationToken cancellationToken) {
            var result = _administrativeRepository.Context
                .Set<AdministrativeModel>()
                .Include(e => e.DrivingSchool)
                .ToList();
            return result;
        }
            
    }
}
