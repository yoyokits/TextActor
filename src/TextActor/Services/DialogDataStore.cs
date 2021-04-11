namespace TextActor.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TextActor.Models;

    /// <summary>
    /// Defines the <see cref="DialogDataStore" />.
    /// </summary>
    public class DialogDataStore : IDataStore<Dialog>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogDataStore"/> class.
        /// </summary>
        public DialogDataStore()
        {
            Dialogs = new List<Dialog>()
            {
                new Dialog { ActorId = 0, Message = "Hi, how are you?" },
                new Dialog { ActorId = 1, Message = "Hi, never better, and how are you?" },
                new Dialog { ActorId = 0, Message = "I am really good!" },
                new Dialog { ActorId = 2, Message = "Hey guys, wow, that's great we can meet here now." },
                new Dialog { ActorId = 1, Message = "Yeah right, ok guys I bring the drinks." },
            };
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Dialogs.
        /// </summary>
        private List<Dialog> Dialogs { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The AddItemAsync.
        /// </summary>
        /// <param name="item">The item<see cref="Dialog"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> AddItemAsync(Dialog item)
        {
            Dialogs.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// The DeleteItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = Dialogs.Where((Dialog arg) => arg.Id == id).FirstOrDefault();
            Dialogs.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// The GetItemAsync.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{Dialog}"/>.</returns>
        public async Task<Dialog> GetItemAsync(int id)
        {
            return await Task.FromResult(Dialogs.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// The GetItemsAsync.
        /// </summary>
        /// <param name="forceRefresh">The forceRefresh<see cref="bool"/>.</param>
        /// <returns>The <see cref="Task{IList{Dialog}}"/>.</returns>
        public async Task<IList<Dialog>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Dialogs);
        }

        /// <summary>
        /// The UpdateItemAsync.
        /// </summary>
        /// <param name="dialog">The dialog<see cref="Dialog"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public async Task<bool> UpdateItemAsync(Dialog dialog)
        {
            var oldItem = Dialogs.Where((Dialog arg) => arg.Id == dialog.Id).FirstOrDefault();
            Dialogs.Remove(oldItem);
            Dialogs.Add(dialog);

            return await Task.FromResult(true);
        }

        #endregion Methods
    }
}