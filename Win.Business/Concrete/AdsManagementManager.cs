using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Model;

namespace Win.Business.Concrete
{
    public class AdsManagementManager
    {
        IAdsManagementDal _adsManagementDal;
        public AdsManagementManager(IAdsManagementDal adsManagementDal)
        {
            _adsManagementDal = adsManagementDal;
        }
        public List<AdsManagement> GetAll()
        {
            return _adsManagementDal.GetAll();
        }
        public AdsManagement Get(int id)
        {
            return _adsManagementDal.Get(id);
        }
        public AdsManagement GetByUserId(int userId)
        {
            return _adsManagementDal.GetByUserId(userId);
        }
        public int Add(AdsManagement entity)
        {
            return _adsManagementDal.Add(entity);
        }
        public void Delete(int id)
        {
            _adsManagementDal.Delete(id);
        }
        public bool Update(AdsManagement entity)
        {
            return _adsManagementDal.Update(entity);
        }
        public bool ClickAds(int userId)
        {
            var adsItem = GetByUserId(userId);
            if (adsItem == null)
            {
                return (Add(new AdsManagement()
                {
                    UserId = userId,
                    LegalClick = 1,
                    TotalClick = 1
                }) > 0) ? true : false;
            }
            else
            {
                if ((DateTime.Now - adsItem.AuditDate).TotalMinutes > 10)
                {
                    adsItem.LegalClick += 1;
                    adsItem.TotalClick += 1;
                }
                else
                    adsItem.IllegalClick += 1;
                return Update(adsItem);
            }
        }
    }
}
