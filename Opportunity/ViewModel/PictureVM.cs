using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Opportunity.ViewModel
{
    public class PictureVM : ViewModelBase
    {
        #region fields
        private BitmapImage source;
        public BitmapImage SourcePicture { get { return source; } set { source = value; RaisePropertyChanged("SourcePicture"); } }
        #endregion

        #region constructors        
        public PictureVM(BitmapImage bm)
        {
            SourcePicture = bm;
        }
        #endregion
    }
}
