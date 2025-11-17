using CRUDAPI.Data;
using System.Text.RegularExpressions;

namespace CRUDAPI.Models
{
    public static class Valid
    {
        const string compareField = @"^[\w-]+@[\w-]+\.\w+$";
        const string compareName = @"^[A-Z][a-z]+[0-9]+$";
        public static bool StringField(string field, int minLen,string nameField, out string error)
        {

            if (string.IsNullOrEmpty(field))
            {
                error = $"Текствове поле {nameField} пусте";
                return false;
            }
            else if(field.Length < minLen){
                error = $"Занадто коротке поле {nameField}";
                return false;
            }
            else if (!Regex.IsMatch(field, compareName))
            {
                error = $"Логін не відповідає шаблону";
                return false;
            }

            error = string.Empty;
            return true;

        }

        public static bool Email(string email,out string error)
        {
            if (!Regex.IsMatch(email, compareField))
            {
                error = "Невірна поштова адреса";
                return false;
            }

            error = string.Empty;
            return true;
        }

        public static bool CheckOwner(int id)
        {
            var owner = Elements.peopleItems.FirstOrDefault(A => A.Id == id);

            if (owner is null)
                return false;
            return true;

        }

        public static bool Check_Subscribe_Owner(int Subid, int OwnerId,out string error)
        {
            var subscription = Elements.subsripptionItems.FirstOrDefault(A => A.Id == Subid);

            if (subscription is null)
            {
                error = "Не знайдено вказаної підписки";
                return false;
            }
            else if (subscription.OwnerId != OwnerId)
            {
                error = "Неправильний власник підписки";
                return false;
            }

            error = string.Empty;
            return true;

        }



    }
}
