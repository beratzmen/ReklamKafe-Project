using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.Helpers.Util
{
    public class Calculation
    {
        public static string GetTitle(double point)
        {
            if (point < 1000)
                return "Çırak";
            else if (point >= 1000 && point < 3000)
                return "Kalifiye Eleman";
            else if (point >= 3000 && point < 6000)
                return "Usta";
            else if (point >= 6000 && point < 9000)
                return "Uzman";
            else if (point >= 9000 && point < 15000)
                return "Doçent";
            else if (point >= 15000 && point < 30000)
                return "Profesor";
            else if (point >= 30000)
                return "Ordinaryus";
            else
                return "Çırak";
        }
    }
}
