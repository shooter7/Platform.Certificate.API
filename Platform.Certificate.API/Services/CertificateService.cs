using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.DAL.Data;
using Platform.Certificate.API.Models.Dtos.Certificates;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.Certificates;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly EfContext _efContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CertificateService(EfContext efContext, IMapper mapper, IFileService fileService)
        {
            _efContext = efContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ServiceResponse<CertificateDto>> Add(CertificateForm form)
        {
            var serviceResponse = new ServiceResponse<CertificateDto>();
            try
            {
                var certificate = await _efContext.Certificates.FirstOrDefaultAsync(x => x.Number.Equals(form.Number));
                if (certificate is not null)
                {
                    serviceResponse.Failed().WithError(0, "Duplicate number", null);
                }
                else
                {
                    certificate = new Models.Dbs.Certificate()
                    {
                        Number = form.Number,
                        Path = await _fileService.UploadFile(form.File)
                    };

                    await _efContext.Certificates.AddAsync(certificate);
                    await _efContext.SaveChangesAsync();
                    serviceResponse.Successful().WithData(_mapper.Map<CertificateDto>(certificate));
                }
            }
            catch (Exception e)
            {
                serviceResponse.Failed().WithError(0, e.Message, null);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CertificateDto>>> Get(GetCertificateListForm form)
        {
            var query = _efContext.Certificates
                .WhereIf(form.Number != null, x => x.Number.Contains(form.Number))
                .Select(x => _mapper.Map<CertificateDto>(x));
            var count = await query.CountAsync();
            var list = await query.Skip(form.Start).Take(form.Take).ToListAsync();
            return new ServiceResponse<List<CertificateDto>>().Successful().WithData(list).WithCount(count);
        }

        public async Task<ServiceResponse<CertificateDto>> GetByNumber(string number)
        {
            var serviceResponse = new ServiceResponse<CertificateDto>();
            var certificate = await _efContext.Certificates.FirstOrDefaultAsync(x => x.Number.Equals(number));
            if (certificate == null)
            {
                return serviceResponse.Failed().WithError(1, "Not found", null);
            }

            var userDto = _mapper.Map<CertificateDto>(certificate);
            return serviceResponse.Successful().WithData(userDto);
        }
    }
}