using AFSClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public static class Utils
    {
        public static async void LoadTags(this object obj, bool subscribe = false)
        {
            if (obj != null)
            {
                Type objType = obj.GetType();
                PropertyInfo piTagPrefix = objType.GetProperty("TagPrefix");
                if (piTagPrefix != null)
                {
                    string tagPrefix = piTagPrefix.GetValue(obj) as string;
                    foreach (var propInfo in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (propInfo.PropertyType == typeof(TagNode))
                        {
                            var subscribeAttr = propInfo.GetCustomAttribute<SubscribeAttribute>();
                            string tagPath = $"{tagPrefix}/{propInfo.Name}";
                            tagPath = AFSClient.Utils.NormalizePath(tagPath);

                            if (subscribe || subscribeAttr != null)
                            {
                                TagNode tag = await ServerConnectorProvider.SubscribeTag(tagPath);
                                propInfo.SetValue(obj, tag);
                            }
                            else
                            {
                                TagNode tag = ServerConnectorProvider.GetTag(tagPath);
                                propInfo.SetValue(obj, tag);
                            }

                        }
                    }
                }
            }
        }


        /// <summary>
        /// The method to get the first day of the month
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1 - dateTime.Day);
        }

        /// <summary>
        /// The method to get the last day of the month
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return dateTime.GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// The method to get first day of the year
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfYear(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1 - dateTime.DayOfYear);
        }

        /// <summary>
        /// The method to get last day of the year
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfYear(this DateTime dateTime)
        {
            return dateTime.Date.GetFirstDayOfYear().AddYears(1).AddDays(-1);
        }
    }
}
