using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnicodeSlug
{
    public class SlugOptions
    {
        public SlugOptions()
        {
            AllowedChars = new char[] { '-', '_', '~' };
            Lowercase = true;
            Spaces = false;
        }

        // regex patterns for specific asian unicode blocks allowed
        // http://msdn.microsoft.com/en-us/library/20bw873z.aspx#SupportedNamedBlocks
        private readonly string cjkCodes =
            @"\p{IsCJKUnifiedIdeographs}|" +
            @"\p{IsHangulJamo}|" +
            @"\p{IsHangulSyllables}|";

        private readonly UnicodeCategory[] letterCategories = new UnicodeCategory[] {
            UnicodeCategory.LowercaseLetter,
            UnicodeCategory.ModifierLetter,
            UnicodeCategory.OtherLetter,
            UnicodeCategory.TitlecaseLetter,
            UnicodeCategory.UppercaseLetter
        };

        private readonly UnicodeCategory[] numberCategories = new UnicodeCategory[] {
            UnicodeCategory.DecimalDigitNumber,
            UnicodeCategory.LetterNumber,
            UnicodeCategory.OtherNumber
        };

        private readonly UnicodeCategory[] separatorCategories = new UnicodeCategory[] {
            UnicodeCategory.LineSeparator,
            UnicodeCategory.SpaceSeparator,
            UnicodeCategory.ParagraphSeparator
        };

        public char[] AllowedChars { get; set; }
        public bool Lowercase { get; set; }
        public bool Spaces { get; set; }

        public string GenerateSlug(string str)
        {
            str = str.Normalize(NormalizationForm.FormKC);
            var builder = new StringBuilder();

            foreach (var c in str)
            {
                if (AllowedChars.Contains(c)) 
                {
                    builder.Append(c);
                    continue;
                }

                if (Regex.Match(c.ToString(), cjkCodes).Success) 
                {
                    builder.Append(c);
                    continue;
                }

                // retain only letter and number characters
                // replace separator characters with spaces
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (letterCategories.Contains(unicodeCategory) || numberCategories.Contains(unicodeCategory))
                {
                    builder.Append(c);
                }
                else if (separatorCategories.Contains(unicodeCategory))
                {
                    builder.Append(' ');
                }
            }

            var slug = builder.ToString().Trim();

            // collapse spaces
            slug = Regex.Replace(slug, @"\s+", " ");

            if (!Spaces)
            {
                // replace spaces with hypens
                slug = Regex.Replace(slug, @"[\s\-]+", "-");
            }

            if (Lowercase)
            {
                slug = slug.ToLowerInvariant();
            }

            return slug;
        }
    }
}