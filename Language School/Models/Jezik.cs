using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Models
{
   public class Jezik
    {
        private string _naziv;

        public string GetJezik()
        { return _naziv; }

        public void SetJezik(string value)
        { _naziv = value; }
        public override string ToString()
        {
            return GetJezik();
        }
    }
   
}
