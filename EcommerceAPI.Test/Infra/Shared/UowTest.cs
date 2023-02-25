using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Infra.Shared
{
    public class UowTest : IDisposable
    {
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
