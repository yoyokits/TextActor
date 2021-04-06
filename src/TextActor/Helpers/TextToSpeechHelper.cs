namespace TextActor.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Essentials;

    /// <summary>
    /// Defines the <see cref="TextToSpeechHelper" />.
    /// </summary>
    internal static class TextToSpeechHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the DefaultLocale.
        /// </summary>
        public static Locale DefaultLocale { get; set; }

        /// <summary>
        /// Gets the Locales.
        /// </summary>
        public static List<Locale> Locales { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetLocale by name.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="Locale"/>.</returns>
        public static Locale GetLocale(string name)
        {
            var locales = Locales.Where(local => local.Name.Contains(name));
            return locales == null || !locales.Any() ? DefaultLocale : locales.First();
        }

        /// <summary>
        /// The InitializeAsync.
        /// </summary>
        public static async void InitializeAsync()
        {
            var locales = await TextToSpeech.GetLocalesAsync();
            Locales = locales.ToList();
            var englishLocales = Locales.Where(local => local.Name.Contains("English"));
            if (englishLocales == null || !englishLocales.Any())
            {
                DefaultLocale = Locales.FirstOrDefault();
                return;
            }

            var usEnglish = englishLocales.Where(local => local.Name.Contains("English(United States)"));
            if (usEnglish == null && !usEnglish.Any())
            {
                DefaultLocale = englishLocales.FirstOrDefault();
                return;
            }

            DefaultLocale = usEnglish.FirstOrDefault();
        }

        #endregion Methods
    }
}