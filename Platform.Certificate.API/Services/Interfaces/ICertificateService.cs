﻿using Platform.Certificate.API.Common.Enums;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.Certificates;
using Platform.Certificate.API.Models.Forms.Certificates;

namespace Platform.Certificate.API.Services.Interfaces;

public interface ICertificateService
{
    public Task<ServiceResponse<CertificateDto>> Add(CertificateForm form);
    public Task<ServiceResponse<List<CertificateDto>>> Get(GetCertificateListForm form);
    public Task<ServiceResponse<CertificateDto>> GetByNumber(string number);
    public Task<ServiceResponse<bool>> UpdateState(Guid id, CertificateStateEnum state);
    public Task<ServiceResponse<bool>> Delete(Guid id);
    public Task<ServiceResponse<CertificateDto>> GetById(Guid id);
}