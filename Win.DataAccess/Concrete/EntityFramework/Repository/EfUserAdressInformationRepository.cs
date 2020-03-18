using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.DataAccess.Abstract;
using Win.Entities.Dto;
using Win.Entities.Enum;
using Win.Entities.Model;

namespace Win.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfUserAdressInformationRepository : IUserAdressInformationDal
    {
        private Context context = new Context();

        public void Add(UserAdressInformation entity)
        {
            entity.AuditStatus = (short)AuditStatus.created;
            entity.AuditDate = DateTime.Now;
            entity.CreatedDate = DateTime.Now;
            //entity.Phone = (entity.Phone == null) ? entity.Phone : entity.Phone.Trim();
            entity.Phone = 0.ToString();
            context.UserAdressInformation.Add(entity);
            context.SaveChanges();
        }

        public UserAdressInformation Get(int Id)
        {
            return context.UserAdressInformation.FirstOrDefault(x => x.AuditStatus != (short)AuditStatus.deleted && x.UserID == Id);
        }
        public bool Update(UserAdressInformation entity)
        {
            var addressInDB = Get(entity.UserID);
            if (addressInDB == null)
                return false;
            else
            {
                addressInDB.Adress = entity.Adress;
                addressInDB.AuditDate = DateTime.Now;
                addressInDB.AuditStatus = (short)AuditStatus.updated;
                addressInDB.CityId = entity.CityId;
                addressInDB.TownId = entity.TownId;
                addressInDB.DistrictId = entity.DistrictId;
                addressInDB.NeighborhoodId = entity.NeighborhoodId;
                addressInDB.Name = entity.Name;
                addressInDB.Note = entity.Note;
                addressInDB.Phone = (entity.Phone == null) ? entity.Phone : entity.Phone.Trim();
                addressInDB.Surname = entity.Surname;
                context.SaveChanges();
                return true;
            }
        }

        public UserAdressInformationViewModel GetUserAdressInformation(int id)
        {
            var user = (from uai in context.UserAdressInformation 
                        join c in context.City on uai.CityId equals c.CityID into temp
                        join t in context.Town on uai.TownId equals t.TownID into temp2
                        join d in context.District on uai.DistrictId equals d.DistrictID into temp3
                        join e in context.Neighborhood on uai.NeighborhoodId equals e.NeighborhoodID into temp4

                        from c in temp.DefaultIfEmpty()
                        from t in temp2.DefaultIfEmpty()
                        from d in temp3.DefaultIfEmpty()
                        from e in temp4.DefaultIfEmpty()
                        
                        where uai.UserID == id
                        select new UserAdressInformationViewModel
                        {
                            cityId = c.CityID,
                            city = c.CityName,
                            townId = t.TownID,
                            town = t.TownName,
                            districtId = d.DistrictID,
                            district = d.DistrictName,
                            neighborhoodId = e.NeighborhoodID,
                            neighborhood = e.NeighborhoodName
                        }
                ).FirstOrDefault();

            return user;
        }
    }
}
