using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.Utils
{
	public static class EnumHelper
	{
		public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
		{
			var enumType = typeof(TEnum);
			var name = Enum.GetName(enumType, value);
			if (name == null) return null;

			var fieldInfo = enumType.GetField(name);
			if (fieldInfo == null) return null;

			var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
			return attribute?.Description;
		}
	}
}
