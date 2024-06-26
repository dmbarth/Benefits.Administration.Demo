using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Benefits.Administration.Application.Entities;
using Benefits.Administration.Application.Exceptions;
using Benefits.Administration.Application.Interfaces;
using Benefits.Administration.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Benefits.Administration.Application.Services 
{
  public class BenefitsService : IBenefitsService
  {
    private readonly ILogger<BenefitsService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BenefitsService(ILogger<BenefitsService> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Benefit> GetBenefitByIdAsync(long id)
    {
      var benefit = await _unitOfWork.Benefits.GetByIdAsync(id);

      if (benefit == null)
        throw new BenefitNotFoundException();

      return benefit;
    }

    public async Task<IEnumerable<Benefit>> GetBenefitsAsync()
    {
      return await _unitOfWork.Benefits.GetAllAsync();
    }

    public async Task<IEnumerable<Benefit>> GetBenefitsAsync(int? year)
    {
      if (year.HasValue == false)
        return await GetBenefitsAsync();
      
      return await _unitOfWork.Benefits.FindAsync(entity => entity.Year == year);
    }
  }
}