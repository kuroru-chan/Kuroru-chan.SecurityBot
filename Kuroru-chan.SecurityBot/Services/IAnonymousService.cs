using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuroruChan.SecurityBot.Services
{
    public interface IAnonymousService
    {
        void GenerateAnoymousNames();
        string GetAnoymousName(int userId);
    }
}
