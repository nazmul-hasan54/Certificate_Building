using Application.Contracts.BaseInterface;
using Application.Contracts.Certificates;
using Application.CoreInformation.Context;
using Application.Domain.Models;
using Application.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Certificates
{
    public class CertificateRepository: BaseRepository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(ProjectDbContext context): base(context)
        {
            
        }

        public void DeleteCertificate(Certificate certificate)
        {
            Delete(certificate);
        }

        public void SaveCertificate(Certificate certificate)
        {
            Create(certificate);
        }

        public void UpdateCertificate(Certificate certificate)
        {
            Update(certificate);
        }
    }
}
