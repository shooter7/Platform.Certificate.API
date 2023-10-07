using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platform.Certificate.API.Common.Enums;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.DAL.Data;
using Platform.Certificate.API.Models.Dtos.Certificates;
using Platform.Certificate.API.Models.Forms.Certificates;
using Platform.Certificate.API.Models.Objects;
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
                var certificate = await _efContext.Certificates.FirstOrDefaultAsync(x => x.Code.Equals(form.Code));
                if (certificate is not null)
                {
                    serviceResponse.Failed().WithError(0, "Duplicate number", null);
                }
                else
                {
                    certificate = new Models.Dbs.Certificate()
                    {
                        PublicId = Guid.NewGuid(),
                        Path = await _fileService.UploadFile(form.File),
                        State = CertificateStateEnum.Pending,
                        Country = form.Country,
                        Address = form.Address,
                        Code = form.Code,
                        ExpireDate = form.ExpireDate,
                        Importer = new Agent()
                        {
                            Fullname = form.Importer.Fullname,
                            Address = form.Importer.Address,
                            Phone = form.Importer.Phone,
                            Email = form.Importer.Email,
                            Country = form.Importer.Country
                        },
                        Exporter = new Agent()
                        {
                            Fullname = form.Exporter.Fullname,
                            Address = form.Exporter.Address,
                            Phone = form.Exporter.Phone,
                            Email = form.Exporter.Email,
                            Country = form.Exporter.Country
                        }
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
                .WhereIf(form.Code != null, x => x.Code.Equals(form.Code))
                .WhereIf(form.Country != null, x => x.Country.Contains(form.Country))
                .WhereIf(form.State != null, x => x.State == form.State)
                .Select(x => _mapper.Map<CertificateDto>(x));
            var count = await query.CountAsync();
            var list = await query.Skip(form.Start).Take(form.Take).ToListAsync();
            return new ServiceResponse<List<CertificateDto>>().Successful().WithData(list).WithCount(count);
        }

        public async Task<ServiceResponse<CertificateDto>> GetByCode(string code)
        {
            var serviceResponse = new ServiceResponse<CertificateDto>();
            var certificate = await _efContext.Certificates.FirstOrDefaultAsync(x => x.Code.Equals(code));
            if (certificate == null)
            {
                return serviceResponse.Failed().WithError(1, "Not found", null);
            }

            var userDto = _mapper.Map<CertificateDto>(certificate);
            return serviceResponse.Successful().WithData(userDto);
        }

        public async Task<ServiceResponse<bool>> UpdateState(int id, CertificateStateEnum state)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var certificate = await _efContext.Certificates.FindAsync(id);
                if (certificate == null)
                {
                    return serviceResponse.Failed().WithError(1, "Not found", null);
                }

                certificate.State = state;
                await _efContext.SaveChangesAsync();
                return serviceResponse.Successful().WithData(true);
            }
            catch (Exception e)
            {
                return serviceResponse.Failed().WithError(0, e.Message, null);
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var certificate = await _efContext.Certificates.FindAsync(id);
                if (certificate == null)
                {
                    return serviceResponse.Failed().WithError(1, "Not found", null);
                }

                _efContext.Certificates.Remove(certificate);
                await _efContext.SaveChangesAsync();
                return serviceResponse.Successful().WithData(true);
            }
            catch (Exception e)
            {
                return serviceResponse.Failed().WithError(0, e.Message, null);
            }
        }

        public async Task<ServiceResponse<CertificateDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<CertificateDto>();
            try
            {
                var certificate = await _efContext.Certificates.FindAsync(id);
                if (certificate == null)
                {
                    return serviceResponse.Failed().WithError(1, "Not found", null);
                }

                var certificateDto = _mapper.Map<CertificateDto>(certificate);
                return serviceResponse.Successful().WithData(certificateDto);
            }
            catch (Exception e)
            {
                return serviceResponse.Failed().WithError(0, e.Message, null);
            }
        }
    }
}