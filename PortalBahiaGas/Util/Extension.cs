using PortalBahiaGas.Attribute;
using PortalBahiaGas.Models.Entidade.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

public static class Extension
{
    public static string ObterDescricao(this Enum value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);

        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
        }
        return null;
    }


    public static SelectList ToSelectList<TEnum>(this TEnum obj, TipoFiltro tipo)
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        return new SelectList(
            Enum.GetValues(typeof(TEnum))
            .OfType<Enum>().Where(x => (Attribute.GetCustomAttribute(x.GetType().GetField(Enum.GetName(x.GetType(), x)), typeof(Filtro)) as Filtro).Tipo == tipo)
            .Select(x => new SelectListItem()
            {
                Text = x.ObterDescricao(),
                Value = (Convert.ToInt32(x)).ToString()
            }), "Value", "Text");
    }

    public static SelectList ToSelectList<TEnum>(this TEnum obj)
       where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        return new SelectList(
            Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem()
            {
                Text = x.ObterDescricao(),
                Value = (Convert.ToInt32(x)).ToString()
            }), "Value", "Text");
    }

    public static Dictionary<string, string> GetAuthors<TEnum>()
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        Dictionary<string, string> _dict = new Dictionary<string, string>();

        PropertyInfo[] props = typeof(TEnum).GetProperties();
        foreach (PropertyInfo prop in props)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                Filter authAttr = attr as Filter;
                if (authAttr != null)
                {
                    string propName = prop.Name;
                    string auth = authAttr.ToString();

                    _dict.Add(propName, auth);
                }
            }
        }

        return _dict;
    }

    public static string ToStringMD5(this string pString)
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pString);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

   

}
