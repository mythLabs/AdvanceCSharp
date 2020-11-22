using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Library
{
    public interface IDigitalProductModel: IProductModel
    {
        int TotalDownloadsLeft { get; }
    }
}
