using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class UserAdressInformationManager
    {
        IUserAdressInformationDal _userAdressInformationDal;
        public UserAdressInformationManager(IUserAdressInformationDal userAdressInformationDal)
        {
            _userAdressInformationDal = userAdressInformationDal;
        }

        public void Add(UserAdressInformation entity)
        {
            _userAdressInformationDal.Add(entity);
        }
        public UserAdressInformation Get(int id)
        {
            return _userAdressInformationDal.Get(id);
        }
        public bool Update(UserAdressInformation entity)
        {
            return _userAdressInformationDal.Update(entity);
        }
        public UserAdressInformationViewModel GetUserAdressInformationViewModel(int id)
        {
            return _userAdressInformationDal.GetUserAdressInformation(id);
        }
    }
}
