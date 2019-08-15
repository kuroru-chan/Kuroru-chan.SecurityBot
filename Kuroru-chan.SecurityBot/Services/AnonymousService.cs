using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuroruChan.SecurityBot.Services
{
    public class AnonymousService : IAnonymousService
    {
        private Dictionary<int, string> _anoymousNames;
        private List<string> _availableAnoymousNames;
        public void GenerateAnoymousNames()
        {
            _anoymousNames = new Dictionary<int, string>();
            _availableAnoymousNames = new List<string>() {
                "M.L. XU",
                "J. ZHENG",
                "Y.M. YANG",
                "X.Y. LI",
                "C.T. ZHOU",
                "S.Z KE",
                "G. CHENG",
                "X.M. LUO",
                "J. LIN",
                "J.Q. LIANG"
            };
        }

        public string GetAnoymousName(int userId)
        {
            if (!_anoymousNames.ContainsKey(userId))
            {
                var anoymousName = _availableAnoymousNames.FirstOrDefault();
                if (anoymousName != null)
                {
                    _availableAnoymousNames.Remove(anoymousName);
                }
                else
                {
                    anoymousName = $"{_anoymousNames.Values.ToList()[new Random().Next(0, _anoymousNames.Values.Count)]}{new Random().Next()}";
                }
                _anoymousNames.Add(userId, anoymousName);
            }
            return _anoymousNames.GetValueOrDefault(userId);
        }
    }
}
