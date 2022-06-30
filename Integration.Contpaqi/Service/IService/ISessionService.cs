using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Contpaqi.Service.IService
{
    public interface ISessionService
    {
        void InitConnection();
        void EndConnection();
    }
}
