﻿using System.Linq;
using System.Collections.Generic;
using System.Web;
using System;

namespace DynamicPlaceholders
{
	public class PlaceholdersContext
	{
		private const string PlaceholdersCacheKey = "DynamicPlaceholders";

		private static List<string> Placeholders
		{
			get
			{
				var placeholders = (List<string>)HttpContext.Current.Items[PlaceholdersCacheKey];

				if (placeholders == null)
				{
					HttpContext.Current.Items[PlaceholdersCacheKey] = placeholders = new List<string>();
				}

                return placeholders;
			}
		}

		public static string Add(string placeholderName, Guid renderingId)
		{
            return Add(placeholderName, renderingId, null);
		}

        public static string Add(string placeholderName, Guid renderingId, string uniqueKey)
        {
            var placeholder = string.Format("{0}_{1}", placeholderName, renderingId);

            if (string.IsNullOrEmpty(uniqueKey))
            {
                var count = 0;

                if ((count = Placeholders.Count(dp => dp.StartsWith(placeholder))) > 0)
                {
                    placeholder = string.Format("{0}_{1}", placeholder, count);
                }
            }
            else
            {
                placeholder = string.Format("{0}_{1}", placeholder, uniqueKey);
            }

            Placeholders.Add(placeholder);

            return placeholder;
        }
    }
}
