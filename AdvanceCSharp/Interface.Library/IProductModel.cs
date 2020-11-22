using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Library
{
    public interface IProductModel
    {
        string Title { get; set; }

        bool HasOrderBeenCompleted { get; }

        void ShipItem(CustomerModel customer);
    }
}
