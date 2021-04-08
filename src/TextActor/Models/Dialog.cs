namespace TextActor.Models
{
    using TextActor.Extensions;
    using TextActor.Helpers;

    /// <summary>
    /// Defines the <see cref="Dialog" />.
    /// </summary>
    public class Dialog : NotifyPropertyChanged
    {
        #region Fields

        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        public Dialog()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Actor Id who play this dialogue..
        /// </summary>
        public int ActorId { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; } = IdCounter++;

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get => _message; set => this.Set(this.PropertyChangedHandler, ref _message, value); }

        /// <summary>
        /// Gets or sets the IdCounter.
        /// </summary>
        private static int IdCounter { get; set; }

        #endregion Properties
    }
}