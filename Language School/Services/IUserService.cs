using SR62_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Services
{

    interface IUserService
    {
        void SaveUsers(Object o);

        void ReadUsers(string filename);

        void DeleteUser(string jmbg);

        void UpdateUser(Object o);
        bool Provera(RegistrovaniKorisnik reg);
    }
}
