﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Essential.Templating.Common.Storage
{
    public class LocalizedName
    {
        private const char CultureDelimiter = '.';

        private readonly CultureInfo _culture;
        private readonly string _name;

        public LocalizedName(string localizedName)
        {
            if (string.IsNullOrEmpty(localizedName))
            {
                throw new ArgumentException("Localized name cannot be null or empty.", "localizedName");
            }

            string name;
            CultureInfo culture;

            Extract(localizedName, out name, out culture);
            _name = name;
            _culture = culture;
        }

        public LocalizedName(string name, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", "name");
            }

            culture = culture ?? CultureInfo.InvariantCulture;
            _name = name;
            _culture = culture;
        }

        public string Name
        {
            get { return _name; }
        }

        public CultureInfo Culture
        {
            get { return _culture; }
        }

        public override string ToString()
        {
            return _culture.Equals(CultureInfo.InvariantCulture)
                ? _name
                : string.Format("{0}{1}{2}", _name, CultureDelimiter, _culture);
        }

        public IEnumerable<string> GetPossibleNames()
        {
            if (Culture.Equals(CultureInfo.InvariantCulture))
            {
                yield return Name;
            }
            else
            {
                var mostSpecificName = ToString();
                yield return mostSpecificName;
                var languageOnlyName =
                    string.Format("{0}{1}{2}", _name, CultureDelimiter, _culture.TwoLetterISOLanguageName);
                if (!string.Equals(mostSpecificName, languageOnlyName, StringComparison.Ordinal))
                {
                    yield return languageOnlyName;
                }

                yield return Name;
            }
        }

        private static void Extract(string localizedName, out string name, out CultureInfo culture)
        {
            var delimiterIndex = localizedName.LastIndexOf(CultureDelimiter);
            if (delimiterIndex < 0)
            {
                name = localizedName;
                culture = CultureInfo.InvariantCulture;
                return;
            }

            var cultureString = localizedName.Substring(delimiterIndex + 1);
            try
            {
                culture = CultureInfo.GetCultureInfo(cultureString);
                name = localizedName.Substring(0, delimiterIndex + 1);
            }
            catch (Exception)
            {
                culture = CultureInfo.InvariantCulture;
                name = localizedName;
            }
        }
    }
}