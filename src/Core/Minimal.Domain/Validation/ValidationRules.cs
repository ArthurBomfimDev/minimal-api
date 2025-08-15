using Minimal.Domain.Validation.Result;
using System.Net.Mail;

namespace Minimal.Domain.Validation;

public static class ValidationRules
{
    #region string
    public static Notification? IsNull(string? value, string fieldName, bool isNullable = false) => (string.IsNullOrWhiteSpace(value) && !isNullable) == true ? Notification.Error($"O Campo: {fieldName}, não pode ser vazio!") : null;
    public static Notification? ValidateLength(string? value, string fieldName, int? maxLength = default, int? minLength = default, bool isNullable = false)
    {
        var isNull = IsNull(value, fieldName, isNullable);
        if (isNull != null)
            return isNull;

        if (value != null)
        {
            var lenght = value.Length;
            if (maxLength != default && lenght > maxLength)
                return Notification.Error($"O Campo: {fieldName}, não pode ser maior do que o limite: {maxLength}");
            if (minLength != default && lenght < minLength)
                return Notification.Error($"O Campo: {fieldName}, não pode ser menor do que o limite minimo: {minLength}");
        }

        return null;
    }
    public static Notification? IsEmail(string? email, string fieldName, int? maxLength = default, int? minLength = default, bool isNullable = false)
    {
        var validate = ValidateLength(email, fieldName, maxLength, minLength, isNullable);

        if (validate != null) return validate;

        if (email != null)
        {
            try
            {
                var valideMail = new MailAddress(email);
                return null;
            }
            catch
            {
                return Notification.Error($"O Campo: {fieldName} é um email, porem o valor digitado não confere com o padrão");
            }
        }

        return null;
    }
    #endregion

    #region numbers
    public static Notification? ValidateYear(int? year, string fieldName, int minYear, bool isNullable = false)
    {
        if (year == null)
            return isNullable ? null : Notification.Error($"O Campo: {fieldName}, não pode ser vazio");

        if (year > DateTime.Now.Year)
            return Notification.Error($"O Campo: {fieldName}, não pode ser maior do que a ano atual");

        if (year < minYear)
            return Notification.Error($"O Campo: {fieldName}, não pode ser menor que o ano de {minYear}");

        return null;
    }

    public static Notification? ValidatePrice(decimal? price, string fieldName, decimal? minPrice = 0.00m, decimal? maxPrice = null, bool isNullable = false)
    {
        if (price == null)
            return isNullable ? null : Notification.Error($"O Campo: {fieldName}, não pode ser vazio");

        if (price < 0)
            return Notification.Error($"O Campo: {fieldName}, não pode ser negativo");

        if (minPrice != null && price < minPrice)
            return Notification.Error($"O Campo: {fieldName}, não pode ser menor que {minPrice:C}");

        if (maxPrice != null && price > maxPrice)
            return Notification.Error($"O Campo: {fieldName}, não pode ser maior que {maxPrice:C}");

        return null;
    }
    #endregion

    #region Enuns
    public static Notification? ValidateEnum<TEnum>(TEnum enumValidate, string fieldName) where TEnum : struct
    {
        if (enumValidate.ToString() == null)
            return Notification.Error($"O Enum digitado no campo {fieldName} é invalido");

        return null;
    }
    #endregion
}