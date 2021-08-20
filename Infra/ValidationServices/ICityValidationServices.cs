using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ValidationServices
{
    public interface ICityValidationServices
    {
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExist(string id, string name);
    }
}
