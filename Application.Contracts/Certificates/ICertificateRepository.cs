using Application.Contracts.BaseInterface;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Certificates
{
    public interface ICertificateRepository: IBaseRepository<Certificate>
    {
        void SaveCertificate(Certificate certificate);
        void UpdateCertificate(Certificate certificate);
        void DeleteCertificate(Certificate certificate);
    }
}
