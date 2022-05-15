using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.Application.Service.IService
{
    public interface IClaimService
    {
        string GetUserId();

        string GetClaim(string key);
    }
}
