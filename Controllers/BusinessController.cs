using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NIP.Models;
using NIP.Tools;

namespace NIP.Controllers
{
    [Route("api/[controller]")]
    public class BusinessController : Controller
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly ILogsRepository _logsRepository;
        private readonly KeyValidator _keyValidator;
        private readonly IMapper _mapper;

        public BusinessController(IBusinessRepository businessRepository, KeyValidator keyValidator, IMapper mapper,
            ILogsRepository logsRepository)
        {
            _businessRepository = businessRepository;
            _logsRepository = logsRepository;
            _keyValidator = keyValidator;
            _mapper = mapper;
        }

        [HttpGet("{key}")]
        public IActionResult Find(string key)
        {
            Business businessFound = null;

            key = _keyValidator.GetNormalizedKey(key);

            if(_keyValidator.IsNIP(key))
            {                
                businessFound = _businessRepository.FindByNIP(key);
            } 
            if(_keyValidator.IsKRS(key) && businessFound == null)
            {
                businessFound = _businessRepository.FindByKRS(key);
            } 
            if(_keyValidator.IsREGON(key) && businessFound == null)
            {
                businessFound = _businessRepository.FindByREGON(key);
            }

            if(businessFound == null)
                return NotFound();

            MakeLog(businessFound.Id);

            BusinessViewModel businessViewModel = _mapper.Map<Business, BusinessViewModel>(businessFound);

            return new ObjectResult(businessViewModel);
        }

        private void MakeLog(int businessId)
        {
            var headers = string.Empty;
            foreach (var header in Request.Headers)
                headers += header.Key + "=" + header.Value + Environment.NewLine;

            Log log = new Log()
            {
                Date = DateTime.Now,
                Headers = headers,
                BusinessId = businessId
            };

            _logsRepository.AddLog(log);
            _logsRepository.SaveChanges();
        }
    }
}