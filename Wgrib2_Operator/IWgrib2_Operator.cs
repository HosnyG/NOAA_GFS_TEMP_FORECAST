using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wgrib2_Operator_NS
{
    public interface IWgrib2_Operator
    {
        Task<TempratureOutputResource> GetTempratureForecastAsync(string wgrib2File, double lat, double lon, int metersAboveGround = 2);
    }
}
