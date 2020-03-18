using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Win.Entities.Model;

namespace Win.AspMvcUI.Helpers.Util
{
    public class Check
    {
        public static string CharacterEdit(string text)
        {
            string edited = text;
            edited = edited.Replace("&#304;", "I");
            edited = edited.Replace("&#305;", "i");
            edited = edited.Replace("&#214;", "Ö");
            edited = edited.Replace("&#246;", "ö");
            edited = edited.Replace("&Ouml;", "Ö");
            edited = edited.Replace("&ouml;", "ö");
            edited = edited.Replace("&#220;", "Ü");
            edited = edited.Replace("&#252;", "ü");
            edited = edited.Replace("&Uuml;", "Ü");
            edited = edited.Replace("&uuml;", "ü");
            edited = edited.Replace("&#199;", "Ç");
            edited = edited.Replace("&#231;", "ç");
            edited = edited.Replace("&Ccedil;", "Ç");
            edited = edited.Replace("&ccedil;", "ç");
            edited = edited.Replace("&#286;", "G");
            edited = edited.Replace("&#287;", "g");
            edited = edited.Replace("&#350;", "S");
            edited = edited.Replace("&#351;", "s");
            return edited;
        }
        public static bool UserAddressInformation(UserAdressInformation address)
        {
            if (address == null)
                return false;
            List<bool> list = new List<bool>();
            list.Add(CheckString(address.Adress));
            list.Add(CheckString(address.CityId.ToString()));
            list.Add(CheckString(address.Name));
            list.Add(CheckString(address.Phone));
            list.Add(CheckString(address.Surname));
            return (list.Any(p => !p)) ? false : true;
        }
        public static bool CheckString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;
            else
                return true;
        }
    }
}