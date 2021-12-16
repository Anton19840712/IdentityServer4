using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Client.Mvc.ViewModels
{
    public interface IClaimManager<T>
    {
        void AddTokenInfo(string nameToken, T element);
    }
}
