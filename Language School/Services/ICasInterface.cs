using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Services
{
    interface ICasInterface
    {
        void SaveCas(Object o);

        void ReadCas(string filename);

        void DeleteCas(string ID);

        void UpdateCas(Object o);
    }
}
