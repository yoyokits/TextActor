namespace TextActor.Models
{
    using System;
    using TextActor.Extensions;
    using TextActor.Helpers;

    /// <summary>
    /// Defines the <see cref="Dialog" />.
    /// </summary>
    public class Dialog : NotifyPropertyChanged
    {
        #region Fields

        private string _actorId;

        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        public Dialog()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Actor Id who play this dialogue.
        /// </summary>
        public string ActorId { get => _actorId; set => this.Set(this.PropertyChangedHandler, ref _actorId, value); }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get => _message; set => this.Set(this.PropertyChangedHandler, ref _message, value); }

        #endregion Properties
    }
}