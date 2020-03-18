using System.Collections.Generic;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.DataAccess.Abstract
{
    public interface IUserAdressInformationDal
    {
        void Add(UserAdressInformation entity);
        UserAdressInformation Get(int Id);
        bool Update(UserAdressInformation entity);
        UserAdressInformationViewModel GetUserAdressInformation(int id);
    }
}
