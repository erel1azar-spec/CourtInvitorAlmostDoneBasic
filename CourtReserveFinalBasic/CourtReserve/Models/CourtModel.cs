using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class CourtModel
    {
        #region Fields
        protected ObservableCollection<AdminExistingCourtsTextModel> courts = new();
        #endregion
        #region Properties
        public ObservableCollection<AdminExistingCourtsTextModel> Courts => courts;
        #endregion
        #region Public Functions
        public abstract Task LoadAsync();
        public abstract Task LoadAsyncClient();

        public abstract void SelectCourt(AdminExistingCourtsTextModel court);
        public abstract void SelectCourtClient(AdminExistingCourtsTextModel court);

        #endregion
        #region Protected Functions
        protected abstract void AddCourtFromDocument(IDocumentSnapshot document);
        #endregion
    }
}
