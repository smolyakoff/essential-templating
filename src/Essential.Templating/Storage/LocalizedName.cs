using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Essential.Templating.Storage
{
    public class LocalizedName
    {
        private const char CultureDelimiter = '.';

        private readonly CultureInfo _culture;
        private readonly string _name;

        public LocalizedName(string localizedName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(localizedName));

            string name;
            CultureInfo culture;

            Extract(localizedName, out name, out culture);
            _name = name;
            _culture = culture;
        }

        public LocalizedName(string name, CultureInfo culture)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
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
                yield return ToString();
                yield return string.Format("{0}{1}{2}", _name, CultureDelimiter, _culture.TwoLetterISOLanguageName);
                yield return Name;
            }
        }

        private static void Extract(string localizedName, out string name, out CultureInfo culture)
        {
            Contract.Requires(localizedName != null);
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