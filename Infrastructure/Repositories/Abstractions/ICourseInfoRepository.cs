using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ICourseInfoRepository
    {
        Task<bool> CheckIsCourseExistAndActiveById(int courseId);
    }
}
