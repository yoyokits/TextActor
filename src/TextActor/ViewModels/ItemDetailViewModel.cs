namespace TextActor.ViewModels
{
    using System;
    using System.Diagnostics;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="ItemDetailViewModel" />.
    /// </summary>
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Fields

        private string _description;

        private int _itemId;

        private string _text;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ItemId.
        /// </summary>
        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text { get => _text; set => SetProperty(ref _text, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The LoadItemId.
        /// </summary>
        /// <param name="itemId">The itemId<see cref="int"/>.</param>
        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        #endregion Methods
    }
}